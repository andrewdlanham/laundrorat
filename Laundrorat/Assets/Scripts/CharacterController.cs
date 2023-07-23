using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float movementSpeed;
    public float bouncingSpeed;

    protected bool isGrounded;
    protected bool isBouncing;

    public Vector2 currentHorizontalDirection;
    public Vector2 currentVerticalDirection;

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
        this.gameObject.transform.Translate(this.currentVerticalDirection * this.bouncingSpeed * Time.deltaTime);
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





    

    

}
