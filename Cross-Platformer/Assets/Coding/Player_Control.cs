using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour {
	[Tooltip("This will affect how fast the player moves")]
	[Range(1, 100)]
	public float Speed = 25.0f;

	//private bool spaceHeld = false;
	private bool jumping = false;
	private float timer = 0;
	private const float timerIncrement = 0.001f;

	// Use this for initialization
	void Start() {

	}

	

	Vector3 Velocity = new Vector3(0, 0, 0);
	Vector3 Jump = new Vector3(0, 0, 0);

	// Update is called once per frame
	void Update () {
		Ray height = new Ray(transform.position, -transform.up);
		if (Input.GetKey(KeyCode.W))
		{
			Velocity += transform.forward * Speed * Time.deltaTime;
            Debug.Log("W Pressed");
		}
		
		if (Input.GetKey(KeyCode.S))
		{
			Velocity += -transform.forward * Speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.A))
		{
			Velocity += -transform.right * Speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.D))
		{
			Velocity += transform.right * Speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.Space) && !jumping)
		{
			//transform.Translate(transform.up * Speed);
			jumping = true;
			//spaceHeld = true;
			Jump.y = 0.5f;
		}

		if (jumping)
		{
			//if (!Input.GetKey(KeyCode.Space))
			//{
			//	spaceHeld = false;
			//}
			timer += Time.deltaTime;
			if (timer < 0.5f)
			{
				Jump.y -= Time.deltaTime * 1.4f;
			}
			else
			{
				Jump.y -= Time.deltaTime * 0.6f;
			}
			
			transform.Translate(Jump);

			
			if (transform.position.y < 0)
			{
				jumping = false;
				timer = 0.0f;
			}
		}

		if(Velocity.magnitude > 0)
		{
			Velocity.z += -Velocity.z * Time.deltaTime;
			Velocity.x += -Velocity.x * Time.deltaTime;
			//Velocity.y += -Velocity.y * 8 * Time.deltaTime;

			
		}
		transform.Translate(Velocity);

		if (transform.position.y < 0)
		{
			Vector3 temp = transform.position;
			temp.y = 0;
			transform.position = temp;
		}
	}
}
