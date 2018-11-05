using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour {
	[Tooltip("This will affect how fast the player moves")]
	[Range(1, 100)]
	public float Speed = 25.0f;

	// Use this for initialization
	void Start() {

	}

	Vector3 Velocity = new Vector3(0, 0, 0);

	// Update is called once per frame
	void Update () {
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

		if (Input.GetKey(KeyCode.Space))
		{
			transform.Translate(transform.up * Speed);
		}

		if(Velocity.x != 0 || Velocity.y != 0 || Velocity.z != 0)
		{
			Velocity.z += -Velocity.z * Time.deltaTime;
			Velocity.x += -Velocity.x * Time.deltaTime;
			Velocity.y += -Velocity.y * Time.deltaTime;
		}
		transform.Translate(Velocity);
	}
}
