using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSelect : MonoBehaviour
{
    /*
    //鼠标放在物体上物体放大
    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * 0.2f;
    }

    //鼠标离开物体物体缩小
    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * 0.2f;
    }
    */

    private Vector3 OnScale;
    private Vector3 originalScale; // 记录原始大小
    private void Start()
    {
        OnScale = transform.localScale  * 1.1f;
        originalScale = transform.localScale;
    }
    private void OnTriggerEnter(Collider collision)
    {
        // 当碰撞发生时，放大物体
        transform.localScale = OnScale;
    }
    private void OnTriggerExit(Collider collision)
    {
        // 当碰撞结束时，恢复原始大小
        transform.localScale = originalScale;
    }
}