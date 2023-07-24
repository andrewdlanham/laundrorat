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
    #endregion

    #region Get Character Points
    public static Vector2 GetTopOfCharacter(GameObject character) {
        BoxCollider2D characterCollider = character.GetComponent<BoxCollider2D>();
        return (Vector2) character.transform.position + new Vector2(0f, characterCollider.bounds.extents.y);
    }

    public static Vector2 GetBottomOfCharacter(GameObject character) {
        BoxCollider2D characterCollider = character.GetComponent<BoxCollider2D>();
        return (Vector2) character.transform.position - new Vector2(0f, characterCollider.bounds.extents.y);
    }

    #endregion

    #region Location Checks
    public static bool IsUnderCeiling(GameObject character) {
        if (Physics.Raycast(GetTopOfCharacter(character), Vector2.up, out RaycastHit hit, 0.2f, ceilingMask)) {
            return true;
        }
        return false;
    }

    public static bool IsOverTrampoline(GameObject character) {
        if (Physics.Raycast(GetBottomOfCharacter(character), Vector2.down, out RaycastHit hit, 0.2f, trampolineMask)) {
            return true;
        }
        return false;
    }

    public static bool CanEnterBouncing(CharacterController character) {
        if (Physics.Raycast(character.gameObject.transform.position, character.currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask)) {
            return true;
        }
        return false;
    }

    public static bool CanExitBouncing(CharacterController character) {
        if (character.currentVerticalDirection == Vector2.down) return false;
        if (Physics.Raycast(character.gameObject.transform.position, character.currentHorizontalDirection, out RaycastHit hit, 1f, trampolineExitPointMask)) {
            return true;
        }
        return false;   
    }
    #endregion

    public static GameObject GetJumpPointObject(CharacterController character) {
        Physics.Raycast(character.gameObject.transform.position, character.currentHorizontalDirection, out RaycastHit hit, 1f, jumpPointMask);
        GameObject jumpPointObject = hit.collider.gameObject;
        return jumpPointObject;
    }

    public static GameObject GetExitPointObject(CharacterController character) {
        Physics.Raycast(character.gameObject.transform.position, character.currentHorizontalDirection, out RaycastHit hit, 1f, trampolineExitPointMask);
        GameObject exitPointObject = hit.collider.gameObject;
        return exitPointObject;
    }

    public static bool ShouldReverseVerticalDirection(CharacterController character) {
        return (IsBouncingIntoTrampoline(character) || IsBouncingIntoCeiling(character));
    }

    private static bool IsBouncingIntoCeiling(CharacterController character) {
        return character.currentVerticalDirection == Vector2.up && IsUnderCeiling(character.gameObject);
    }

    private static bool IsBouncingIntoTrampoline(CharacterController character) {
        return character.currentVerticalDirection == Vector2.down && IsOverTrampoline(character.gameObject);
    }




}