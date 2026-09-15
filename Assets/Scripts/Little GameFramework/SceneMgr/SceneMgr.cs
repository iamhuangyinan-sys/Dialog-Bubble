using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    private SceneMgr() { }

    /// <summary>
    /// 同步切换场景
    /// </summary>
    /// <param name="name">场景名称</param>
    /// <param name="callback">回调函数</param>
    public void LoadScene(string name, UnityAction callback = null)
    {
        SceneManager.LoadScene(name);
        callback?.Invoke();
    }

    /// <summary>
    /// 异步切换场景
    /// </summary>
    /// <param name="name">场景名称</param>
    /// <param name="callback">回调函数</param>
    public void LoadSceneAsyn(string name, UnityAction callback = null)
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsyn(name, callback));
    }
    private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction callback = null)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        while (!ao.isDone)
        {
            EventCenter.Instance.EventTrigger<float>("SceneLoading", ao.progress);
            yield return 0;
        }
        EventCenter.Instance.EventTrigger<float>("SceneLoading", 1.0f);
        callback?.Invoke();
    }

}
