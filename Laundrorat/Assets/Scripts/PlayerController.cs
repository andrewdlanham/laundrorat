using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{

    [SerializeField] private LayerMask trampolineExitPointMask;

    [SerializeField] private LayerMask jumpPointMask;


    private float horizontalInput;

    void Awake() {
        this.isGrounded = true;
        this.isBouncing = false;
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
            if (CanExit()) {
                HandleExit();
            }
            ContinueBouncing();
        }

        if (IsGrounded()) {
            if (CharacterMovement.OverTrampoline(this.gameObject)) {
                SwitchToBouncing();
            }
            if (CanJump()) {
                HandleJump();
            }
            HandleHorizontalMovement();
        }
        
        
    }

    private void ReverseVerticalDirection() {
        if (this.currentVerticalDirection == Vector2.up) {
            this.currentVerticalDirection = Vector2.down;
        } else {
            this.currentVerticalDirection = Vector2.up;
        }
    }

    private void HandleExit() {
        SwitchToGrounded();
        GameObject exitPointObject = getExitPointObject();
        transform.position = exitPointObject.transform.position;
    }

    private void HandleJump() {
        SwitchToBouncing();
        this.currentVerticalDirection = Vector2.down;
        GameObject jumpPointObject = getJumpPointObject();
        transform.position = jumpPointObject.transform.position;

    }

    private GameObject getJumpPointObject() {
        Physics.Raycast(transform.position, this.currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask);
        GameObject jumpPointObject = hit.collider.gameObject;
        return jumpPointObject;
    }

    private GameObject getExitPointObject() {
        Physics.Raycast(transform.position, this.currentHorizontalDirection, out RaycastHit hit, 1f, trampolineExitPointMask);
        GameObject exitPointObject = hit.collider.gameObject;
        return exitPointObject;
    }

    
    private void UpdateHorizontalInput() {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void UpdateHorizontalDirection() {
        this.currentHorizontalDirection = new Vector2(horizontalInput, 0f);
    }

    private bool CanExit() {
        if (this.currentVerticalDirection == Vector2.down) return false;
        if (Physics.Raycast(transform.position, this.currentHorizontalDirection, out RaycastHit hit, 1f, trampolineExitPointMask)) {
            Debug.Log("Player can exit");
            return true;
        }

        //Debug.Log("Player cannot exit");
        return false;
        
    }

    private bool CanJump() {
        if (Physics.Raycast(transform.position, this.currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask)) {
            Debug.Log("Player can jump");
            return true;
        }
        return false;
    }

    

    

    


}
