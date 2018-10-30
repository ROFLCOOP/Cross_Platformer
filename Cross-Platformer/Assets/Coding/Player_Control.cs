using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour {
	[Tooltip("This will affect how fast the player moves")]
	[Range(1, 100)]
	public float Speed = 25.0f;

	private bool spaceHeld = false;
	private float timer = 0;
	private const float timerIncrement = 0.001f;

	// Use this for initialization
	void Start() {

	}

	Vector3 Velocity = new Vector3(0, 0, 0);
	Vector3 Jump = new Vector3(0, 0, 0);

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W))
		{
			Velocity += transform.forward * Speed * Time.deltaTime;
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

		if (Input.GetKey(KeyCode.Space) && !spaceHeld)
		{
			//transform.Translate(transform.up * Speed);
			spaceHeld = true;
			Jump.y = 1;
		}

		if(spaceHeld || timer > 0.0f)
		{
			if (!Input.GetKey(KeyCode.Space))
			{
				spaceHeld = false;
			}
			timer += Time.deltaTime * timerIncrement;
			if(timer < 1.0f && spaceHeld)
			{
				Jump.y -= 0.5f * Time.deltaTime;
			}
			else
			{
				timer = 0.0f;
				Velocity.y = 0;
			}
			transform.Translate(Jump);
		}

		if(Velocity.x != 0 || Velocity.y != 0 || Velocity.z != 0)
		{
			Velocity.z += -Velocity.z * Time.deltaTime;
			Velocity.x += -Velocity.x * Time.deltaTime;
			Velocity.y += -Velocity.y * 8 * Time.deltaTime;
		}
		transform.Translate(Velocity);
	}
}
