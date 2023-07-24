using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private float smoothingSpeed;
    private Vector3 cameraOffset;

    void Awake() {
        smoothingSpeed = 0.1f;
        cameraOffset = new Vector3(0,0,-10);
    }

    void LateUpdate() {
        Vector3 newPosition = cameraOffset;
        newPosition.x += playerController.gameObject.transform.position.x;
        transform.position = newPosition;
    }
}
