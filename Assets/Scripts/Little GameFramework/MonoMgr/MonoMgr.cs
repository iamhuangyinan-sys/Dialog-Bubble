using UnityEngine.Events;

/// <summary>
/// 公共Mono模块管理器
/// </summary>
public class MonoMgr : SingletonAutoMono<MonoMgr>
{
    private MonoMgr() { }

    private event UnityAction updateEvent;
    private event UnityAction fixedUpateEvent;
    private event UnityAction lateUpdateEvent;

    /// <summary>
    /// 添加Update帧更新监听函数
    /// </summary>
    /// <param name="action"></param>
    public void AddUpdateListener(UnityAction action)
    {
        updateEvent += action;
    }

    /// <summary>
    /// 移除Update帧更新监听函数
    /// </summary>
    /// <param name="action"></param>
    public void RemoveUpdateListener(UnityAction action)
    {
        updateEvent -= action;
    }

    /// <summary>
    /// 添加FixedUpdate帧更新监听函数
    /// </summary>
    /// <param name="action"></param>
    public void AddFixedUpdateListener(UnityAction action)
    {
        fixedUpateEvent += action;
    }

    /// <summary>
    /// 移除FixedUpdate帧更新监听函数
    /// </summary>
    /// <param name="action"></param>
    public void RemoveFixedUpdateListener(UnityAction action)
    {
        fixedUpateEvent -= action;
    }

    /// <summary>
    /// 添加LateUpdate帧更新监听函数
    /// </summary>
    /// <param name="action"></param>
    public void AddLateUpdateListener(UnityAction action)
    {
        lateUpdateEvent += action;
    }

    /// <summary>
    /// 移除LateUpdate帧更新监听函数
    /// </summary>
    /// <param name="action"></param>
    public void RemoveLateUpdateListener(UnityAction action)
    {
        lateUpdateEvent -= action;
    }

    private void Update()
    {
        updateEvent?.Invoke();
    }

    private void FixedUpdate()
    {
        fixedUpateEvent?.Invoke();
    }

    private void LateUpdate()
    {
        lateUpdateEvent?.Invoke();
    }
}
