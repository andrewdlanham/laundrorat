using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    private new SpriteRenderer renderer;

    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        HideJumpPointSprite();
    }

    private void HideJumpPointSprite() {
        renderer.enabled = false;
    }
}
