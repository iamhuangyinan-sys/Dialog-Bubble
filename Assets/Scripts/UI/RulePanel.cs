using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulePanel : BasePanel
{
    public override void Show() { }

    public override void Hide() { }

    protected override void ClickButton(string btnName)
    {
        if (btnName == "CloseButton")
        {
            UIMgr.Instance.HidePanel<RulePanel>();
        }
    }
}
