using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected float movementSpeed;
    protected float bouncingSpeed;

    protected bool isGrounded;
    protected bool isBouncing;

    protected Vector2 currentHorizontalDirection;
    protected Vector2 currentVerticalDirection;

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
        this.isBouncing = true;
        this.isGrounded = false;
    }

    protected void ContinueBouncing() {
        this.transform.Translate(this.currentVerticalDirection * this.bouncingSpeed * Time.deltaTime);
    }

    protected void HandleHorizontalMovement() {
        transform.Translate(this.currentHorizontalDirection * this.movementSpeed * Time.deltaTime);
    }

}
