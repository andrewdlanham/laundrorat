using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyObjectsList;

    void Awake() {
        GameObject[] enemyObjectsArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemyObjectsArray) {
            enemyObjectsList.Add(obj);
        }
    }

    public void DisableAllEnemies() {
        foreach (GameObject enemyObject in enemyObjectsList) {
            enemyObject.GetComponent<EnemyController>().enabled = false;
        }
    }
}
