using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{

    public float horizontalInput;
    public Collectable lastCollectable;

    public bool canDetectInput;

    void Awake() {
        SetComponentReferences();
        this.movementSpeed = 3f;
        this.bouncingSpeed = 5f;
        this.currentFloor = 0;
        this.currentTrampoline = null;
        SwitchToGrounded();
        jumpPointMask = 1 << LayerMask.NameToLayer("JumpPoint");
        EnablePlayerInput();
    }

    void Update()
    {   
        if (canDetectInput) {
            UpdateHorizontalInput();
        }
        
        UpdateHorizontalDirection();
        FlipSpriteIfNeeded();
        HandlePlayerMovement();
    }

    private void UpdateHorizontalInput() {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void UpdateHorizontalDirection() {
        if (horizontalInput > 0) {
            this.currentHorizontalDirection = Vector2.right;
        } else if (horizontalInput < 0) {
            this.currentHorizontalDirection = Vector2.left;
        } else {
            this.currentHorizontalDirection = new Vector2(0,0);
        }
    }

    private void HandlePlayerMovement() {
        if (IsBouncing()) {
            if (CharacterMovement.IsBouncingIntoTrampoline(this)) {
                HandleTrampolineBounce();
                ReverseVerticalDirection();
                return;
            } 
            if (CharacterMovement.IsBouncingIntoCeiling(this)) {
                ReverseVerticalDirection();
                return;
            }
            if (IsHoldingDirection()) {
                if (CharacterMovement.CanBounceOffWall(this)) {
                    Debug.Log("currentHorizontalDirection: " + currentHorizontalDirection);
                    Debug.Log("Bounced off wall");
                    this.currentVerticalDirection = Vector2.down;
                    return;
                }
                if (CharacterMovement.CanExitBouncing(this)) {
                    ExitBouncing();
                    return;
                }
            }
            
            ContinueBouncing();
        }

        if (IsGrounded()) {
            if (CharacterMovement.CanEnterBouncing(this)) {
                // TODO: Remove abilty to input for a little while
                EnterBouncing();
                StartCoroutine(TestRoutine());
                return;
            }
            HandleHorizontalMovement();
        }
    }

    void OnCollisionEnter2D (Collision2D collision) {
        GameObject.Find("GameManager").GetComponent<GameManager>().TriggerDeathSequence();
    }

    private void FlipSpriteIfNeeded() {
        if (horizontalInput > 0) {
            renderer.flipX = false;
        } else if (horizontalInput < 0) {
            renderer.flipX = true;
        }
    }

    private bool IsHoldingDirection() {
        return horizontalInput != 0;
    }

    IEnumerator TestRoutine() {
        DisablePlayerInput();
        horizontalInput = 0;
        currentHorizontalDirection = new Vector2(0,0);
        Debug.Log("TestRoutine");
        yield return new WaitForSeconds(0.3f);
        EnablePlayerInput();
    }

    private void HandleTrampolineBounce() {
        Trampoline trampoline = CharacterMovement.GetTrampoline(this);
        trampoline.RegisterBounce();
        currentTrampoline = trampoline;
    }

    private void EnablePlayerInput() {
        canDetectInput = true;
    }

    private void DisablePlayerInput() {
        canDetectInput = false;
    }

}
