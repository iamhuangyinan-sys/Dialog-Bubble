using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Resources资源管理器
/// </summary>
public class ResourcesMgr : Singleton<ResourcesMgr>
{
    private ResourcesMgr() { }

    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="path">资源路径</param>
    /// <returns>加载的资源</returns>
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
    
    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="path">资源路径</param>
    /// <param name="callBack">加载结束后回调的函数</param>
    public void LoadAsync<T>(string path, UnityAction<T> callBack) where T : Object
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadAsync<T>(path, callBack));
    }

    private IEnumerator ReallyLoadAsync<T>(string path, UnityAction<T> callBack) where T : Object
    {
        ResourceRequest rr = Resources.LoadAsync<T>(path);
        yield return rr;
        callBack(rr.asset as T);
    }

    /// <summary>
    /// 指定卸载一个资源
    /// </summary>
    /// <param name="assetToUnload">被指定的资源</param>
    public void UnloadAsset(Object assetToUnload)
    {
        Resources.UnloadAsset(assetToUnload);
    }

    /// <summary>
    /// 异步卸载没有使用资源
    /// </summary>
    /// <param name="callBack">卸载结束后回调的函数</param>
    public void UnloadUnusedAssets(UnityAction callBack)
    {
        MonoMgr.Instance.StartCoroutine(ReallyUnloadUnusedAssets(callBack));
    }

    private IEnumerator ReallyUnloadUnusedAssets(UnityAction callBack)
    {
        AsyncOperation ao = Resources.UnloadUnusedAssets();
        yield return ao;
        callBack();
    }

}
