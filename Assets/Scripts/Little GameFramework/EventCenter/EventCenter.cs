using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 用于事件信息子类的父类
/// </summary>
public abstract class EventInfoBase { }

/// <summary>
/// 事件信息类 封装了所有函数
/// </summary>
/// <typeparam name="T">参数类型</typeparam>
public class EventInfo<T> : EventInfoBase
{
    //记录了所有函数的委托
    public UnityAction<T> actions;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="action">初始函数</param>
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

/// <summary>
/// 事件信息类 无参数
/// </summary>
public class EventInfo : EventInfoBase
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

/// <summary>
/// 事件中心模块
/// </summary>
public class EventCenter : Singleton<EventCenter>
{
    private EventCenter() { }

    //存储所有事件监听的字典
    private Dictionary<string, EventInfoBase> eventDic = new Dictionary<string, EventInfoBase>();

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <typeparam name="T">参数类型</typeparam>
    public void EventTrigger<T>(string eventName, T info)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions?.Invoke(info);
        }
    }

    /// <summary>
    /// 触发事件 无参数
    /// </summary>
    /// <param name="eventName">事件名称</param>
    public void EventTrigger(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).actions?.Invoke();
        }
    }

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">添加的函数</param>
    /// <typeparam name="T">参数类型</typeparam>
    public void AddEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(eventName, new EventInfo<T>(null));
            (eventDic[eventName] as EventInfo<T>).actions += action;
        }
    }

    /// <summary>
    /// 添加事件监听 无参数
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">添加的函数</param>
    public void AddEventListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(eventName, new EventInfo(null));
            (eventDic[eventName] as EventInfo).actions += action;
        }
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">移除的函数</param>
    /// <typeparam name="T">参数类型</typeparam>
    public void RemoveEventListener<T>(string eventName, UnityAction<T> action = null)
    {
        if(eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions -= action;
        }
    }

    /// <summary>
    /// 移除事件监听 无参数
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">移除的函数</param>
    public void RemoveEventListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).actions -= action;
        }
    }

    /// <summary>
    /// 移除所有事件监听
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }

    /// <summary>
    /// 移除某个事件监听
    /// </summary>
    /// <param name="eventName">事件名称</param>
    public void Clear(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            eventDic.Remove(eventName);
        }
    }
}
