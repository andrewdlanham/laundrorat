using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{

    private float horizontalInput;

    void Awake() {
        SwitchToGrounded();
        this.movementSpeed = 8f;
        this.bouncingSpeed = 5f;
    }

    void Update()
    {   
        UpdateHorizontalInput();
        UpdateHorizontalDirection();

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

    private void UpdateHorizontalInput() {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void UpdateHorizontalDirection() {
        this.currentHorizontalDirection = new Vector2(horizontalInput, 0f);
    }

}
