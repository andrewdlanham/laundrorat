using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    private SpriteRenderer trampolineRenderer;

    private int numBouncesLeft;

    void Awake() {
        trampolineRenderer = GetComponent<SpriteRenderer>();
        numBouncesLeft = 3;
        UpdateTrampolineColor();
    }

    private void UpdateTrampolineColor() {
        Color newColor = Color.black;
        Material newMaterial = new Material(trampolineRenderer.material);

        switch (numBouncesLeft) {
            case 3:
                newColor = Color.green;
                break;
            case 2:
                newColor = Color.blue;
                break;
            case 1:
                newColor = Color.yellow;
                break;
            case 0:
                newColor = Color.red;
                break;
        }
        newMaterial.color = newColor;
        trampolineRenderer.material = newMaterial;

    }

    private void BreakTrampoline() {
        Destroy(gameObject);
    }

    public void RegisterBounce() {
        numBouncesLeft--;
        if (numBouncesLeft == -1) {
            BreakTrampoline();
        } else {
            UpdateTrampolineColor();
        }
    }



}
