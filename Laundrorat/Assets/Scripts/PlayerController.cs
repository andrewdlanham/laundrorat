using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private LayerMask trampolineMask;
    [SerializeField] private LayerMask trampolineExitPointMask;


    private float movementSpeed = 8f;

    private bool isGrounded;
    private bool isBouncing;

    private float horizontalInput;
    private Vector2 currentDirection;

    

    void Awake() {
        isGrounded= true;
        isBouncing = false;
    }

    void Update()
    {   
        updateHorizontalInput();
        updateCurrentDirection();


        if (isBouncing) {
            if (canExit()) {
                handleExit();
            }
            continueBouncing();
            
        }

        if (isGrounded) {
            if (isOverTrampoline()) {
                switchToBouncing();
            }
            handleHorizontalMovement();
        }
        
        
    }

    private void handleExit() {
        switchToGrounded();
        GameObject exitPointObject = getExitPointObject();
        transform.position = exitPointObject.transform.position;
    }

    private GameObject getExitPointObject() {
        Physics.Raycast(transform.position, currentDirection, out RaycastHit hit, 1f, trampolineExitPointMask);
        GameObject exitPointObject = hit.collider.gameObject;
        return exitPointObject;
    }

    private void switchToGrounded() {
        isGrounded = true;
        isBouncing = false;
    }

    private void switchToBouncing() {
        isBouncing = true;
        isGrounded = false;
    }

    private void updateHorizontalInput() {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void updateCurrentDirection() {
        currentDirection = new Vector2(horizontalInput, 0f);
    }

    private bool canExit() {
        
        if (Physics.Raycast(transform.position, currentDirection, out RaycastHit hit, 1f, trampolineExitPointMask)) {
            Debug.Log("Player can exit");
            return true;
        }

        Debug.Log("Player cannot exit");
        return false;
        
    }

    private void handleHorizontalMovement() {
        transform.Translate(currentDirection * movementSpeed * Time.deltaTime);
    }

    private bool isOverTrampoline() {
        if (Physics.Raycast(transform.position, Vector2.down, out RaycastHit hit, 1f, trampolineMask)) {
            Debug.Log("Player is over trampoline");
            return true;
        }
        return false;
    }

    private void continueBouncing() {
        Vector2 movementDirection = new Vector2(0f, 10f);
        transform.Translate(movementDirection * 0.5f * Time.deltaTime);
    }


}
