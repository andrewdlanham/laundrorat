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

        DontDestroyOnLoad(gameObject);
    }

    public void InitializeEnemyObjectsList() {

        Debug.Log("InitializeEnemyObjectsArray()");

        enemyObjectsList = new List<GameObject>();
        
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

    public void EnableAllEnemies() {
        foreach (GameObject enemyObject in enemyObjectsList) {
            enemyObject.GetComponent<EnemyController>().enabled = true;
        }
    }

    public void SpeedUpAllEnemies() {
        Debug.Log("SpeedUpAllEnemies()");
        foreach (GameObject enemyObject in enemyObjectsList) {
            enemyObject.GetComponent<EnemyController>().movementSpeed += 1.5f;
            enemyObject.GetComponent<EnemyController>().bouncingSpeed += 1;
        }
    }

    public void SetSpeedAllEnemies(float movementSpeed, float bouncingSpeed) {
        Debug.Log("SetSpeedAllEnemies()");
        foreach (GameObject enemyObject in enemyObjectsList) {
            enemyObject.GetComponent<EnemyController>().movementSpeed = movementSpeed;
            enemyObject.GetComponent<EnemyController>().bouncingSpeed = bouncingSpeed;
        }
    }
}
