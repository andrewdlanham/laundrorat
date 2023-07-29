using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement
{

    #region LayerMasks
    private static LayerMask ceilingMask = 1 << LayerMask.NameToLayer("Ceiling");
    private static LayerMask trampolineMask = 1 << LayerMask.NameToLayer("Trampoline");
    private static LayerMask trampolineExitPointMask = 1 << LayerMask.NameToLayer("TrampolineExitPoint");
    private static LayerMask jumpPointMask = 1 << LayerMask.NameToLayer("JumpPoint");
    private static LayerMask wallMask = 1 << LayerMask.NameToLayer("Wall");
    #endregion

    #region Get Character Points
    public static Vector2 GetTopOfCharacter(GameObject characterObject) {
        BoxCollider2D characterCollider = characterObject.GetComponent<BoxCollider2D>();
        return (Vector2) characterObject.transform.position + new Vector2(0f, characterCollider.bounds.extents.y);
    }

    public static Vector2 GetBottomOfCharacter(GameObject characterObject) {
        BoxCollider2D characterCollider = characterObject.GetComponent<BoxCollider2D>();
        return (Vector2) characterObject.transform.position - new Vector2(0f, characterCollider.bounds.extents.y);
    }

    #endregion

    #region Location Checks
    public static bool IsUnderCeiling(GameObject characterObject) {
        if (Physics.Raycast(GetTopOfCharacter(characterObject), Vector2.up, out RaycastHit hit, 0.2f, ceilingMask)) {
            return true;
        }
        return false;
    }

    public static bool IsOverTrampoline(GameObject characterObject) {
        if (Physics.Raycast(GetBottomOfCharacter(characterObject), Vector2.down, out RaycastHit hit, 0.2f, trampolineMask)) {
            return true;
        }
        return false;
    }

    public static bool CanEnterBouncing(CharacterController character) {
        if (Physics.Raycast(character.gameObject.transform.position, character.currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask)) {
            Debug.Log("Can enter bouncing");
            return true;
        }
        return false;
    }

    public static bool CanExitBouncing(CharacterController character) {
        if (character.currentVerticalDirection == Vector2.down) return false;
        if (Physics.Raycast(character.gameObject.transform.position, character.currentHorizontalDirection, out RaycastHit hit, 2.5f, trampolineExitPointMask)) {
            Debug.Log("Can exit bouncing");
            return true;
        }
        return false;   
    }

    public static bool CanBounceOffWall(PlayerController player) {
        if (player.currentVerticalDirection == Vector2.down) return false;
        if (player.horizontalInput == 0 ) {
            Debug.Log("Cannot bounce without input");
            return false;
        }
        if (Physics.Raycast(player.gameObject.transform.position, player.currentHorizontalDirection, out RaycastHit hit, 2.5f, wallMask)) {
            Debug.Log("Can bounce off wall");
            return true;
        }
        return false;  
    }
    #endregion

    

    public static GameObject GetExitPointObject(CharacterController character) {
        Physics.Raycast(character.gameObject.transform.position, character.currentHorizontalDirection, out RaycastHit hit, 2.5f, trampolineExitPointMask);
        GameObject exitPointObject = hit.collider.gameObject;
        return exitPointObject;
    }

    public static Trampoline GetTrampoline(CharacterController character) {
        Physics.Raycast(GetBottomOfCharacter(character.gameObject), Vector2.down, out RaycastHit hit, 0.2f, trampolineMask);
        Trampoline trampoline = hit.collider.gameObject.GetComponent<Trampoline>();
        return trampoline;
    }

    public static bool ShouldReverseVerticalDirection(CharacterController character) {
        return (IsBouncingIntoTrampoline(character) || IsBouncingIntoCeiling(character));
    }

    public static bool IsBouncingIntoCeiling(CharacterController character) {
        return character.currentVerticalDirection == Vector2.up && IsUnderCeiling(character.gameObject);
    }

    public static bool IsBouncingIntoTrampoline(CharacterController character) {
        return character.currentVerticalDirection == Vector2.down && IsOverTrampoline(character.gameObject);
    }

    








}
