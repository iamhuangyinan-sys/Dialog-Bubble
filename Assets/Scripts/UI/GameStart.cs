using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Start()
    {
        UIMgr.Instance.ShowPanel<BeginPanel>();
        MusicMgr.Instance.PlayBGM("主题曲BGM菜单界面");
    }
}
