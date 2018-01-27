using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour {


    public Animator animator;
	private Vector2 movementVector;
	
	// Update is called once per frame
	void Update () {

		movementVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		movementVector = movementVector.normalized;

		transform.eulerAngles = Vector3.up * Mathf.Atan2 (movementVector.x, movementVector.y) * Mathf.Rad2Deg;
		transform.Translate(transform.forward * 50 * Time.deltaTime, Space.World);

	}
}
