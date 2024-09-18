using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject objectToPool;
        public int size;
    }


    public List<Pool> _pools;
    public Dictionary<string, Queue<GameObject>> _poolDistionary;

    #region Singleton
    public static PoolManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        _poolDistionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject buffer = Instantiate(pool.objectToPool, transform);
                buffer.SetActive(false);
                objectPool.Enqueue(buffer);
            }

            _poolDistionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject InstantiateFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDistionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with " + tag + " tag doesn't exist");
            return null;
        }

        GameObject objectToSpawn = _poolDistionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject poolInterface = objectToSpawn.GetComponent<IPooledObject>();
        if (poolInterface != null)
        {
            poolInterface.OnObjectInstantiated();
        }
        _poolDistionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
