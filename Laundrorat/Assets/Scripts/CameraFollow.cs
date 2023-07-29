using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private Vector3 cameraOffset;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        cameraOffset = new Vector3(0,0,-10);
    }

    void LateUpdate() {
        Vector3 newPosition = cameraOffset;
        newPosition.x += player.gameObject.transform.position.x;
        transform.position = newPosition;
    }
}
