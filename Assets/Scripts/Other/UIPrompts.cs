using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrompts : MonoBehaviour
{
    public static Text[] texts;
    static CanvasGroup group;
    static float fadeTime = 1f;
    public static bool fadeIn = false;

    void Start()
    {
        texts = GetComponentsInChildren<Text>();
        group = GetComponent<CanvasGroup>();
    }

    void Update() => group.alpha += fadeIn ? (1f * Time.deltaTime) / fadeTime : (-1f * Time.deltaTime) / fadeTime;


    public static void UpdateUIText(string promptText)
    {
        foreach (Text prompt in texts)
            prompt.text = promptText;
    }

    void OnDestroy()
    {
        group = null;
        texts = null;
        fadeIn = false;
    }
}

    //public static IEnumerator UIFade(bool fadingIn, string promptText)
    //{
    //    if (fadingIn)
    //        foreach (Text prompt in texts)
    //            prompt.text = promptText;

    //    float targetAlpha = fadingIn ? 1f : 0f;
    //    float startAlpha = fadingIn ? 0f : 1f;

    //    for (float t = 0f; t < fadeTime; t += Time.deltaTime)
    //    {
    //        group.alpha = Mathf.SmoothStep(startAlpha, targetAlpha, t / fadeTime);
    //        yield return null;
    //    }
    //}