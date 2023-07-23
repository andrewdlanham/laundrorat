using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{

    private float horizontalInput;

    void Awake() {
        SwitchToGrounded();
        this.movementSpeed = 3f;
        this.bouncingSpeed = 5f;
        this.currentFloor = 0;
    }

    void Update()
    {   
        UpdateHorizontalInput();
        UpdateHorizontalDirection();
        HandleMovement();
    }

    private void UpdateHorizontalInput() {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void UpdateHorizontalDirection() {
        this.currentHorizontalDirection = new Vector2(horizontalInput, 0f);
    }

    private void HandleMovement() {
        if (IsBouncing()) {
            if (CharacterMovement.ShouldReverseVerticalDirection(this)) {
                ReverseVerticalDirection();
            }
            if (CharacterMovement.CanExitBouncing(this)) {
                ExitBouncing();
            }
            ContinueBouncing();
        }

        if (IsGrounded()) {
            if (CharacterMovement.IsOverTrampoline(this.gameObject)) {
                SwitchToBouncing();
            }
            if (CharacterMovement.CanEnterBouncing(this)) {
                EnterBouncing();
            }
            HandleHorizontalMovement();
        }
    }

    void OnCollisionEnter2D (Collision2D collision) {
        Debug.Log("Player collided with enemy!");
        GameObject.Find("GameManager").GetComponent<GameManager>().TriggerDeathSequence();
    }

}
