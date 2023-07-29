using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    public int floorNumber;
    private new SpriteRenderer renderer;

    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        HideExitPointSprite();
        SetFloorNumber();
    }

    private void HideExitPointSprite() {
        renderer.enabled = false;
    }

    // Set floor number based on transform's y position
    private void SetFloorNumber() {
        floorNumber = -1;
        Vector2 exitPointPosition = gameObject.transform.position;
        float yPosition = exitPointPosition.y;
        if (yPosition > 0 && yPosition < 1.5) {
            floorNumber = 0;
        } else if (yPosition >= 1.5 && yPosition < 3) {
            floorNumber = 1;
        } else if (yPosition >= 3 && yPosition < 4.5) {
            floorNumber = 2;
        } else if (yPosition >= 4.5 && yPosition < 6) {
            floorNumber = 3;
        } else if (yPosition >= 6 && yPosition < 7.5) {
            floorNumber = 4;
        }
    }

}
