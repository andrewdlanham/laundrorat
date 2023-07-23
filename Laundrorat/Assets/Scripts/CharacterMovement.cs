using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private LayerMask ceilingMask;

    public bool IsUnderCeiling(GameObject character) {
        if (Physics.Raycast(GetTopOfCharacter(character), Vector2.up, out RaycastHit hit, 0.2f, ceilingMask)) {
            return true;
        }
        return false;
    }

    public Vector2 GetTopOfCharacter(GameObject character) {
        BoxCollider2D characterCollider = character.GetComponent<BoxCollider2D>();
        return (Vector2) character.transform.position + new Vector2(0f, characterCollider.bounds.extents.y);
    }

}
