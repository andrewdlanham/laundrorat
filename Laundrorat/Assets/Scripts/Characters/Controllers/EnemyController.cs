using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{

    private PlayerController player;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        SwitchToGrounded();
        this.movementSpeed = 2f;
        this.bouncingSpeed = 5f;
        this.currentHorizontalDirection = Vector2.left;
        this.currentTrampoline = null;
        this.animator = GetComponent<Animator>();
    }

    void Update() {
        HandleEnemyMovement();
    }

    private void HandleEnemyMovement() {
        if (IsBouncing()) {
            if (CharacterMovement.ShouldReverseVerticalDirection(this)) {
                ReverseVerticalDirection();
            }

            if (PlayerIsToRight()) {
                this.currentHorizontalDirection = Vector2.right;
            } else {
                this.currentHorizontalDirection = Vector2.left;
            }

            if (EnemyShouldExit()) {
                ExitBouncing();
            }
            ContinueBouncing();
        }

        if (IsGrounded()) {
            if (CharacterMovement.CanEnterBouncing(this)) {
                EnterBouncing();
            }
            HandleHorizontalMovement();
        }
    }

    private bool EnemyShouldExit() {
        if (CharacterMovement.CanExitBouncing(this)) {
            GameObject exitPointObject = CharacterMovement.GetExitPointObject(this);
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
        // TODO: Calculate random exit floor for other cases
        return 0;
    }

    private bool PlayerIsToRight() {
        return player.gameObject.transform.position.x > this.gameObject.transform.position.x;
    }


}
