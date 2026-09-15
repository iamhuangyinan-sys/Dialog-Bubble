using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1FadingControl : MonoBehaviour
{
    public CanvasGroup blackScreenCanvasGroup; // 黑屏 UI
    public float fadeDuration = 1f;            // 淡入淡出时间
    private bool isFading = false;             // 控制淡入淡出

    void Start()
    {
        blackScreenCanvasGroup.alpha = 1f; // 初始黑屏完全遮挡
        StartCoroutine(FadeInAndStartGame()); // 开始时淡入黑屏，开始游戏
    }

    // 渐变黑屏（淡入效果）
    IEnumerator FadeInAndStartGame()
    {
        isFading = true;
        float timer = 0f;
        while (timer <= fadeDuration)
        {
            blackScreenCanvasGroup.alpha = 1f - (timer / fadeDuration); // 渐渐淡出
            timer += Time.deltaTime;
            yield return null;
        }
        blackScreenCanvasGroup.alpha = 0f; // 完全淡出
        isFading = false;

        // 这里可以执行游戏的其他初始化操作
    }

    // 可以在需要时使用淡出黑屏的效果，例如游戏结束时
    public void FadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadSceneCoroutine(sceneName));
    }

    // 渐变黑屏淡出并进入下一个场景
    IEnumerator FadeOutAndLoadSceneCoroutine(string sceneName)
    {
        isFading = true;
        float timer = 0f;
        while (timer <= fadeDuration)
        {
            blackScreenCanvasGroup.alpha = timer / fadeDuration; // 渐渐变黑
            timer += Time.deltaTime;
            yield return null;
        }
        blackScreenCanvasGroup.alpha = 1f; // 完全黑屏
        isFading = false;

        // 加载下一个场景
        SceneManager.LoadScene(sceneName);
    }
}