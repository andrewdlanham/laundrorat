using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private LayerMask trampolineMask;
    
    [SerializeField] private LayerMask ceilingMask;

    [SerializeField] private LayerMask trampolineExitPointMask;

    [SerializeField] private LayerMask jumpPointMask;


    private float movementSpeed;
    private float bouncingSpeed;

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isBouncing;

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
    }

    void Update()
    {   
        updateHorizontalInput();
        updatecurrentHorizontalDirection();


        if (isBouncing) {
            if (currentVerticalDirection == Vector2.up && isUnderCeiling() || (currentVerticalDirection == Vector2.down && isOverTrampoline())) {
                reverseVerticalDirection();
            }
            if (canExit()) {
                handleExit();
            }
            continueBouncing();
        }

        if (isGrounded) {
            if (isOverTrampoline()) {
                switchToBouncing();
            }
            if (canJump()) {
                handleJump();
            }
            handleHorizontalMovement();
        }
        
        
    }

    private Vector2 getBottomOfPlayer() {
        return (Vector2) transform.position - new Vector2(0f, playerCollider.bounds.extents.y);
    }

    private Vector2 getTopOfPlayer() {
        return (Vector2) transform.position + new Vector2(0f, playerCollider.bounds.extents.y);
    }

    private void reverseVerticalDirection() {
        if (currentVerticalDirection == Vector2.up) {
            currentVerticalDirection = Vector2.down;
        } else {
            currentVerticalDirection = Vector2.up;
        }
    }

    private bool isUnderCeiling() {
        if (Physics.Raycast(getTopOfPlayer(), Vector2.up, out RaycastHit hit, 0.2f, ceilingMask)) {
            Debug.Log("Player is under ceiling");
            return true;
        }
        return false;
    }

    private void handleExit() {
        switchToGrounded();
        GameObject exitPointObject = getExitPointObject();
        transform.position = exitPointObject.transform.position;
    }

    private void handleJump() {
        switchToBouncing();
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

    private void switchToGrounded() {
        isGrounded = true;
        isBouncing = false;
    }

    private void switchToBouncing() {
        currentVerticalDirection = Vector2.up;
        isBouncing = true;
        isGrounded = false;
    }

    private void updateHorizontalInput() {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void updatecurrentHorizontalDirection() {
        currentHorizontalDirection = new Vector2(horizontalInput, 0f);
    }

    private bool canExit() {
        if (currentVerticalDirection == Vector2.down) return false;
        if (Physics.Raycast(transform.position, currentHorizontalDirection, out RaycastHit hit, 1f, trampolineExitPointMask)) {
            Debug.Log("Player can exit");
            return true;
        }

        //Debug.Log("Player cannot exit");
        return false;
        
    }

    private bool canJump() {
        if (Physics.Raycast(transform.position, currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask)) {
            Debug.Log("Player can jump");
            return true;
        }
        return false;
    }

    private void handleHorizontalMovement() {
        transform.Translate(currentHorizontalDirection * movementSpeed * Time.deltaTime);
    }

    private bool isOverTrampoline() {
        if (Physics.Raycast(getBottomOfPlayer(), Vector2.down, out RaycastHit hit, 0.2f, trampolineMask)) {
            Debug.Log("Player is over trampoline");
            return true;
        }
        return false;
    }

    private void continueBouncing() {
        transform.Translate(currentVerticalDirection * bouncingSpeed * Time.deltaTime);
    }


}
