using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 300;

    [SerializeField] private GameObject inkPrefab;

    // Singleton
    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Instantiate all objects to pool
    private void Start() {
        for (int i = 0; i < amountToPool; i++)   
        {
            GameObject obj = Instantiate(inkPrefab);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Retrieves a pooled object that is not already active
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
