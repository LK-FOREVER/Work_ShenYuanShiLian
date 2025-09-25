using System.Collections.Generic;
using UnityEngine;

public class MainObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;          // 对象标识
        public GameObject prefab;    // 预制体
        public int size;             // 初始池大小
    }

    public List<Pool> pools;                // 可配置的池列表
    public Dictionary<string, Queue<GameObject>> poolDictionary; // 对象池字典

    #region Singleton
    public static MainObjectPool Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    #endregion

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // 初始化所有池
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab,transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    /// <summary>
    /// 从对象池中获取对象
    /// </summary>
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"对象池中没有找到tag为{tag}的对象");
            return null;
        }

        // 如果池空了，就新建一个对象
        if (poolDictionary[tag].Count == 0)
        {
            GameObject newObj = Instantiate(GetPrefabByTag(tag));
            newObj.SetActive(false);
            poolDictionary[tag].Enqueue(newObj);
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // 调用对象的OnSpawn方法（如果存在）
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        return objectToSpawn;
    }

    /// <summary>
    /// 将对象返回到对象池
    /// </summary>
    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"对象池中没有找到tag为{tag}的对象");
            return;
        }

        objectToReturn.SetActive(false);
        objectToReturn.transform.SetParent(transform); // 放回池管理
        objectToReturn.transform.localPosition = Vector3.zero; // 可选：归位到原点
        poolDictionary[tag].Enqueue(objectToReturn);
    }

    private GameObject GetPrefabByTag(string tag)
    {
        foreach (var pool in pools)
        {
            if (pool.tag == tag)
            {
                return pool.prefab;
            }
        }
        return null;
    }
}

/// <summary>
/// 可被对象池管理的对象接口
/// </summary>
public interface IPooledObject
{
    /// <summary>
    /// 当对象从池中取出时调用
    /// </summary>
    void OnObjectSpawn();
}