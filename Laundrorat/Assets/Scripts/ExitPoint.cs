using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    [SerializeField] public int floorNumber;
    private SpriteRenderer renderer;

    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        HideExitPointSprite();
    }

    private void HideExitPointSprite() {
        renderer.enabled = false;
    }
}
