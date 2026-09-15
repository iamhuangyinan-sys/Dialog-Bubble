using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 不挂载的单例模式基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : class
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //利用反射得到无参私有的构造函数 来用于对象的实例化
                Type type = typeof(T);
                ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                            null,
                                                            Type.EmptyTypes,
                                                            null);
                if (info != null)
                    instance = info.Invoke(null) as T;
                else
                    Debug.LogError("没有得到对应的无参构造函数");
            }

            return instance;
        }
    }
}
