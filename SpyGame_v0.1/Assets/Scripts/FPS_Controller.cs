using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPS_Controller : MonoBehaviour {

	public float moveSpeed = 7.5f;
	public float mouseSens = 3f;
	public float vertClamp = 75f;
	public float jumpSpeed = 5f;

	float rotH = 0f;
	float rotV = 0f;
	float verV = 0f;

	Vector3 speed;

	CharacterController charCon;

	// Use this for initialization
	void Start (){
		Screen.lockCursor = true;
		charCon = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation
		rotH = Input.GetAxis("Mouse X") * mouseSens;
		rotV -= Input.GetAxis("Mouse Y") * mouseSens;

		rotV = Mathf.Clamp(rotV, -vertClamp, vertClamp);

		Camera.main.transform.localRotation = Quaternion.Euler(rotV, 0, 0);

		transform.Rotate(0, rotH, 0);

		if(charCon.isGrounded){
			if(Input.GetButton("Jump")){
				verV = jumpSpeed;
			}
		} else {
			verV += Physics.gravity.y * Time.deltaTime;
		}
		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * moveSpeed;

		speed = new Vector3(sideSpeed, verV, forwardSpeed);
		speed = transform.rotation * speed;
		
		charCon.Move(speed * Time.deltaTime);
	}
}
