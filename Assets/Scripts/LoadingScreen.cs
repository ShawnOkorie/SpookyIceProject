using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private Canvas myCanvas;
    private CanvasGroup blackScreen;

    public float waitDuration;
    public float fadeDuration;

    public delegate void FadeStart();
    public static event FadeStart onFadeStart;
    
    public delegate void FadeEnd();
    public static event FadeEnd onFadeEnd;
    
    private void Awake()
    {
        myCanvas = GetComponent<Canvas>();
        blackScreen = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(gameObject);
    }
    

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn(waitDuration, fadeDuration));
    }
    
    private IEnumerator FadeIn(float waitDuration, float fadeDuration)
    {
        onFadeStart?.Invoke();
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
        onFadeEnd?.Invoke();
    }
    
    private IEnumerator FadeOut(float waitDuration, float fadeDuration)
    {
        onFadeStart?.Invoke();
        blackScreen.alpha = 0;
        myCanvas.sortingOrder = 10;
        
        yield return new WaitForSeconds(waitDuration);
        
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            blackScreen.alpha = Mathf.Lerp(0,1,t / fadeDuration);
            
            yield return null;
        }
        blackScreen.alpha = 1;
        onFadeEnd?.Invoke();
    }
}
