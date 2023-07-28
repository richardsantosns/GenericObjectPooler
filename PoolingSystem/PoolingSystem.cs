using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToPool;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, GameObject> objectPrefabDictionary;

    public static PoolingSystem Instance;

    private void OnEnable()
    {
        IPooler.OnPoolRequest += SpawnFromPool;
    }

    private void OnDisable()
    {
        IPooler.OnPoolRequest -= SpawnFromPool;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        objectPrefabDictionary = new Dictionary<string, GameObject>();

        objectsToPool = new List<GameObject>();
        objectsToPool = Resources.LoadAll<GameObject>("PooledObjects").ToList(); // Create a folder under Resource folder named PooledObjects

        foreach (GameObject obj in objectsToPool)
        {
            string key = obj.name;
            objectPrefabDictionary.Add(key, obj);
            CreatePrefabPool(obj);
        }
    }

    private void CreatePrefabPool(GameObject objectToPool)
    {
        string key = objectToPool.name;
        IPoolable poolable = objectToPool.GetComponent<IPoolable>();

        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < poolable.AmountToPool(); i++)
        {
            GameObject obj = Instantiate(objectPrefabDictionary[key]);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(key, objectPool);
    }

    private GameObject SpawnFromPool(string key, Vector3 position)
    {
        if (poolDictionary.ContainsKey(key) && poolDictionary[key].Count > 0)
        {
            GameObject objectToSpawn = poolDictionary[key].Dequeue();
            return ActivateObjectPrefab(position, objectToSpawn);
        }

        GameObject newObjectPrefab = Instantiate(objectPrefabDictionary[key]);
        return ActivateObjectPrefab(position, newObjectPrefab);
    }

    private GameObject ActivateObjectPrefab(Vector3 position, GameObject objectPrefab)
    {
        objectPrefab.SetActive(true);
        objectPrefab.transform.position = position;
        objectPrefab.transform.rotation = Quaternion.identity;

        IPoolable poolable = objectPrefab.GetComponent<IPoolable>();

        if (poolable != null)
        {
            poolable.Spawn();
        }

        return objectPrefab;
    }

    public void ReturnObjectToPool(GameObject objectToReturn, string key)
    {
        objectToReturn.name = key;
        objectToReturn.transform.SetParent(transform);
        objectToReturn.SetActive(false);
        poolDictionary[key].Enqueue(objectToReturn);
    }
}
