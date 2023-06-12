using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private Canvas myCanvas;
    private CanvasGroup blackScreen;

    public float waitDuration;
    public float fadeDuration;

    private void Awake()
    {
        myCanvas = GetComponent<Canvas>();
        blackScreen = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(FadeOut(waitDuration, fadeDuration));
    }

    public void ActivateLoadingScreen()
    {
        StartCoroutine(FadeOut(waitDuration, fadeDuration));
    }
    
    private IEnumerator FadeOut(float waitDuration, float fadeDuration)
    {
        myCanvas.sortingOrder = 10;
        blackScreen.alpha = 1;
        yield return new WaitForSeconds(waitDuration);
        
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            blackScreen.alpha = Mathf.Lerp(1,0,t / fadeDuration);
            
            yield return null;
        }
        blackScreen.alpha = 0;
        myCanvas.sortingOrder = 0;
    }
}
