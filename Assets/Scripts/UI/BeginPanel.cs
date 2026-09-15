using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BeginPanel : BasePanel
{
    public CanvasGroup blackScreenCanvasGroup; // 黑屏 UI
    public float fadeDuration = 1f;            // 淡入时间
    private bool isFading = false;             // 控制淡入状态

    public override void Show()
    {
        blackScreenCanvasGroup = GetControl<Image>("Black").gameObject.GetComponent<CanvasGroup>();
        blackScreenCanvasGroup.alpha = 0f; // 初始不黑屏
    }

    public override void Hide()
    {
        MusicMgr.Instance.StopBGM();
    }

    protected override void ClickButton(string btnName)
    {
        if (btnName == "StartButton")
        {
            SoundMgr.Instance.ClearSounds();
            PoolMgr.Instance.ClearPool();
            StartCoroutine(FadeToBlack()); // 点击按钮后执行渐变黑
        }
        else if (btnName == "QuitButton")
        {
            Application.Quit();
        }

    }
    IEnumerator FadeToBlack()
    {
        Debug.Log("Fade");
        isFading = true;
        float timer = 0f;

        // 黑屏渐渐淡入
        while (timer <= fadeDuration)
        {
            Debug.Log("Fading");
            blackScreenCanvasGroup.alpha = timer / fadeDuration; // 渐渐变黑
            timer += Time.deltaTime;
            yield return null;
        }
        blackScreenCanvasGroup.alpha = 1f; // 完全黑屏
        isFading = false;


        // 异步加载下一个场景
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level1");
        asyncOperation.allowSceneActivation = false;  // 阻止场景自动激活

        // 等待场景加载完成
        while (!asyncOperation.isDone)
        {
            // 只要场景加载接近完成，就激活场景
            if (asyncOperation.progress >= 0.9f)
            {
                // 开始激活新场景
                asyncOperation.allowSceneActivation = true;
                UIMgr.Instance.HidePanel<BeginPanel>();
            }
            yield return null;
        }
    }
}
