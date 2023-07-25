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
    }

    void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        if (IsBouncing()) {
            if (CharacterMovement.ShouldReverseVerticalDirection(this)) {
                ReverseVerticalDirection();
            }

            if (PlayerIsToRight()) {
                this.currentHorizontalDirection = Vector2.right;
            } else {
                this.currentHorizontalDirection = Vector2.left;
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

    private bool EnemyShouldExit() {
        GameObject exitPointObject = CharacterMovement.GetExitPointObject(this);
        int exitFloor = exitPointObject.GetComponent<ExitPoint>().floorNumber;
        return exitFloor == player.currentFloor;
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
