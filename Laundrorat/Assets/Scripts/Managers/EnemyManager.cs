using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{


    public static EnemyManager instance;
    [SerializeField] List<GameObject> enemyObjectsList;

    void Awake() {
        Debug.Log("EnemyManager Awake()");

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }


        GameObject[] enemyObjectsArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemyObjectsArray) {
            enemyObjectsList.Add(obj);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        Debug.Log("EnemyManager Start()");
    }

    public void DisableAllEnemies() {
        foreach (GameObject enemyObject in enemyObjectsList) {
            enemyObject.GetComponent<EnemyController>().enabled = false;
        }
    }
}
