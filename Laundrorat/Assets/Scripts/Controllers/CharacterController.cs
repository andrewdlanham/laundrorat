using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // LayerMasks
    public static LayerMask jumpPointMask;
    public LayerMask ceilingMask;
    public LayerMask trampolineMask;
    public LayerMask wallMask;
    public LayerMask exitPointMask;
    
    public float movementSpeed;
    public float bouncingSpeed;

    public bool isGrounded;
    public bool isBouncing;

    public Vector2 currentHorizontalDirection;
    public Vector2 currentVerticalDirection;

    public int currentFloor;

    public Trampoline currentTrampoline;

    // Component References
    public Animator animator;
    public new SpriteRenderer renderer;
    public BoxCollider2D collider;

    protected void SetComponentReferences() {
        this.animator = GetComponent<Animator>();
        this.renderer = GetComponent<SpriteRenderer>();
        this.collider = GetComponent<BoxCollider2D>();
    }

    protected void SetLayerMasks() {
        jumpPointMask = 1 << LayerMask.NameToLayer("JumpPoint");
        ceilingMask = 1 << LayerMask.NameToLayer("Ceiling");
        trampolineMask = 1 << LayerMask.NameToLayer("Trampoline");
        wallMask = 1 << LayerMask.NameToLayer("Wall");
        exitPointMask = 1 << LayerMask.NameToLayer("TrampolineExitPoint");
    }

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
        GameObject exitPointObject = GetExitPointObject();
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

    protected Vector2 GetTopOfCollider() {
        return (Vector2) this.gameObject.transform.position + new Vector2(0f, this.collider.bounds.extents.y);
    }

    protected Vector2 GetBottomOfCollider() {
        return (Vector2) this.gameObject.transform.position +-new Vector2(0f, this.collider.bounds.extents.y);
    }

    protected bool IsUnderCeiling() {
        return Physics.Raycast(GetTopOfCollider(), Vector2.up, out RaycastHit hit, 0.2f, ceilingMask);
    }

    protected bool IsOverTrampoline() {
        return Physics.Raycast(GetBottomOfCollider(), Vector2.down, out RaycastHit hit, 0.2f, trampolineMask);
    }

    protected bool IsBouncingIntoCeiling() {
        return this.currentVerticalDirection == Vector2.up && IsUnderCeiling();
    }

    protected bool IsBouncingIntoTrampoline() {
        return this.currentVerticalDirection == Vector2.down && IsOverTrampoline();
    }

    protected bool CanExitBouncing() {
        if (this.currentVerticalDirection == Vector2.down) return false;
        return Physics.Raycast(this.gameObject.transform.position, this.currentHorizontalDirection, out RaycastHit hit, 2.5f, exitPointMask); 
    }

    protected bool CanEnterBouncing() {
        return Physics.Raycast(this.gameObject.transform.position, this.currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask);
    }

    protected GameObject GetExitPointObject() {
        Physics.Raycast(this.gameObject.transform.position, this.currentHorizontalDirection, out RaycastHit hit, 2.5f, exitPointMask);
        GameObject exitPointObject = hit.collider.gameObject;
        return exitPointObject;
    }

    protected Trampoline GetTrampoline() {
        Physics.Raycast(GetBottomOfCollider(), Vector2.down, out RaycastHit hit, 0.2f, trampolineMask);
        Trampoline trampoline = hit.collider.gameObject.GetComponent<Trampoline>();
        return trampoline;
    }

    

    

}
