using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    public float scrollSpeed = 200f;  // 控制平移速度
    private RectTransform rectTransform;
    private float imageWidth;
    private float canvasWidth;

    void Start()
    {
        // 获取该Image的RectTransform
        rectTransform = GetComponent<RectTransform>();
        imageWidth = rectTransform.rect.width;  // 获取图片宽度
        canvasWidth = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.width;  // 获取Canvas的宽度
    }

    void Update()
    {
        // 每帧移动图片
        rectTransform.anchoredPosition += new Vector2(-scrollSpeed * Time.deltaTime, 0);

        // 判断图片的右边缘是否接触到Canvas的右边缘
        if (rectTransform.anchoredPosition.x + imageWidth <= canvasWidth+960)
        {
            // 如果图片的右边缘接触到Canvas的右边缘，重置位置
            rectTransform.anchoredPosition = new Vector2(960, rectTransform.anchoredPosition.y);
        }
    }
}
