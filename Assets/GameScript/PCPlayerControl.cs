using UnityEngine;
using System;
using System.Collections;


public class PCPlayerControl : MonoBehaviour {


	public Transform playerCamera;
	private Vector3 playerMovement = Vector3.zero;
	private CharacterController playerController;


	public float forwardSpeed;
	public float sideSpeed;
	public float jumpModifier;

	public float pitch;
	public float yaw;


	public float mouseSensitivity;
	public float minYaw;
	public float maxYaw;


	private int yInvert = -1;
	private float yInput, xInput;
	private float currentPitch = 0;

	void Start ()

	{

		playerController = GetComponent<CharacterController>();

	}



	void FixedUpdate ()

	{
		movePlayer();
		movePlayerMouse();
		//transform.localPosition+=new Vector3(0,-0.5f,0);

		

	}

	void movePlayer()
	{

		playerMovement.Set(Input.GetAxis("Horizontal") * sideSpeed, 0.0f, Input.GetAxis("Vertical") * forwardSpeed);

		// Clamped to forwardSpeed so moving in diagonal isn't faster
		playerController.Move(transform.TransformDirection(Vector3.ClampMagnitude(playerMovement, forwardSpeed)));


		//if(Input.GetButton("Space"))
		if(Input.GetKeyDown("space"))
		{
			transform.localPosition+=new Vector3(0,2,0);

		}

	}



	void movePlayerMouse()

	{

		yInput = Input.GetAxisRaw("Mouse Y") * pitch * mouseSensitivity * yInvert;
		Debug.Log("Mouse yInput: "+ yInput);


		xInput = Input.GetAxisRaw("Mouse X") * yaw * mouseSensitivity;



		// Player rotates left and right (and camera with)

		transform.Rotate(Vector3.up * xInput);



		// Only camera rotates vertically

		currentPitch = Mathf.Clamp(currentPitch + yInput, minYaw, maxYaw);

		playerCamera.transform.localEulerAngles = Vector3.right * currentPitch;

	}

}