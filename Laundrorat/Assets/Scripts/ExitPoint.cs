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
        if (yPosition > 0 && yPosition < 2.2) {
            floorNumber = 0;
        } else if (yPosition >= 2.2 && yPosition < 4) {
            floorNumber = 1;
        } else if (yPosition >= 4 && yPosition < 5.5) {
            floorNumber = 2;
        } else if (yPosition >= 5.5 && yPosition < 7) {
            floorNumber = 3;
        } else if (yPosition >= 7 && yPosition < 8.5) {
            floorNumber = 4;
        }
    }

}
