using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed;
	private Rigidbody2D playerRigidBody;
	private Vector3 change;

	// Use this for initialization
	void Start () {
		playerRigidBody = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		HandlePlayerMovement ();
		Debug.Log (change);
	}

	private void HandlePlayerMovement () {
		// ResetPlayerVector();
		UpdatePlayerAxis ();

		if (IsMovementChange ()) {
			MoveCharacter ();
		}
	}

	private void ResetPlayerVector () {
		change = Vector3.zero;
	}
	private void UpdatePlayerAxis () {
		ResetPlayerVector();
		change.x = Input.GetAxisRaw ("Horizontal");
		change.y = Input.GetAxisRaw ("Vertical");
	}

	private void MoveCharacter () {
		playerRigidBody.MovePosition (
			transform.position + change * movementSpeed * Time.deltaTime
		);
	}

	private bool IsMovementChange () {
		return change != Vector3.zero;
	}
}
