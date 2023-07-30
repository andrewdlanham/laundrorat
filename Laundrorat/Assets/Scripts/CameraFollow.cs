using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private Vector3 cameraOffset;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        cameraOffset = new Vector3(0,4.5f,-10);
    }

    void LateUpdate() {
        Vector3 newPosition = cameraOffset;
        newPosition.x += player.gameObject.transform.position.x;
        //Debug.Log("newPosition: " + newPosition);
        if (IsInBounds(newPosition)) {
            transform.position = newPosition;
        }
    }

    private bool IsInBounds(Vector3 position) {
        
        if (position.x < 1 || position.x > 14) {
            return false;
        }
        return true;
    }
}
