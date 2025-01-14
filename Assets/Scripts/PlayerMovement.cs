﻿using UnityEngine;

[
    RequireComponent(typeof(RotationFollowMouse)),
    RequireComponent(typeof(Rigidbody)),
    RequireComponent(typeof(Animator))
]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D playerRigidBody;
    private Vector3 change;
    private Animator playerAnimator;
    private RotationFollowMouse playerRotator;
    private Vector2 mouseDelta;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        mouseDelta = GetComponent<RotationFollowMouse>().mouseDelta;
        HandlePlayerMovement();
        RotatePlayer();
    }

    private void HandlePlayerMovement()
    {
        UpdatePlayerAxis();

        if (IsMovementChange())
        {
            MoveCharacter();
        }
        else { StopPlayerMovement(); }
    }

    private void ResetPlayerVector()
    {
        change = Vector3.zero;
    }
    private void UpdatePlayerAxis()
    {
        ResetPlayerVector();
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
    }

    private void MoveCharacter()
    {
        playerRigidBody.MovePosition(
            transform.position + change * movementSpeed * Time.deltaTime
        );

        StartPlayerMovement();
    }

    private void StopPlayerMovement()
    {
        playerAnimator.SetBool("moving", false);
    }

    private void StartPlayerMovement()
    {
        playerAnimator.SetBool("moving", true);
        playerAnimator.SetFloat("moveX", change.x);
        playerAnimator.SetFloat("moveY", change.y);
    }

    private void RotatePlayer()
    {
        if (!playerAnimator.GetBool("moving"))
        {
            playerAnimator.SetFloat("moveX", mouseDelta.x);
            playerAnimator.SetFloat("moveY", mouseDelta.y);
        }
    }

    private bool IsMovementChange()
    {
        return change != Vector3.zero;
    }
}