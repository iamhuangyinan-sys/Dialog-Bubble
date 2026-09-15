using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel1 : MonoBehaviour
{
    private void Start()
    {
        SoundMgr.Instance.PlaySound("¿ªÊ¼ÓÎÏ·");
        BubbleMgr.Instance.Initialize();
        List<BubbleElement> list = new List<BubbleElement>();
        list.Add(new BubbleElement(E_ElementType.no));
        BubbleElement tEle = new BubbleElement(E_ElementType.dislike);
        BubbleElement sEle = new BubbleElement(E_ElementType.sad);

        BubbleMgr.Instance.LevelStart(list, tEle, sEle);

        UIMgr.Instance.ShowPanel<GamePanel>();
    }
}
