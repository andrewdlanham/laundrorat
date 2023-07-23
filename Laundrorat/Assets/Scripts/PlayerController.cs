using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{

    [SerializeField] private LayerMask trampolineExitPointMask;

    [SerializeField] private LayerMask jumpPointMask;


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
            if (this.currentVerticalDirection == Vector2.up && CharacterMovement.IsUnderCeiling(this.gameObject) || (this.currentVerticalDirection == Vector2.down && CharacterMovement.OverTrampoline(this.gameObject))) {
                ReverseVerticalDirection();
            }
            if (CanExitBouncing()) {
                ExitBouncing();
            }
            ContinueBouncing();
        }

        if (IsGrounded()) {
            if (CharacterMovement.OverTrampoline(this.gameObject)) {
                SwitchToBouncing();
            }
            if (CanEnterBouncing()) {
                EnterBouncing();
            }
            HandleHorizontalMovement();
        }
        
        
    }

    private void HandleHorizontalMovement() {
        this.gameObject.transform.Translate(this.currentHorizontalDirection * this.bouncingSpeed * Time.deltaTime);
    }

    private void ReverseVerticalDirection() {
        if (this.currentVerticalDirection == Vector2.up) {
            this.currentVerticalDirection = Vector2.down;
        } else {
            this.currentVerticalDirection = Vector2.up;
        }
    }
    
    private void UpdateHorizontalInput() {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void UpdateHorizontalDirection() {
        this.currentHorizontalDirection = new Vector2(horizontalInput, 0f);
    }

    private bool CanExitBouncing() {
        if (this.currentVerticalDirection == Vector2.down) return false;
        if (Physics.Raycast(transform.position, this.currentHorizontalDirection, out RaycastHit hit, 1f, trampolineExitPointMask)) {
            Debug.Log("Player can exit");
            return true;
        }

        //Debug.Log("Player cannot exit");
        return false;
        
    }

    private bool CanEnterBouncing() {
        if (Physics.Raycast(transform.position, this.currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask)) {
            Debug.Log("Player can jump");
            return true;
        }
        return false;
    }

    

    

    


}
