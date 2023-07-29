using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int numPointsWorth;

    [SerializeField] private BoxCollider2D collectableCollider;

    [SerializeField] public float scoreMultiplier;

    [SerializeField] private Collectable matchingCollectable;

    private Coroutine flashingCoroutine;

    private new SpriteRenderer renderer;

    void Awake() {
        collectableCollider = GetComponent<BoxCollider2D>();
        scoreMultiplier = 1.0f;
        this.renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collectable triggered");
        PlayCoinSound();

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();

        string scoreText = numPointsWorth.ToString();
        int scoreMultiplier = 1;

        if (matchingCollectable == player.lastCollectable) {
            scoreMultiplier = gameManager.scoreMultiplier;
            numPointsWorth *= scoreMultiplier;
            scoreText += " x " + scoreMultiplier.ToString();
            gameManager.scoreMultiplier++;
        }
        
        if (gameManager.flashingCollectable != null) {
            gameManager.flashingCollectable.StopFlashing();
        }

        if (matchingCollectable.gameObject.activeSelf) {
            matchingCollectable.StartFlashing();
            gameManager.flashingCollectable = matchingCollectable;
        }

        player.lastCollectable = this;
        ScoreManager scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        scoreManager.AddPointsToScore(numPointsWorth);
        scoreManager.ShowScorePopup(this.gameObject, scoreText);
        GameObject.Find("CollectableManager").GetComponent<CollectableManager>().RemoveCollectableObject(this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void StartFlashing() {
        Debug.Log("StartFlashing()");
        flashingCoroutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash() {
        while (true) {
            renderer.enabled = (!renderer.enabled);
            yield return new WaitForSeconds(0.4f);
        }
    }

    public void StopFlashing() {
        Debug.Log("StopFlashing()");
        StopCoroutine(flashingCoroutine);
        renderer.enabled = true;
    }

    private void PlayCoinSound() {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound("CoinSound");
    }

}
