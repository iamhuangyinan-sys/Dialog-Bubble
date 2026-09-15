using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel2 : MonoBehaviour
{
    private void Start()
    {
        SoundMgr.Instance.PlaySound("¿ªÊ¼ÓÎÏ·");

        BubbleMgr.Instance.Initialize();
        List<BubbleElement> list = new List<BubbleElement>();
        list.Add(new BubbleElement(E_ElementType.empty));
        list.Add(new BubbleElement(E_ElementType.no));
        BubbleMgr.Instance.LevelStart(list, null, null);

        UIMgr.Instance.ShowPanel<GamePanel>();
    }
}
