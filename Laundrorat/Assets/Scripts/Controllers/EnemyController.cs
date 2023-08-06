using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{

    private PlayerController player;

    void Awake() {
        SetComponentReferences();
        SetLayerMasks();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        
        //this.movementSpeed = 1.5f;
        //this.bouncingSpeed = 3f;
        this.currentHorizontalDirection = Vector2.left;
        this.currentTrampoline = null;
        SwitchToGrounded();
        
    }


    void Update() {
        HandleEnemyMovement();
        FlipSpriteIfNeeded();
    }

    private void HandleEnemyMovement() {

        if (IsJumping()) {
            return;
        }

        if (IsBouncing()) {
            if (IsBouncingIntoTrampoline() || IsBouncingIntoCeiling()) {
                ReverseVerticalDirection();
                return;
            }

            if (PlayerIsToRight()) {
                this.currentHorizontalDirection = Vector2.right;
            } else {
                this.currentHorizontalDirection = Vector2.left;
            }

            if (EnemyShouldExit()) {
                ExitBouncing();
                return;
            }
            ContinueBouncing();
        }

        if (IsGrounded()) {
            if (CanEnterBouncing()) {
                EnterBouncing();
                return;
            }
            HandleHorizontalMovement();
        }
    }

    private bool EnemyShouldExit() {
        if (CanExitBouncing()) {
            GameObject exitPointObject = GetExitPointObject();
            if (exitPointObject == null) return false;
            int exitFloor = exitPointObject.GetComponent<ExitPoint>().floorNumber;
            return exitFloor == GetExitFloor();
        } else {
            return false;
        }
        
    }

    private int GetExitFloor() {
        if (player.isGrounded) {
            return player.currentFloor;
        }
        // TODO: Add more rules for calculating exit floor
        return 0;
    }

    private bool PlayerIsToRight() {
        return player.gameObject.transform.position.x > this.gameObject.transform.position.x;
    }


}
