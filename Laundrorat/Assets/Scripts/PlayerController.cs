using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private CharacterMovement characterMovement;

    [SerializeField] private LayerMask trampolineMask;

    

    [SerializeField] private LayerMask trampolineExitPointMask;

    [SerializeField] private LayerMask jumpPointMask;


    private float movementSpeed;
    private float bouncingSpeed;

    private bool isGrounded;
    private bool isBouncing;

    private float horizontalInput;
    [SerializeField] private Vector2 currentHorizontalDirection; //TODO: Remove SField after testing
    [SerializeField] private Vector2 currentVerticalDirection;  //TODO: Remove SField after testing

    private BoxCollider2D playerCollider;
    

    void Awake() {
        isGrounded= true;
        isBouncing = false;
        movementSpeed = 8f;
        bouncingSpeed = 5f;
        playerCollider = GetComponent<BoxCollider2D>();
        characterMovement = GameObject.Find("CharacterMovement").GetComponent<CharacterMovement>();
    }

    void Update()
    {   
        UpdateHorizontalInput();
        UpdateHorizontalDirection();


        if (isBouncing) {
            if (currentVerticalDirection == Vector2.up && characterMovement.IsUnderCeiling(this.gameObject) || (currentVerticalDirection == Vector2.down && OverTrampoline())) {
                ReverseVerticalDirection();
            }
            if (CanExit()) {
                HandleExit();
            }
            ContinueBouncing();
        }

        if (isGrounded) {
            if (OverTrampoline()) {
                SwitchToBouncing();
            }
            if (CanJump()) {
                handleJump();
            }
            HandleHorizontalMovement();
        }
        
        
    }

    private Vector2 GetBottomOfCharacter() {
        return (Vector2) transform.position - new Vector2(0f, playerCollider.bounds.extents.y);
    }

    

    private void ReverseVerticalDirection() {
        if (currentVerticalDirection == Vector2.up) {
            currentVerticalDirection = Vector2.down;
        } else {
            currentVerticalDirection = Vector2.up;
        }
    }

    

    private void HandleExit() {
        SwitchToGrounded();
        GameObject exitPointObject = getExitPointObject();
        transform.position = exitPointObject.transform.position;
    }

    private void handleJump() {
        SwitchToBouncing();
        currentVerticalDirection = Vector2.down;
        GameObject jumpPointObject = getJumpPointObject();
        transform.position = jumpPointObject.transform.position;

    }

    private GameObject getJumpPointObject() {
        Physics.Raycast(transform.position, currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask);
        GameObject jumpPointObject = hit.collider.gameObject;
        return jumpPointObject;
    }

    private GameObject getExitPointObject() {
        Physics.Raycast(transform.position, currentHorizontalDirection, out RaycastHit hit, 1f, trampolineExitPointMask);
        GameObject exitPointObject = hit.collider.gameObject;
        return exitPointObject;
    }

    private void SwitchToGrounded() {
        isGrounded = true;
        isBouncing = false;
    }

    private void SwitchToBouncing() {
        currentVerticalDirection = Vector2.up;
        isBouncing = true;
        isGrounded = false;
    }

    private void UpdateHorizontalInput() {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void UpdateHorizontalDirection() {
        currentHorizontalDirection = new Vector2(horizontalInput, 0f);
    }

    private bool CanExit() {
        if (currentVerticalDirection == Vector2.down) return false;
        if (Physics.Raycast(transform.position, currentHorizontalDirection, out RaycastHit hit, 1f, trampolineExitPointMask)) {
            Debug.Log("Player can exit");
            return true;
        }

        //Debug.Log("Player cannot exit");
        return false;
        
    }

    private bool CanJump() {
        if (Physics.Raycast(transform.position, currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask)) {
            Debug.Log("Player can jump");
            return true;
        }
        return false;
    }

    private void HandleHorizontalMovement() {
        transform.Translate(currentHorizontalDirection * movementSpeed * Time.deltaTime);
    }

    private bool OverTrampoline() {
        if (Physics.Raycast(GetBottomOfCharacter(), Vector2.down, out RaycastHit hit, 0.2f, trampolineMask)) {
            Debug.Log("Player is over trampoline");
            return true;
        }
        return false;
    }

    private void ContinueBouncing() {
        transform.Translate(currentVerticalDirection * bouncingSpeed * Time.deltaTime);
    }


}
