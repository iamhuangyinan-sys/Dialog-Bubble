using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 元素类
/// </summary>
public class BubbleElement
{
    public E_ElementType ElementType;

    public BubbleElement(E_ElementType elementType)
    {
        this.ElementType = elementType;
    }
}

/// <summary>
/// 元素的枚举
/// </summary>
public enum E_ElementType
{
    yes,
    no,
    question,
    happy,
    sad,
    dislike,

    dontknow,


    whilte,
    black,
    grey,
    empty,

}

public class BubbleMgr : SingletonAutoMono<BubbleMgr>
{
    private BubbleMgr() { }

    //气泡信息
    public List<BubbleElement> bubbleList;
    //关卡信息
    //public List<string> levelList = new List<string> { "StartLevel1", "StartLevel2" };
    public string currentScene;
    //记录Sprite的字典
    public Dictionary<string, Sprite> elementsDic = new Dictionary<string, Sprite>();
    public Dictionary<string, BubbleElement> typeDic = new Dictionary<string, BubbleElement>();

    public GameObject playerBubble;
    public GameObject aiBubble;
    public GameObject inventory;

    public GameObject TalkElementObj = null;

    //关卡2的返回结果
    public GameObject TalkElementObj2 = null;

    public List<bool> inventoryList = new List<bool>();

    public bool PlayerIsPlaceLeft = false;
    public bool PlayerIsPlaceRight = false;

    //public bool f = false;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        bubbleList = new List<BubbleElement>();
        inventoryList.Clear();
        TalkElementObj = null;
        TalkElementObj2 = null;
        PlayerIsPlaceLeft = false;
        PlayerIsPlaceRight = false;
        //f = false;

        elementsDic.Clear();
        elementsDic.Add("yes", ResourcesMgr.Instance.Load<Sprite>("Icon/yes"));
        elementsDic.Add("no", ResourcesMgr.Instance.Load<Sprite>("Icon/no"));
        elementsDic.Add("question", ResourcesMgr.Instance.Load<Sprite>("Icon/question"));
        elementsDic.Add("dislike", ResourcesMgr.Instance.Load<Sprite>("Icon/dislike"));
        elementsDic.Add("sad", ResourcesMgr.Instance.Load<Sprite>("Icon/sad"));
        elementsDic.Add("happy", ResourcesMgr.Instance.Load<Sprite>("Icon/happy"));
        elementsDic.Add("dontknow", ResourcesMgr.Instance.Load<Sprite>("Icon/dontknow"));
        elementsDic.Add("whilte", ResourcesMgr.Instance.Load<Sprite>("Icon/whilte"));
        elementsDic.Add("black", ResourcesMgr.Instance.Load<Sprite>("Icon/black"));
        elementsDic.Add("grey", ResourcesMgr.Instance.Load<Sprite>("Icon/grey"));
        elementsDic.Add("empty", ResourcesMgr.Instance.Load<Sprite>("Icon/empty"));

        typeDic.Clear();
        typeDic.Add("yes", new BubbleElement(E_ElementType.yes));
        typeDic.Add("no", new BubbleElement(E_ElementType.no));
        typeDic.Add("question", new BubbleElement(E_ElementType.question));
        typeDic.Add("dislike", new BubbleElement(E_ElementType.dislike));
        typeDic.Add("sad", new BubbleElement(E_ElementType.sad));
        typeDic.Add("happy", new BubbleElement(E_ElementType.happy));
        typeDic.Add("dontknow", new BubbleElement(E_ElementType.dontknow));
        typeDic.Add("whilte", new BubbleElement(E_ElementType.whilte));
        typeDic.Add("black", new BubbleElement(E_ElementType.black));
        typeDic.Add("grey", new BubbleElement(E_ElementType.grey));
        typeDic.Add("empty", new BubbleElement(E_ElementType.empty));

        playerBubble = GameObject.Find("PlayerBubble");
        aiBubble = GameObject.Find("AiBubble");
        inventory = GameObject.Find("Inventory");
    }

    //关卡开始
    public void LevelStart(List<BubbleElement> cards, BubbleElement teatherElement, BubbleElement studentElement)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            inventoryList.Add(true);

            GameObject go = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Element"));
            go.transform.SetParent(inventory.transform);
            go.transform.localPosition = new Vector3(-5.5f + i, 0, -1);
            string elementName = cards[i].ElementType.ToString();
            go.GetComponent<SpriteRenderer>().sprite = elementsDic[elementName];
            go.name = elementName;
        }

        if (teatherElement != null)
        {
            GameObject tgo = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Element"));
            tgo.transform.SetParent(playerBubble.transform);
            tgo.transform.localPosition = new Vector3(-4f, 1.4f, -1);
            string telementName = teatherElement.ElementType.ToString();
            tgo.GetComponent<SpriteRenderer>().sprite = elementsDic[telementName];
            tgo.name = telementName;
            bubbleList.Add(typeDic[tgo.name]);

            PlayerIsPlaceLeft = true;
        }

        if (studentElement != null)
        {
            GameObject sgo = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Element"));
            if (GetSceneName() == "Level1")
            {
                TalkElementObj = sgo;
            }
            if (GetSceneName() == "Level2")
            {
                TalkElementObj2 = sgo;
            }

            sgo.transform.SetParent(aiBubble.transform);
            sgo.transform.localPosition = new Vector3(0, 1.4f, -1);
            string selementName = studentElement.ElementType.ToString();
            sgo.GetComponent<SpriteRenderer>().sprite = elementsDic[selementName];
            sgo.name = selementName;
        }

    }

    public int InventoryListAdd()
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (!inventoryList[i])
            {
                inventoryList[i] = true;
                return i;
            }
        }
        inventoryList.Add(true);
        return inventoryList.Count - 1;
    }

    public void InventoryListRemove(int i)
    {
        inventoryList[i] = false;
    }

    //添加气泡元素
    public void InsertBubbleElement(BubbleElement element)
    {
        bubbleList.Insert(0, element);
    }

    public void AddBubbleElement(BubbleElement element)
    {
        bubbleList.Add(element);
    }

    public void RemoveHeadBubbleElement()
    {
        bubbleList.RemoveAt(0);
    }

    public void RemoveTailBubbleElement()
    {
        bubbleList.RemoveAt(bubbleList.Count - 1);
    }

    private BubbleElement Infer()
    {
        if (bubbleList.Count == 0)
        {
            return null;
        }

        if (bubbleList.Count == 1)
        {
            if (bubbleList[0].ElementType == E_ElementType.no || bubbleList[0].ElementType == E_ElementType.dislike)
            {
                return new(E_ElementType.sad);
            }
            else
            {
                return new(E_ElementType.dontknow);
            }
        }
        else
        {
            if (bubbleList[0].ElementType == E_ElementType.no && bubbleList[1].ElementType == E_ElementType.sad)
            {
                return new(E_ElementType.happy);
            }
            else
            {
                return new(E_ElementType.dontknow);
            }
        }



        /*
        //如果是问句
        if (bubbleList[bubbleList.Count - 1].ElementType == E_ElementType.question)
        {
            //只有一个问号
            if (bubbleList.Count == 1)
            {
                return person;
            }
            else if (bubbleList.Count == 2)
            {
                if (bubbleList[0].ElementType == person.ElementType)
                {
                    return new BubbleElement(E_ElementType.yes);
                }
                else
                {
                    return new BubbleElement(E_ElementType.no);
                }
            }

            else
            {
                if (bubbleList[0].ElementType == bubbleList[1].ElementType)
                {
                    bubbleList.RemoveAt(0);
                    if (bubbleList[0].ElementType == E_ElementType.no)
                    {
                        bubbleList[0].ElementType = E_ElementType.yes;
                    }
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (bubbleList[i].ElementType == E_ElementType.yes)
                        {
                            bubbleList.RemoveAt(i);
                        }
                    }
                }

                if (bubbleList.Count == 3)
                {
                    int noIndex = -1;
                    for (int i = 0; i < 2; i++)
                    {
                        if (bubbleList[i].ElementType == E_ElementType.no)
                        {
                            noIndex = i;
                            bubbleList.RemoveAt(i);
                        }
                    }
                    if (noIndex != -1)
                    {
                        //反义词
                        if (bubbleList[1 - noIndex].ElementType == E_ElementType.happy)
                        {
                            bubbleList[1 - noIndex].ElementType = E_ElementType.sad;
                        }
                        if (bubbleList[1 - noIndex].ElementType == E_ElementType.sad)
                        {
                            bubbleList[1 - noIndex].ElementType = E_ElementType.happy;
                        }
                    }
                    else
                    {
                        return new BubbleElement(E_ElementType.question);
                    }
                }
                return Infer();
            }
        }
        */


    }

    private BubbleElement Infer2()
    {
        if (bubbleList.Count == 0)
        {
            return null;
        }

        if (bubbleList.Count == 1)
        {
            if (bubbleList[0].ElementType == E_ElementType.empty || bubbleList[1].ElementType == E_ElementType.empty)
            {
                return new(E_ElementType.whilte);
            }
            else
            {
                return new(E_ElementType.dontknow);
            }
        }
        else
        {
            if ((bubbleList[0].ElementType == E_ElementType.whilte && bubbleList[1].ElementType == E_ElementType.black) || 
            (bubbleList[1].ElementType == E_ElementType.whilte && bubbleList[0].ElementType == E_ElementType.black))
            {
                return new(E_ElementType.grey);
            }
            else if (bubbleList[0].ElementType == E_ElementType.no && bubbleList[1].ElementType == E_ElementType.whilte)
            {
                return new(E_ElementType.black);
            }
            else
            {
                return new(E_ElementType.dontknow);
            }
        }
    }
    //Infer结束

    public void Talk()
    {
        BubbleElement element = Infer();

        if (element == null)
        {
            return;
        }
        if (element.ElementType == E_ElementType.happy)
        {
            SoundMgr.Instance.PlaySound("游戏胜利");
            Invoke("DelayWin", 1.0f);
            

        }

        if (TalkElementObj == null)
        {

            Debug.Log("重新生成");
            TalkElementObj = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Element"));
            TalkElementObj.transform.SetParent(aiBubble.transform);
            //TalkElementObj.transform.localScale *= 0.75f;
            TalkElementObj.transform.localPosition = new Vector3(0, 1.4f, -1);
            string elementName = element.ElementType.ToString();
            TalkElementObj.GetComponent<SpriteRenderer>().sprite = elementsDic[elementName];
            TalkElementObj.name = elementName;
            TalkElementObj.SetActive(true);
        }
        else
        {
            Debug.Log("更换ICON");
            string elementName = element.ElementType.ToString();
            TalkElementObj.GetComponent<SpriteRenderer>().sprite = elementsDic[elementName];
            TalkElementObj.name = elementName;
        }
    }

    public void Talk2()
    {
        BubbleElement element = Infer2();

        if (element == null)
        {
            return;
        }
        if (element.ElementType == E_ElementType.grey)
        {
            SoundMgr.Instance.PlaySound("游戏胜利");
            Invoke("DelayOver", 1.0f);
        }

        if (TalkElementObj2 == null)
        {

            Debug.Log("重新生成");
            TalkElementObj2 = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Element"));
            TalkElementObj2.transform.SetParent(aiBubble.transform);
            //TalkElementObj.transform.localScale *= 0.75f;
            TalkElementObj2.transform.localPosition = new Vector3(0, 1.4f, -1);
            string elementName = element.ElementType.ToString();
            TalkElementObj2.GetComponent<SpriteRenderer>().sprite = elementsDic[elementName];
            TalkElementObj2.name = elementName;
            TalkElementObj2.SetActive(true);
        }
        else
        {
            Debug.Log("更换ICON");
            string elementName = element.ElementType.ToString();
            TalkElementObj2.GetComponent<SpriteRenderer>().sprite = elementsDic[elementName];
            TalkElementObj2.name = elementName;
        }
    }

    private void DelayWin()
    {
        UIMgr.Instance.ShowPanel<Tongguan>();
    }

    private void DelayOver()
    {
        UIMgr.Instance.ShowPanel<Jieshu>();
    }


    public string GetSceneName()
    {
        currentScene = SceneManager.GetActiveScene().name;
        return currentScene;
    }
}
