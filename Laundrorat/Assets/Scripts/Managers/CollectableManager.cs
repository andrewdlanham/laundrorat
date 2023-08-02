using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager instance;

    [SerializeField] List<GameObject> collectableObjectsList;

    public bool triggerStageClear;

    void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
    }
    
    public void InitializeCollectableObjectsList() {
        GameObject[] collectableObjectsArray = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (GameObject obj in collectableObjectsArray) {
            collectableObjectsList.Add(obj);
        }
    }

    void Update() {
        if (triggerStageClear) {
            GameObject.Find("GameManager").GetComponent<GameManager>().StageClear();
            triggerStageClear = false;
        }
    }

    public void RemoveCollectableObject(GameObject collectableObject) {
        collectableObjectsList.Remove(collectableObject);
    }

    public bool NoCollectablesLeft() {
        return collectableObjectsList.Count == 0;
    }


}
