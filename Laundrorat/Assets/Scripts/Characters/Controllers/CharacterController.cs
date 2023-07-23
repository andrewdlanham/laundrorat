using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float movementSpeed;
    public float bouncingSpeed;

    public bool isGrounded;
    public bool isBouncing;

    public Vector2 currentHorizontalDirection;
    public Vector2 currentVerticalDirection;

    public int currentFloor;


    protected bool IsGrounded() {
        return this.isGrounded;
    }

    protected bool IsBouncing() {
        return this.isBouncing;
    }

    protected void SwitchToGrounded() {
        this.isGrounded = true;
        this.isBouncing = false;
    }

    protected void SwitchToBouncing() {
        this.currentVerticalDirection = Vector2.up;
        this.isGrounded = false;
        this.isBouncing = true;
    }

    protected void EnterBouncing() {
        SwitchToBouncing();
        this.currentVerticalDirection = Vector2.down;
        GameObject jumpPointObject = CharacterMovement.GetJumpPointObject(this);
        transform.position = jumpPointObject.transform.position;
    }

    protected void ExitBouncing() {
        SwitchToGrounded();
        GameObject exitPointObject = CharacterMovement.GetExitPointObject(this);
        transform.position = exitPointObject.transform.position;
    }

    protected void ContinueBouncing() {
        this.gameObject.transform.Translate(this.currentVerticalDirection * this.bouncingSpeed * Time.deltaTime);
    }

    protected void HandleHorizontalMovement() {
        this.gameObject.transform.Translate(this.currentHorizontalDirection * this.movementSpeed * Time.deltaTime);
    }

    protected void ReverseVerticalDirection() {
        if (this.currentVerticalDirection == Vector2.up) {
            this.currentVerticalDirection = Vector2.down;
        } else {
            this.currentVerticalDirection = Vector2.up;
        }
    }

}
