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
        ScoreManager scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        scoreManager.AddPointsToScore(numPointsWorth);
        scoreManager.ShowScorePopup(this.gameObject, numPointsWorth);
        GameObject.Find("CollectableManager").GetComponent<CollectableManager>().RemoveCollectableObject(this.gameObject);
        Destroy(this.gameObject);
    }
}
