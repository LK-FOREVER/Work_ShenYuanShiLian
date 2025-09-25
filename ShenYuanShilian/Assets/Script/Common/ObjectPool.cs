using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetable
{
    void OnReset();
}

public class ObjectPool : SingletonAutoMonoBase<ObjectPool>
{
    private Dictionary<string, Queue<GameObject>> objectPool = new();
    public GameObject CreateObject(GameObject prefab, Vector3 pos, Quaternion rotation, string key = null)
    {
        if (string.IsNullOrEmpty(key)) key = prefab.name;

        GameObject go = FindUsableObject(key);

        go ??= AddObject(key, prefab);

        UseObject(go, pos, rotation);
        return go;
    }

    public void CollectObject(GameObject go, float delayTime = 0, string key = null)
    {
        if (string.IsNullOrEmpty(key)) key = go.name.Replace("(Clone)", string.Empty);
        StartCoroutine(DelayCollectObject(go, delayTime, key));
    }

    private IEnumerator DelayCollectObject(GameObject go, float delayTime, string key)
    {
        yield return new WaitForSeconds(delayTime);
        if (!objectPool[key].Contains(go))
        {
            objectPool[key].Enqueue(go);
            go.SetActive(false);
        }
    }

    public void Clear(string key)
    {
        foreach (var item in objectPool[key])
        {
            Destroy(item);
        }
        objectPool.Remove(key);
    }

    public void Clear()
    {
        List<string> keyList = new(objectPool.Keys);
        foreach (var item in keyList)
        {
            Clear(item);
        }
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }

    /// <summary>
    /// 查找指定类别中可用的对象
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private GameObject FindUsableObject(string key)
    {
        GameObject go = null;
        if (objectPool.ContainsKey(key) && objectPool[key].Count > 0)
            go = objectPool[key].Dequeue();
        return go;
    }

    /// <summary>
    /// 添加对象
    /// </summary>
    /// <param name="key"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private GameObject AddObject(string key, GameObject prefab)
    {
        GameObject go = Instantiate(prefab);
        if (!objectPool.ContainsKey(key)) objectPool.Add(key, new Queue<GameObject>());

        GameObject childPool = GameObject.Find(key + "Pool");
        if (!childPool)
        {
            childPool = new GameObject(key + "Pool");
            childPool.transform.SetParent(transform);
        }
        go.transform.SetParent(childPool.transform);
        return go;
    }

    /// <summary>
    /// 使用对象
    /// </summary>
    /// <param name="go"></param>
    /// <param name="pos"></param>
    /// <param name="rotation"></param>
    private void UseObject(GameObject go, Vector3 pos, Quaternion rotation)
    {
        go.transform.SetPositionAndRotation(pos, rotation);
        go.SetActive(true);
        foreach (var item in go.GetComponents<IResetable>())
        {
            item.OnReset();
        }
    }
}
