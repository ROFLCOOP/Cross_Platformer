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
	private float distanceToObject;
	private bool CollisionInFront = false;

    PS4_Controller.InputPacket InputDataStream = new PS4_Controller.InputPacket(0,0,0,0,false);

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

        float LS_Horizontal = InputDataStream.LS_Horiz;
        inputVelocity += transform.right * Speed * Time.deltaTime * LS_Horizontal;

        float LS_Vertical = InputDataStream.LS_Vert;
        inputVelocity += transform.forward * Speed * Time.deltaTime * LS_Vertical;

        //float RS_Horizontal = InputDataStream.RS_Horiz;

        

        if (InputDataStream.btn_jump && !jumping)
        {
            jumping = true;
            inputVelocity.y = 1.0f;
            jumpDecelerationTimer = 0;
        }

        //if (Input.GetKey(KeyCode.W))
        //{
        //	inputVelocity += transform.forward * Speed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //	inputVelocity += -transform.forward * Speed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //	inputVelocity += -transform.right * Speed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //	inputVelocity += transform.right * Speed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.Space) && !jumping)
        //{
        //	jumping = true;
        //	inputVelocity.y = 1.0f;
        //	jumpDecelerationTimer = 0;
        //}

        // create rays here for frontal colission detection

        return inputVelocity;

	}

	private void OnDrawGizmos()
	{

		Vector3 tempVelocity = Velocity; // a version of velocity that has the Y value set to 0.
		tempVelocity.y = 0;

		Vector3 rayOffset = tempVelocity.normalized * radius;
		Quaternion leftOffsetRotate = Quaternion.AngleAxis(45, new Vector3(0, 1, 0));
		Quaternion RightOffsetRotate = Quaternion.AngleAxis(-45, new Vector3(0, 1, 0));
		Vector3 midOrigin = transform.position + rayOffset;
		Vector3 leftOrigin = transform.position + (leftOffsetRotate * rayOffset);
		Vector3 rightOrigin = transform.position + (RightOffsetRotate * rayOffset);

		Gizmos.DrawLine(midOrigin, midOrigin + tempVelocity);
		Gizmos.DrawLine(leftOrigin, leftOrigin + tempVelocity);
		Gizmos.DrawLine(rightOrigin, rightOrigin + tempVelocity);
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

		Vector3 rayOffset = tempVelocity.normalized * radius;
		Quaternion leftOffsetRotate = Quaternion.AngleAxis(45, new Vector3(0, 1, 0));
		Quaternion RightOffsetRotate = Quaternion.AngleAxis(-45, new Vector3(0, 1, 0));
		Vector3 midOrigin = transform.position + rayOffset;
		Vector3 leftOrigin = transform.position + (leftOffsetRotate * rayOffset);
		Vector3 rightOrigin = transform.position + (RightOffsetRotate * rayOffset);


		for (float offset = -0.75f; offset <= 0.75f; offset += 0.75f)

		{
			if (Physics.Raycast(midOrigin + new Vector3(0, offset, 0), tempVelocity.normalized, out ForwardMid, maxDistanceray, layerMask))
			{
				if (ForwardMid.distance < Velocity.magnitude)
				{
					distanceToObject = ForwardMid.distance;
					return true;
				}
			}

			if (Physics.Raycast(leftOrigin, tempVelocity.normalized, out ForwardLeft, maxDistanceray, layerMask))
			{
				if (ForwardLeft.distance < Velocity.magnitude)
				{
					distanceToObject = ForwardLeft.distance;
					return true;
				}
			}

			if (Physics.Raycast(rightOrigin, tempVelocity.normalized, out ForwardRight, maxDistanceray, layerMask))
			{
				if (ForwardRight.distance < Velocity.magnitude)
				{
					distanceToObject = ForwardLeft.distance;
					return true;
				}
			}
		}

		return false;
	}

	void CheckForHorizontalCollision()
	{
		CollisionInFront = ObjectIsInFront();

		if (CollisionInFront)
		{
			//Vector3 tempVelocity = Velocity;
			//Velocity.x = 0;
			//Velocity.z = 0;
			//tempVelocity.y = 0;
			//tempVelocity *= distanceToObject;
			//Velocity += tempVelocity;

			Velocity.x = 0;
			Velocity.z = 0;
		}
	}

    void processInput(PS4_Controller.InputPacket ctrl_arr)
    {
        InputDataStream = ctrl_arr;
    }
	private void FixedUpdate()
	{
		updateCollisionDetails();

		Velocity += getInputAcceleration();
		applyVelocityDampening();




		applyGravity();
		checkForGrounded();

		// this function should cast 3 rays in the direction the player is headed to see if the player is about to collide with anything
		CheckForHorizontalCollision();

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
