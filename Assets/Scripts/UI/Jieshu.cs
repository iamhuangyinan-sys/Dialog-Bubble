using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jieshu : BasePanel
{
    public override void Show() { }

    public override void Hide() { }

    protected override void ClickButton(string btnName)
    {
        if (btnName == "NextButton")
        {
            SoundMgr.Instance.ClearSounds();
            PoolMgr.Instance.ClearPool();
            UIMgr.Instance.HidePanel<Tongguan>();
            SceneMgr.Instance.LoadSceneAsyn("StartPage");
        }
    }
}
