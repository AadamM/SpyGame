using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Controller : MonoBehaviour {

	public float moveSpeed = 7.5f;
	public float mouseSens = 3.0f;
	public float vertClamp = 75f;

	float rotV = 0f;

	// Use this for initialization
	void Start (){
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation
		float rotH = Input.GetAxis("Mouse X") * mouseSens;
		rotV -= Input.GetAxis("Mouse Y") * mouseSens;

		rotV = Mathf.Clamp(rotV, -vertClamp, vertClamp);

		Camera.main.transform.localRotation = Quaternion.Euler(rotV, 0, 0);

		transform.Rotate(0, rotH, 0);
		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * moveSpeed;

		Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
		speed = transform.rotation * speed;

		CharacterController charCon = GetComponent<CharacterController>();
		charCon.SimpleMove(speed);
	}
}
