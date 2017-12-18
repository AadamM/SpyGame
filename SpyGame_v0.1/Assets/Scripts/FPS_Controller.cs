using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPS_Controller : MonoBehaviour {

	public float moveSpeed = 7.5f;
	public float mouseSens = 3f;
	public float vertClamp = 75f;
	public float jumpSpeed = 5f;
	public float defCamHeight;
	public float maxCamHeight;

	float rotH = 0f;
	float rotV = 0f;
	float verV = 0f;
	float target;

	Vector3 speed;
	Vector3 camPos;

	CharacterController charCon;
	Camera cam;

	// Use this for initialization
	void Start (){
		Screen.lockCursor = true;
		charCon = GetComponent<CharacterController>();
		cam = Camera.main;

		defCamHeight = cam.transform.localPosition.y;
		target = maxCamHeight;
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation
		rotH = Input.GetAxis("Mouse X") * mouseSens;
		rotV -= Input.GetAxis("Mouse Y") * mouseSens;

		rotV = Mathf.Clamp(rotV, -vertClamp, vertClamp);

		cam.transform.localRotation = Quaternion.Euler(rotV, 0, 0);

		transform.Rotate(0, rotH, 0);

		if(charCon.isGrounded){
			if(Input.GetButton("Jump")){
				verV = jumpSpeed;
			}
		} else {
			verV += Physics.gravity.y * Time.deltaTime;
		}
		// Movement
		float ySpeed = Input.GetAxis("Vertical") * moveSpeed;
		float xSpeed = Input.GetAxis("Horizontal") * moveSpeed;

		camPos = cam.transform.localPosition;
		if(( (ySpeed!=0) || (xSpeed!=0) ) && charCon.isGrounded){
			//Debug.Log(target.ToString() + camPos.y.ToString());

			if(Mathf.Abs(Mathf.Abs(camPos.y)-Mathf.Abs(defCamHeight+target)) < maxCamHeight/5){
				target=-target;
			}

			camPos.y = Mathf.Lerp(camPos.y, defCamHeight+target, .1f);
		} else {
			camPos.y = Mathf.Lerp(camPos.y, defCamHeight, .1f);
		}
		cam.transform.localPosition = camPos;

		speed = new Vector3(xSpeed, verV, ySpeed);
		speed = transform.rotation * speed;
		
		charCon.Move(speed * Time.deltaTime);
	}
}
