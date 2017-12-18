using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPS_Controller : MonoBehaviour {

	public float moveSpeed = 7.5f;
	public float sprintMult = 1.5f;
	public float crouchSpeedMult = .75f;

	public float crouchHeightMult = .7f;

	public float mouseSens = 3f;
	public float vertClamp = 75f;
	public float jumpSpeed = 5f;
	public float maxCamHeight;
	public float gravMult = 1.1f;

	float defHeight;

	float rotH = 0f;
	float rotV = 0f;

	float defSpeed;
	float verV = 0f;
	float xSpeed = 0f;
	float ySpeed = 0f;

	float target;
	float defCamHeight;

	Vector3 speed;
	Vector3 camPos;

	CharacterController charCon;
	Camera cam;

	void Start (){
		// Making the cursor invisible
		Screen.lockCursor = true;

		// Collecting default values at runtime
		charCon = GetComponent<CharacterController>();
		cam = Camera.main;
		defHeight = charCon.height;
		defSpeed = moveSpeed;
		defCamHeight = cam.transform.localPosition.y;

		// Setting target height of cam
		target = maxCamHeight;
	}

	void Update () {
		// Horizontal rotation
		rotH = Input.GetAxis("Mouse X") * mouseSens;
		transform.Rotate(0, rotH, 0);

		// Vertical rotation
		rotV -= Input.GetAxis("Mouse Y") * mouseSens;
		rotV = Mathf.Clamp(rotV, -vertClamp, vertClamp);
		cam.transform.localRotation = Quaternion.Euler(rotV, 0, 0);

		// Configuring viewbobbing
		camPos = cam.transform.localPosition;
		if(( (ySpeed!=0) || (xSpeed!=0) ) && charCon.isGrounded){
			if(Mathf.Abs(Mathf.Abs(camPos.y)-Mathf.Abs(defCamHeight+target)) < maxCamHeight/5){
				target=-target;
			}

			camPos.y = Mathf.Lerp(camPos.y, defCamHeight+target, ((moveSpeed/(defSpeed*sprintMult))*.17f));
		} else {
			camPos.y = Mathf.Lerp(camPos.y, defCamHeight, .1f);
		}
		cam.transform.localPosition = camPos;

		// Jumping
		if(charCon.isGrounded){
			if(Input.GetButton("Jump")){
				verV = jumpSpeed;
			}
		} else {
			verV += Physics.gravity.y * gravMult * Time.deltaTime;
		}

		// Collecting input for sprinting and crouching
		if(Input.GetKey(KeyCode.LeftShift)){
			moveSpeed = defSpeed * sprintMult;
			charCon.height = defHeight;
		} else if(Input.GetKey(KeyCode.C)){
			moveSpeed = defSpeed * crouchSpeedMult;
			charCon.height = defHeight * crouchHeightMult;
		} else {
			moveSpeed = defSpeed;
			charCon.height = defHeight;
		}
		// Collecting movment input
		ySpeed = Input.GetAxis("Vertical") * moveSpeed;
		xSpeed = Input.GetAxis("Horizontal") * moveSpeed;

		// Translating movment input to actual movement
		speed = new Vector3(xSpeed, verV, ySpeed);
		speed = transform.rotation * speed;
		charCon.Move(speed * Time.deltaTime);
	}
}
