using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int numPointsWorth;

    [SerializeField] private BoxCollider2D collectableCollider;

    void Awake() {
        collectableCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collectable triggered");
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().AddPointsToScore(numPointsWorth);
        GameObject.Find("CollectableManager").GetComponent<CollectableManager>().RemoveCollectableObject(this.gameObject);
        Destroy(this.gameObject);
    }
}
