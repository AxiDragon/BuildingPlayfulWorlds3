using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour
{
    CanvasGroup blackFade;

    void Start()
    {
        blackFade = GetComponent<CanvasGroup>();
        StartCoroutine(Fade(true, 6f, null));
    }

    public IEnumerator Fade(bool fadingIn, float fadeTime, Action actionAfter)
    {
        float targetAlpha = fadingIn ? 0f : 1f;
        float startAlpha = fadingIn ? 1f : 0f;

        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            blackFade.alpha = Mathf.SmoothStep(startAlpha, targetAlpha, t / fadeTime);
            yield return null;
        }

        if (actionAfter != null)
            actionAfter();
    }
}
