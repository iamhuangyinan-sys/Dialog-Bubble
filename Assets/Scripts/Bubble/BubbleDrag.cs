using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;
using Unity.Mathematics;

//using System.Numerics;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.SceneManagement;

public class BubbleDrag : MonoBehaviour
{
    private Vector3 StartPos;
    private Vector3 StartLocalPos;
    //BubbleMgr.Instance.currentScene = SceneManager.GetActiveScene().name

    //鼠标拖拽物体
    private void OnMouseDrag()
    {
        Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseWorldPos.z = Camera.main.farClipPlane;
        transform.position = new Vector3(MouseWorldPos.x, MouseWorldPos.y, MouseWorldPos.z - 10f);
    }

    private void OnMouseDown()
    {
        StartPos = gameObject.transform.position;
        StartLocalPos = gameObject.transform.localPosition;
    }

    //鼠标放在物体上物体放大
    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * 0.025f;
    }

    //鼠标离开物体物体缩小
    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * 0.025f;
    }

    private void OnMouseUp()
    {
        Debug.Log(BubbleMgr.Instance.TalkElementObj);
        //如果移到下面
        if (gameObject.transform.position.y < -2f)
        {
            Debug.Log("1移到下面");
            //如果一开始就在下面
            if (StartPos.y < -2f)
            {
                //回去
                transform.position = StartPos;
                Debug.Log("1.1一开始就在下面");
            }
            //是从上面移下去的
            else
            {
                //移到下面
                transform.SetParent(BubbleMgr.Instance.inventory.transform);
                transform.localPosition = transform.localPosition = new Vector3(-5.5f + BubbleMgr.Instance.InventoryListAdd(), 0, -1);
                SoundMgr.Instance.PlaySound("放置成功");
                Debug.Log("1.2是从上面移下去的");
                //如果是从AiBubble移下去的
                if (math.abs(BubbleMgr.Instance.aiBubble.transform.position.x - StartPos.x) < 3f &&
                    math.abs(BubbleMgr.Instance.aiBubble.transform.position.y - StartPos.y) < 3f)
                {
                    //Destroy(BubbleMgr.Instance.TalkElementObj);
                    if (BubbleMgr.Instance.GetSceneName() == "Level1")
                    {
                        BubbleMgr.Instance.TalkElementObj = null;
                    }
                    if (BubbleMgr.Instance.GetSceneName() == "Level2")
                    {
                        BubbleMgr.Instance.TalkElementObj2 = null;
                    }
                    Debug.Log("1.2.1从AiBubble移下去的");
                }
                //如果是从PlayerBubble移下去的
                else if (math.abs(BubbleMgr.Instance.playerBubble.transform.position.x - StartPos.x) < 3f &&
                         math.abs(BubbleMgr.Instance.playerBubble.transform.position.y - StartPos.y) < 3f)
                {
                    Debug.Log("1.2.2从PlayerBubble移下去的");
                    //从左边移走的
                    if (StartLocalPos.x < 0f)
                    {
                        Debug.Log("11111111111111" + BubbleMgr.Instance.TalkElementObj);
                        Debug.Log("1.2.2.1从左边移走的");
                        BubbleMgr.Instance.RemoveHeadBubbleElement();
                        if (BubbleMgr.Instance.GetSceneName() == "Level1")
                        {
                            Debug.Log("1Talk");
                            BubbleMgr.Instance.Talk();
                        }
                        if (BubbleMgr.Instance.GetSceneName() == "Level2")
                        {
                            Debug.Log("1Talk2");
                            BubbleMgr.Instance.Talk2();
                        }
                        BubbleMgr.Instance.PlayerIsPlaceLeft = false;
                    }
                    //从右边移走的
                    else if (StartLocalPos.x > 0f)
                    {
                        Debug.Log("1.2.2.2从右边移走的");
                        BubbleMgr.Instance.RemoveTailBubbleElement();
                        if (BubbleMgr.Instance.GetSceneName() == "Level1")
                        {
                            Debug.Log("2Talk");
                            BubbleMgr.Instance.Talk();
                        }
                        if (BubbleMgr.Instance.GetSceneName() == "Level2")
                        {
                            Debug.Log("2Talk2");
                            BubbleMgr.Instance.Talk2();
                        }
                        BubbleMgr.Instance.PlayerIsPlaceRight = false;
                    }
                }
            }
        }
        else
        {
            Debug.Log("2.不移到下面");
            //如果移到PlayerBubble左边
            if (BubbleMgr.Instance.playerBubble.transform.position.x - transform.position.x > 0f &&
                math.abs(BubbleMgr.Instance.playerBubble.transform.position.y - transform.position.y) < 3f)
            {
                Debug.Log("2.1移到左边");
                if (BubbleMgr.Instance.PlayerIsPlaceLeft == true || this.gameObject.name == "question" || this.gameObject.name == "Command")
                {
                    transform.position = StartPos;
                }
                else
                {
                    SoundMgr.Instance.PlaySound("放置成功");
                    if (math.abs(BubbleMgr.Instance.aiBubble.transform.position.x - StartPos.x) < 3f &&
                    math.abs(BubbleMgr.Instance.aiBubble.transform.position.y - StartPos.y) < 3f)
                    {

                        if (BubbleMgr.Instance.GetSceneName() == "Level1")
                        {
                            BubbleMgr.Instance.TalkElementObj = null;
                        }
                        if (BubbleMgr.Instance.GetSceneName() == "Level2")
                        {
                            BubbleMgr.Instance.TalkElementObj2 = null;
                        }
                        ;
                        Debug.Log("1.2.1从AiBubble移去左边");
                    }
                    else
                    {
                        BubbleMgr.Instance.InventoryListRemove((int)(StartLocalPos.x - (-5.5f)));
                    }
                    Debug.Log("2.1移到PlayerBubble左边");
                    transform.SetParent(BubbleMgr.Instance.playerBubble.transform);
                    transform.localPosition = new Vector3(-4f, 1.4f, -1);
                    BubbleMgr.Instance.InsertBubbleElement(BubbleMgr.Instance.typeDic[gameObject.name]);
                    if (BubbleMgr.Instance.GetSceneName() == "Level1")
                    {
                        Debug.Log("3Talk");
                        BubbleMgr.Instance.Talk();
                    }
                    if (BubbleMgr.Instance.GetSceneName() == "Level2")
                    {
                        Debug.Log("3Talk2");
                        BubbleMgr.Instance.Talk2();
                    }
                    BubbleMgr.Instance.PlayerIsPlaceLeft = true;
                }
            }
            //如果移到PlayerBubble右边
            else if (BubbleMgr.Instance.playerBubble.transform.position.x - transform.position.x < 0f &&
                math.abs(BubbleMgr.Instance.playerBubble.transform.position.y - transform.position.y) < 3f)
            {
                Debug.Log("2.1移到右边");
                if (BubbleMgr.Instance.PlayerIsPlaceRight == false)
                {
                    if (this.gameObject.name != "question" || this.gameObject.name != "command")
                    {
                        SoundMgr.Instance.PlaySound("放置成功");
                        if (math.abs(BubbleMgr.Instance.aiBubble.transform.position.x - StartPos.x) < 3f &&
                        math.abs(BubbleMgr.Instance.aiBubble.transform.position.y - StartPos.y) < 3f)
                        {
                            if (BubbleMgr.Instance.GetSceneName() == "Level1")
                            {
                                BubbleMgr.Instance.TalkElementObj = null;
                            }
                            if (BubbleMgr.Instance.GetSceneName() == "Level2")
                            {
                                BubbleMgr.Instance.TalkElementObj2 = null;
                            }
                            Debug.Log("1.2.1从AiBubble移去右边");
                        }
                        else
                        {
                            BubbleMgr.Instance.InventoryListRemove((int)(StartLocalPos.x - (-5.5f)));
                        }
                        Debug.Log("2.2移到PlayerBubble右边");
                        transform.SetParent(BubbleMgr.Instance.playerBubble.transform);
                        transform.localPosition = new Vector3(4f, 1.4f, -1);
                        BubbleMgr.Instance.AddBubbleElement(BubbleMgr.Instance.typeDic[gameObject.name]);
                        if (BubbleMgr.Instance.GetSceneName() == "Level1")
                        {
                            Debug.Log("4Talk");
                            BubbleMgr.Instance.Talk();
                        }
                        if (BubbleMgr.Instance.GetSceneName() == "Level2")
                        {
                            Debug.Log("4Talk2");
                            BubbleMgr.Instance.Talk2();
                        }
                        BubbleMgr.Instance.PlayerIsPlaceRight = true;
                    }
                    else
                    {
                        transform.position = StartPos;
                    }
                }
                else
                {
                    transform.position = StartPos;
                }

            }
        }

    }
}


