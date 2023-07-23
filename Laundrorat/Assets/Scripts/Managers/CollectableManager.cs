using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{

    [SerializeField] List<GameObject> collectableObjectsList;

    void Awake() {
        GameObject[] collectableObjectsArray = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (GameObject obj in collectableObjectsArray) {
            collectableObjectsList.Add(obj);
        }
    }

    void Update() {
        if (NoCollectablesLeft()) {
            GameObject.Find("GameManager").GetComponent<GameManager>().FinishLevel();
        }
    }

    public void RemoveCollectableObject(GameObject collectableObject) {
        collectableObjectsList.Remove(collectableObject);
    }

    private bool NoCollectablesLeft() {
        return collectableObjectsList.Count == 0;
    }


}
