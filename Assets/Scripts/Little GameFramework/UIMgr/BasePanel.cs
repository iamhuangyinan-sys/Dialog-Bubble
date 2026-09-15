using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI面板基类
/// </summary>
public abstract class BasePanel : MonoBehaviour
{
    //存储所有UI控件的字典
    protected Dictionary<string, UIBehaviour> controlDic = new Dictionary<string, UIBehaviour>();

    //所有默认控件名字（当控件没有被重命名时，意味着我们不需要使用这个控件）
    private static List<string> defaultNameList = new List<string>() { "Image",
                                                                   "Text (TMP)",
                                                                   "RawImage",
                                                                   "Background",
                                                                   "Checkmark",
                                                                   "Label",
                                                                   "Text (Legacy)",
                                                                   "Arrow",
                                                                   "Placeholder",
                                                                   "Fill",
                                                                   "Handle",
                                                                   "Viewport",
                                                                   "Scrollbar Horizontal",
                                                                   "Scrollbar Vertical"};

    /// <summary>
    /// 在Awake中查找组件
    /// </summary>
    protected virtual void Awake()
    {
        //优先查找重要的组件
        FindChildrenControl<Button>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<InputField>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<Dropdown>();
        //次要组件
        FindChildrenControl<Text>();
        FindChildrenControl<TextMeshPro>();
        FindChildrenControl<Image>();
        FindChildrenControl<TextMeshProUGUI>();
    }

    /// <summary>
    /// 显示面板（抽象函数）
    /// </summary>
    public abstract void Show();

    /// <summary>
    /// 隐藏面板（抽象函数）
    /// </summary>
    public abstract void Hide();

    /// <summary>
    /// 获取指定名字及类型的组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="name">组件名称</param>
    /// <returns>获取的组件</returns>
    public T GetControl<T>(string name) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(name))
        {
            T control = controlDic[name] as T;
            if (control == null)
                Debug.LogError($"不存在类型为{typeof(T)}的组件");
            return control;
        }
        else
        {
            Debug.LogError($"不存在名称为{name}的组件");
            return null;
        }
    }

    /// <summary>
    /// 按钮点击后执行的函数（要在子类中重写）
    /// </summary>
    /// <param name="btnName">按钮名称</param>
    protected virtual void ClickButton(string btnName) { }

    /// <summary>
    /// 滑动条滑动后执行的函数（要在子类中重写）
    /// </summary>
    /// <param name="sliderName">滑动条名称</param>
    /// <param name="value">滑动条的值</param>
    protected virtual void SliderValueChange(string sliderName, float value) { }

    /// <summary>
    /// 勾选框点击后执行的函数（要在子类中重写）
    /// </summary>
    /// <param name="sliderName">勾选框名称</param>
    /// <param name="value">勾选框是否勾选</param>
    protected virtual void ToggleValueChange(string sliderName, bool value) { }

    /// <summary>
    /// 查找控件并将其添加到字典中 并增加适当的监听
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>() where T : UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>(true);
        for (int i = 0; i < controls.Length; i++)
        {
            string controlName = controls[i].gameObject.name;
            if (!controlDic.ContainsKey(controlName))
            {
                if (!defaultNameList.Contains(controlName))
                {
                    controlDic.Add(controlName, controls[i]);

                    //判断控件的类型 决定是否加事件监听
                    if (controls[i] is Button)
                    {
                        (controls[i] as Button).onClick.AddListener(() =>
                        {
                            ClickButton(controlName);
                        });
                    }
                    else if (controls[i] is Slider)
                    {
                        (controls[i] as Slider).onValueChanged.AddListener((value) =>
                        {
                            SliderValueChange(controlName, value);
                        });
                    }
                    else if (controls[i] is Toggle)
                    {
                        (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                        {
                            ToggleValueChange(controlName, value);
                        });
                    }
                }

            }
        }
    }

}
