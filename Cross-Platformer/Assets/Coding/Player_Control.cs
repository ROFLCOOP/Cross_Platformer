using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour {
	[Tooltip("This will affect how fast the player moves")]
	[Range(1, 100)]
	public float Speed = 25.0f;

	private Vector3 gravity = new Vector3 (0.0f, 0.6f, 0.0f);

	//private bool spaceHeld = false;
	private bool jumping = false;
	private float jumpDecelerationTimer = 0;
	private const float timerIncrement = 0.001f;
	private const int maxDistanceray = 1000;
	private float height = 0;

	// Use this for initialization
	void Start() {

	}


	Vector3 frameJumpVelocity = new Vector3(0, 0, 0);
	Vector3 Velocity = new Vector3(0, 0, 0);
	Vector3 Jump = new Vector3(0, 0, 0);

	GameObject ground;

	void updateHeightFromGroundDetails ()
	{
		RaycastHit hit;
		int layerMask = 1 << 8;
		layerMask = ~layerMask;
		if (Physics.Raycast(transform.position + new Vector3(0, 0.35f, 0), -transform.up, out hit, maxDistanceray, layerMask))
		{
			height = hit.distance - 0.35f;
			ground = hit.collider.gameObject;
		}

	}

	Vector3 getInputAcceleration()
	{
		Vector3 inputVelocity = new Vector3();
		if (Input.GetKey(KeyCode.W))
		{
			inputVelocity += transform.forward * Speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.S))
		{
			inputVelocity += -transform.forward * Speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.A))
		{
			inputVelocity += -transform.right * Speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.D))
		{
			inputVelocity += transform.right * Speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.Space) && !jumping)
		{
			jumping = true;
			inputVelocity.y = 1.0f;
			jumpDecelerationTimer = 0;
		}



		return inputVelocity;

	}

	//void getVelocity()
	//{
	//	if (jumping)
	//	{
	//		timer += Time.deltaTime;
	//		if (timer < 0.5f)
	//		{
	//			Jump.y -= Time.deltaTime * 15.0f;
	//		}
	//		else
	//		{
	//			Jump.y -= Time.deltaTime * 10.0f;
	//		}
	//
	//		frameJumpVelocity = Jump * Time.deltaTime;
	//
	//		if (height < -frameJumpVelocity.y)
	//		{
	//			jumping = false;
	//			Vector3 pos = transform.position;
	//			pos.y -= height;
	//			transform.position = pos;
	//		}
	//		else
	//		{
	//			transform.Translate(frameJumpVelocity);
	//		}
	//	}
	//

	private void applyGravity()
	{
		if(jumping)
		{
			jumpDecelerationTimer += Time.deltaTime;
			if (jumpDecelerationTimer < 0.5f)
			{
				Velocity.y -= Time.deltaTime * 2.5f;
			}
			else
			{
				Velocity.y -= Time.deltaTime * 1.0f;
			}
		}
		else
		{
			Velocity.y -= Time.deltaTime * 1.0f;
		}
	}

	void applyVelocityDampening()
	{
		
		if (Velocity.magnitude > 0)
		{
			Velocity.z += -Velocity.z * Time.deltaTime * 2.0f;
			Velocity.x += -Velocity.x * Time.deltaTime * 2.0f;
			//if (!jumping && height > 0)
			//{
			//	Velocity.y += -0.5f * Time.deltaTime;
			//}
	
		}
	}
	//
	//void applyVelocity()
	//{
	//	if (Velocity.y > height)
	//	{
	//		Velocity.y = height;
	//		transform.Translate(Velocity);
	//		Velocity.y = 0;
	//	}
	//	else
	//	{
	//		transform.Translate(Velocity);
	//	}
	//}w

	private void checkForGrounded()
	{
		if(-Velocity.y > height)
		{
			jumping = false;
			Velocity.y = -height + 0.01f;
		}
	}

	private void FixedUpdate()
	{
		updateHeightFromGroundDetails();

		Velocity += getInputAcceleration();
		applyVelocityDampening();




		applyGravity();
		checkForGrounded();

		transform.Translate(Velocity);



		//Debug.Log(height);
		//
		//getInput(); // contains anything that checks for player input
		//
		//getVelocity(); // changes velocity values where necessary
		//
		//applyVelocity(); // applies all velocity
		
	}

	



	// Update is called once per frame
	void Update () {

	}
}
