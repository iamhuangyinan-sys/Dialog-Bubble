using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缓存池中对象数据类
/// </summary>
public class poolData
{
    //对象容器
    private Stack<GameObject> dataStack = new Stack<GameObject>();
    //对象根物体
    private GameObject rootObject;

    //对象容器长度
    public int Count => dataStack.Count;

    /// <summary>
    /// 初始化对象数据类
    /// </summary>
    /// <param name="root">缓存池根物体</param>
    /// <param name="name">对象根物体</param>
    public poolData(GameObject poolRoot, string name)
    {
        rootObject = new GameObject(name);
        rootObject.transform.SetParent(poolRoot.transform);
    }
    /// <summary>
    /// 从对象数据中取出对象
    /// </summary>
    /// <returns>取出的游戏物体</returns>
    public GameObject Pop()
    {
        GameObject obj = dataStack.Pop();
        obj.SetActive(true);
        obj.transform.SetParent(null);

        return obj;
    }
    /// <summary>
    /// 在对象数据中放入对象
    /// </summary>
    /// <param name="obj">放入的游戏物体</param>
    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(rootObject.transform);
        dataStack.Push(obj);
    }
}

/// <summary>
/// 缓存池模块管理器
/// </summary>
public class PoolMgr : Singleton<PoolMgr>
{
    private PoolMgr() { }

    //存放所有对象数据类的字典
    private Dictionary<string, poolData> poolDic = new Dictionary<string, poolData>();
    //缓存池根物体
    private GameObject poolObject;

    /// <summary>
    /// 从缓存池中取出一个对象
    /// </summary>
    /// <param name="name">缓存池中对象数据类的名称</param>
    /// <returns>取出的对象</returns>
    public GameObject GetObject(string name)
    {
        GameObject obj;
        if(poolDic.ContainsKey(name) && poolDic[name].Count > 0)
        {
            obj = poolDic[name].Pop();
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;
        }
        return obj;
    }

    /// <summary>
    /// 往缓存池中放入一个对象
    /// </summary>
    /// <param name="name">缓存池中对象数据类的名称</param>
    /// <param name="obj">放入的对象</param>
    public void PushObject(string name, GameObject obj)
    {
        if(poolObject == null)
        {
            poolObject = new GameObject("Pool");
        }

        if (!poolDic.ContainsKey(obj.name))
        {
            poolDic.Add(name, new poolData(poolObject, obj.name));
        }
        poolDic[name].Push(obj);
    }

    /// <summary>
    /// 清空缓存池
    /// </summary>
    public void ClearPool()
    {
        poolDic.Clear();
        poolObject = null;
    }
}
