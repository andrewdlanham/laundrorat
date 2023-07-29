using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public LayerMask jumpPointMask;
    
    public float movementSpeed;
    public float bouncingSpeed;

    public bool isGrounded;
    public bool isBouncing;

    public Vector2 currentHorizontalDirection;
    public Vector2 currentVerticalDirection;

    public int currentFloor;

    public Trampoline currentTrampoline;

    public Animator animator;

    public SpriteRenderer renderer;

    protected bool IsGrounded() {
        return this.isGrounded;
    }

    protected bool IsBouncing() {
        return this.isBouncing;
    }

    protected void SwitchToGrounded() {
        animator.SetBool("IsBouncing", false);
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.isGrounded = true;
        this.isBouncing = false;
    }

    protected void SwitchToBouncing() {
        Debug.Log("SwitchToBouncing()");
        //StartCoroutine(PlayJumpAnimation());
        animator.SetBool("IsBouncing", true);
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.isGrounded = false;
        this.isBouncing = true;
    }

    protected void EnterBouncing() {
        Debug.Log("EnterBouncing()");
        SwitchToBouncing();
        GameObject jumpPointObject = GetJumpPointObject();
        transform.position = jumpPointObject.transform.position;
        this.currentVerticalDirection = Vector2.down;
    }

    protected void ExitBouncing() {
        SwitchToGrounded();
        GameObject exitPointObject = CharacterMovement.GetExitPointObject(this);
        transform.position = exitPointObject.transform.position;
        int exitFloor = exitPointObject.GetComponent<ExitPoint>().floorNumber;
        this.currentFloor = exitFloor;
        if (currentTrampoline != null) {
            currentTrampoline.RefreshTrampoline();
        }
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

    private IEnumerator PlayJumpAnimation() {
        Debug.Log("PlayJumpAnimation()");
        animator.SetBool("IsJumping", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("IsJumping", false);
    }

    private GameObject GetJumpPointObject() {
        Debug.Log("GetJumpPointObject()");
        Physics.Raycast(this.gameObject.transform.position, this.currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask);
        GameObject jumpPointObject = hit.collider.gameObject;
        return jumpPointObject;
    }

    

    

}
