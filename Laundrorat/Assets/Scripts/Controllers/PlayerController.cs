using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{

    
    public static PlayerController instance;
    
    public float horizontalInput;
    public Collectable lastCollectable;

    public bool canDetectInput;

    void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        SetComponentReferences();
        SetLayerMasks();
        this.movementSpeed = 2f;
        this.bouncingSpeed = 3f;
        this.currentFloor = 0;
        this.currentTrampoline = null;
        SwitchToGrounded();
        
        EnablePlayerInput();
    }

    void Update()
    {   
        if (canDetectInput && !IsJumping()) {
            UpdateInput();
        }
        
        UpdateHorizontalDirection();
        FlipSpriteIfNeeded();
        HandlePlayerMovement();
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

        if (IsJumping()) {
            return;
        }

        if (IsBouncing()) {
            if(IsBouncingIntoFloor()) {
                KillPlayer();
                return;
            }
            if (IsBouncingIntoTrampoline()) {
                HandleTrampolineBounce();
                ReverseVerticalDirection();
                return;
            } 
            if (IsBouncingIntoCeiling()) {
                ReverseVerticalDirection();
                return;
            }
            if (IsHoldingDirection()) {
                /*
                if (CanBounceOffWall()) {
                    Debug.Log("currentHorizontalDirection: " + currentHorizontalDirection);
                    Debug.Log("Bounced off wall");
                    this.currentVerticalDirection = Vector2.down;
                    return;
                }
                */
                if (CanExitBouncing()) {
                    ExitBouncing();
                    return;
                }
            }
            
            ContinueBouncing();
        }

        if (IsGrounded()) {
            if (IsHoldingDirection()) {
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsIdle", false);
            } else {
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsIdle", true);
            }
            if (CanEnterBouncing()) {
                EnterBouncing();
                //StartCoroutine(JumpInputBuffer());
                return;
            }
            HandleHorizontalMovement();
        }
    }

    void OnCollisionEnter2D (Collision2D collision) {
        KillPlayer();
    }

    private void KillPlayer() {
        Debug.Log("KillPlayer()");
        GameObject.Find("GameManager").GetComponent<GameManager>().TriggerDeathSequence();
    }

    private bool IsHoldingDirection() {
        return horizontalInput != 0;
    }

    
    private void HandleTrampolineBounce() {
        Trampoline trampoline = GetTrampoline();
        trampoline.RegisterBounce();
        currentTrampoline = trampoline;
    }

    

    private bool CanBounceOffWall() {
        if (this.currentVerticalDirection == Vector2.down) return false;
        if (this.horizontalInput == 0 ) {
            Debug.Log("Cannot bounce without input");
            return false;
        }
        return Physics.Raycast(this.gameObject.transform.position, this.currentHorizontalDirection, out RaycastHit hit, 2.5f, wallMask);
    }

    #region Player Input
    private void UpdateInput() {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void EnablePlayerInput() {
        canDetectInput = true;
    }

    private void DisablePlayerInput() {
        canDetectInput = false;
    }

    IEnumerator JumpInputBuffer() {
        DisablePlayerInput();
        horizontalInput = 0;
        currentHorizontalDirection = new Vector2(0,0);
        Debug.Log("JumpInputBuffer()");
        yield return new WaitForSeconds(0.3f);
        EnablePlayerInput();
    }


    #endregion

    public void ResetPlayerPosition() {
        transform.position = new Vector3(12.75f, 1.35f, 0f);
        SwitchToGrounded();
    }
}
