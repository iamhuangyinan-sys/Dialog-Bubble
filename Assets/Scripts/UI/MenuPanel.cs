using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : BasePanel
{
    public override void Show() { }

    public override void Hide() { }

    protected override void ClickButton(string btnName)
    {
        if (btnName == "Back")
        {
            UIMgr.Instance.HidePanel<MenuPanel>();
            UIMgr.Instance.HidePanel<GamePanel>();
            SceneMgr.Instance.LoadSceneAsyn("StartPage");
            SoundMgr.Instance.PlaySound("µã»÷ÉùÒô");
            SoundMgr.Instance.ClearSounds();
            PoolMgr.Instance.ClearPool();
        }
        else if (btnName == "Guide")
        {
            SoundMgr.Instance.PlaySound("µã»÷ÉùÒô");
            UIMgr.Instance.ShowPanel<RulePanel>();
        }
        else if(btnName == "Quit")
        {
            SoundMgr.Instance.PlaySound("µã»÷ÉùÒô");
            Application.Quit();
        }
        else if (btnName == "Close")
        {
            SoundMgr.Instance.PlaySound("¹Ø±Õ²Ëµ¥");
            UIMgr.Instance.HidePanel<MenuPanel>();
        }
    }
}
