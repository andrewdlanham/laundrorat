using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // LayerMasks
    public static LayerMask jumpPointMask;
    public LayerMask ceilingMask;
    public LayerMask trampolineMask;
    public LayerMask wallMask;
    public LayerMask exitPointMask;
    public LayerMask floorMask;

    // Raycast Lengths
    private float jumpPointRCLength = 1.5f;
    private float ceilingRCLength = 0.2f;
    private float trampolineRCLength = 0.2f;
    private float exitPointRCLength = 2.5f;

    private float floorRCLength = 0.2f;
    
    public float movementSpeed;
    public float bouncingSpeed;

    public bool isGrounded;
    public bool isBouncing;
    
    public bool isJumping;

    public Vector2 currentHorizontalDirection;
    public Vector2 currentVerticalDirection;

    public int currentFloor;

    public Trampoline currentTrampoline;

    // Component References
    public Animator animator;
    public new SpriteRenderer renderer;
    public new BoxCollider2D collider;

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
        floorMask = 1 << LayerMask.NameToLayer("Floor");
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
        //SwitchToBouncing();
        GameObject jumpPointObject = GetJumpPointObject();
        JumpToObject(jumpPointObject);
        //transform.position = jumpPointObject.transform.position;
        //this.currentVerticalDirection = Vector2.down;
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

    
    private IEnumerator PlayJumpAnimation(GameObject obj) {
        Debug.Log("PlayJumpAnimation()");
        this.isJumping = true;
        //animator.SetBool("IsJumping", true);
        
        // TODO: Handle jump animation
        yield return new WaitForSeconds(1f / 60f); // Crouch for one frame

        // Move towards object for some frames
        Vector3 startingPosition = transform.position;
        Vector3 endPosition = obj.transform.position;
        Vector3 movementVector = (endPosition - startingPosition) / 30; 

        for (int i = 0; i < 30; i++) {
            transform.position += movementVector;
            yield return new WaitForSeconds(1f / 60f);
        }


        this.isJumping = false;
        //animator.SetBool("IsJumping", false);

        //transform.position = obj.transform.position;
        this.currentVerticalDirection = Vector2.down;

        SwitchToBouncing();
    }

    

    protected Vector2 GetTopOfCollider() {
        return (Vector2) this.gameObject.transform.position + new Vector2(0f, this.collider.bounds.extents.y);
    }

    protected Vector2 GetBottomOfCollider() {
        return (Vector2) this.gameObject.transform.position - new Vector2(0f, this.collider.bounds.extents.y);
    }

    protected Vector2 GetLeftOfCollider() {
        return new Vector2(this.collider.bounds.min.x, this.gameObject.transform.position.y);
    }

    protected Vector2 GetRightOfCollider() {
        return new Vector2(this.collider.bounds.max.x, this.gameObject.transform.position.y);
    }

    protected bool IsUnderCeiling() {
        return Physics.Raycast(GetTopOfCollider(), Vector2.up, out RaycastHit hit, ceilingRCLength, ceilingMask);
    }

    protected bool IsOverTrampoline() {
        return Physics.Raycast(GetBottomOfCollider(), Vector2.down, out RaycastHit hit, trampolineRCLength, trampolineMask);
    }

    protected bool IsOverFloor() {
        return Physics.Raycast(GetBottomOfCollider(), Vector2.down, out RaycastHit hit, floorRCLength, floorMask);
    }

    protected bool IsBouncingIntoCeiling() {
        return this.currentVerticalDirection == Vector2.up && IsUnderCeiling();
    }

    protected bool IsBouncingIntoTrampoline() {
        return this.currentVerticalDirection == Vector2.down && IsOverTrampoline();
    }

    protected bool IsBouncingIntoFloor() {
        return this.currentVerticalDirection == Vector2.down && IsOverFloor();
    }

    protected bool CanExitBouncing() {
        if (this.currentVerticalDirection == Vector2.down) return false;
        if (IsHoldingLeft()) {
            return Physics.Raycast(GetLeftOfCollider(), Vector2.left, out RaycastHit hit, exitPointRCLength, exitPointMask); 
        } else {
            return Physics.Raycast(this.gameObject.transform.position, this.currentHorizontalDirection, out RaycastHit hit, exitPointRCLength, exitPointMask); 
        }
    }

    protected bool CanEnterBouncing() {
        return Physics.Raycast(this.gameObject.transform.position, this.currentHorizontalDirection, out RaycastHit hit, jumpPointRCLength, jumpPointMask);
    }

    protected GameObject GetExitPointObject() {
        GameObject exitPointObject;
        if (IsHoldingLeft()) {
            Physics.Raycast(GetLeftOfCollider(), Vector2.left, out RaycastHit hit, exitPointRCLength, exitPointMask);
            exitPointObject = hit.collider.gameObject;
        } else {
            Physics.Raycast(GetRightOfCollider(), Vector2.right, out RaycastHit hit, exitPointRCLength, exitPointMask);
            exitPointObject = hit.collider.gameObject;
        }
        
        return exitPointObject;
    }

    private GameObject GetJumpPointObject() {
        Debug.Log("GetJumpPointObject()");
        GameObject jumpPointObject;
        if (IsHoldingLeft()) {
            Physics.Raycast(GetLeftOfCollider(), Vector2.left, out RaycastHit hit, jumpPointRCLength, jumpPointMask);
            jumpPointObject = hit.collider.gameObject;
        } else {
            Physics.Raycast(GetRightOfCollider(), Vector2.right, out RaycastHit hit, jumpPointRCLength, jumpPointMask);
            jumpPointObject = hit.collider.gameObject;
        }
        
        return jumpPointObject;        
    }

    protected Trampoline GetTrampoline() {
        Physics.Raycast(GetBottomOfCollider(), Vector2.down, out RaycastHit hit, trampolineRCLength, trampolineMask);
        Trampoline trampoline = hit.collider.gameObject.GetComponent<Trampoline>();
        return trampoline;
    }

    protected void FlipSpriteIfNeeded() {
        if (currentHorizontalDirection.x > 0) {
            renderer.flipX = false;
        } else if (currentHorizontalDirection.x < 0) {
            renderer.flipX = true;
        }
    }

    protected bool IsHoldingLeft() {
        return this.currentHorizontalDirection == Vector2.left;
    }

    protected void JumpToObject(GameObject obj) {
        StartCoroutine(PlayJumpAnimation(obj));
    }

    protected bool IsJumping() {
        return this.isJumping;
    }

    

    

}
