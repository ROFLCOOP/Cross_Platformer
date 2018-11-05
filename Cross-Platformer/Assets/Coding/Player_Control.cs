﻿using System;
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
	private bool CollisionInFront = false;

	// Use this for initialization
	void Start() {
	}


	Vector3 frameJumpVelocity = new Vector3(0, 0, 0);
	Vector3 Velocity = new Vector3(0, 0, 0);
	Vector3 Jump = new Vector3(0, 0, 0);

	GameObject ground;
	float radius = 0.5f;


	void updateCollisionDetails ()
	{
		RaycastHit hit;
		int layerMask = 1 << 8;
		layerMask = ~layerMask;
		if (Physics.Raycast(transform.position + new Vector3(0, -1.0f, 0), -transform.up, out hit, maxDistanceray, layerMask))
		{
			height = hit.distance;
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

		// create rays here for frontal colission detection

		return inputVelocity;

	}
	

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
	
		}
	}

	private void checkForGrounded()
	{
		if(-Velocity.y > height)
		{
			jumping = false;
			Velocity.y = -height + 0.01f;
		}
	}

	bool ObjectIsInFront()
	{
		RaycastHit ForwardMid;
		RaycastHit ForwardLeft;
		RaycastHit ForwardRight;

		int layerMask = 1 << 8;
		layerMask = ~layerMask;

		Vector3 tempVelocity = Velocity; // a version of velocity that has the Y value set to 0.
		tempVelocity.y = 0;

		Vector3 midOrigin = transform.position + tempVelocity.normalized * radius;
		Vector3 leftOrigin = midOrigin + (Vector3.Cross(tempVelocity.normalized, transform.up) * radius);
		Vector3 rightOrigin = midOrigin + (Vector3.Cross(transform.up, tempVelocity.normalized) * radius);

		if (Physics.Raycast(midOrigin, tempVelocity.normalized, out ForwardMid, maxDistanceray, layerMask))
		{
			if(ForwardMid.distance < (transform.position + Velocity).)
			{
				return true;
			}
		}

		if (Physics.Raycast(leftOrigin, tempVelocity.normalized, out ForwardLeft, maxDistanceray, layerMask))
		{
			if (ForwardLeft.distance < Velocity.magnitude)
			{
				return true;
			}
		}

		if (Physics.Raycast(rightOrigin, tempVelocity.normalized, out ForwardRight, maxDistanceray, layerMask))
		{
			if (ForwardRight.distance < Velocity.magnitude)
			{
				return true;
			}
		}

		return false;
	}

	private void FixedUpdate()
	{
		updateCollisionDetails();

		Velocity += getInputAcceleration();
		applyVelocityDampening();




		applyGravity();
		checkForGrounded();

		CollisionInFront = ObjectIsInFront(); // this function should cast 3 rays in the direction the player is headed to see if the player is about to collide with anything

		if(CollisionInFront)
		{
			Velocity -= new Vector3(0, Velocity.y, 0);
		}

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
