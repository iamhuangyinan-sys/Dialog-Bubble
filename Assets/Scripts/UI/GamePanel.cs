using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePanel : BasePanel
{
    public override void Show()
    {
        TextMeshProUGUI targetText = GetControl<TextMeshProUGUI>("TargetText");
        Debug.Log(targetText.text);
        if (BubbleMgr.Instance.GetSceneName() == "Level1")
        {
            targetText.text = "让小明笑出来";
        }
        else if (BubbleMgr.Instance.GetSceneName() == "Level2")
        {
            targetText.text = "让右边的小孩得到灰色颜料";
        }
    }

    public override void Hide() { }

    protected override void ClickButton(string btnName)
    {
        if (btnName == "SettingButton")
        {
            SoundMgr.Instance.PlaySound("点击声音");
            UIMgr.Instance.ShowPanel<MenuPanel>();

        }
    }
}
