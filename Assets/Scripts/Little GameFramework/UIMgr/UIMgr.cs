using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 层级父对象的枚举
/// </summary>
public enum E_UILayer
{
    //最底层
    Bottom,
    //中间层
    Middle,
    //顶层
    Top,
    //系统层（最高层）
    System,
}

public class UIMgr : Singleton<UIMgr>
{
    private Canvas uiCanvas;
    private EventSystem uiEventSystem;

    //存储层级父对象
    Transform buttomLayer;
    Transform middleLayer;
    Transform topLayer;
    Transform systemLayer;

    private UIMgr()
    {
        uiCanvas = GameObject.Instantiate(ResourcesMgr.Instance.Load<GameObject>("UIMgr/Canvas")).GetComponent<Canvas>();
        GameObject.DontDestroyOnLoad(uiCanvas.gameObject);
        uiCanvas.gameObject.name = "Canvas";
        uiEventSystem = GameObject.Instantiate(ResourcesMgr.Instance.Load<GameObject>("UIMgr/EventSystem")).GetComponent<EventSystem>();
        GameObject.DontDestroyOnLoad(uiEventSystem.gameObject);
        uiEventSystem.gameObject.name = "EventSystem";

        buttomLayer = uiCanvas.transform.Find("Bottom");
        middleLayer = uiCanvas.transform.Find("Middle");
        topLayer = uiCanvas.transform.Find("Top");
        systemLayer = uiCanvas.transform.Find("System");
    }

    Dictionary<string,BasePanel> panelDic = new Dictionary<string,BasePanel>();

    /// <summary>
    /// 通过枚举得到层级父对象
    /// </summary>
    /// <param name="layer">层级父对象的枚举</param>
    /// <returns>层级父对象</returns>
    public Transform GetLayer(E_UILayer layer)
    {
        switch (layer)
        {
            case E_UILayer.Bottom:
                return buttomLayer;
            case E_UILayer.Middle:
                return middleLayer;
            case E_UILayer.Top:
                return topLayer;
            case E_UILayer.System:
                return systemLayer;
            default: 
                return null;
        }
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">面板对象挂载的脚本名称（同时也是面板对象的名称）</typeparam>
    /// <param name="layer">层级父对象的枚举</param>
    /// <param name="callBack">回调函数</param>
    public void ShowPanel<T>(E_UILayer layer = E_UILayer.Middle, UnityAction<T> callBack = null)where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if(panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].Show();
            callBack?.Invoke(panelDic[panelName] as T);
        }
        else
        {
            ResourcesMgr.Instance.LoadAsync<GameObject>("UI/" + panelName, (obj) =>
            {
                Transform trans = GetLayer(layer);
                if(trans !=  null)
                {
                    GameObject panelObj = GameObject.Instantiate(obj, trans, false);
                    panelObj.name = panelName;  
                    T panel = panelObj.GetComponent<T>();
                    panel.Show();
                    callBack?.Invoke(panel);
                    panelDic.Add(panelName, panel);
                }
            });
        }
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T">面板对象挂载的脚本名称（同时也是面板对象的名称）</typeparam>
    public void HidePanel<T>()where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].Hide();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// 获取面板
    /// </summary>
    /// <typeparam name="T">面板对象挂载的脚本名称（同时也是面板对象的名称）</typeparam>
    public T GetPanel<T>()where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        else
        {
            return null;
        }
    }
}
