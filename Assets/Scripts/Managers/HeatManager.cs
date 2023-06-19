using System;
using System.Collections;
using System.Collections.Generic;
using Minigames;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class HeatManager : Singleton<HeatManager>
{
    [SerializeField] private List<Canvas> mingameCanvasList = new List<Canvas>();
    [SerializeField] private Button respawnButton;
    [SerializeField] private TextMeshProUGUI gameOverText;
    
    [SerializeField] private float timeLimit = 300;
    public float currentTimer => timer;
    private float timer;
    private bool timerIsZero;
   
    private float freezePercent;
    [SerializeField] private List<Image> freezingStages = new List<Image>();
    [SerializeField] private float fadeDuration;
    [SerializeField] private float waitDuration;

    private AudioSource myAudioSource;
    [SerializeField] private AudioClip newStageSound;
    [SerializeField] private AudioClip deathSound;
    
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        ResetTimer();
    }
    
    private void Update()
    {
        //return if cutscene canvas active

        foreach (Canvas canvas in mingameCanvasList)
        {
            if (canvas.gameObject.activeSelf)
            {
                return;
            }
        }
        
        if (timerIsZero == false)
        {
            timer -= Time.deltaTime;
        }
        
        if (timer <= 0 && timerIsZero == false)
        {
            TimerEnded();
        }

        freezePercent =  timer / timeLimit;

        //print(freezePercent);
        
        if (freezePercent <= 0)
        {
            ChangeImage(3);
        }
        else if (freezePercent <= 0.15f)
        {
            ChangeImage(2);
        }
        else if (freezePercent <= 0.3f)
        {
            ChangeImage(1);
        }
        else if (freezePercent <= 0.5f)
        {
            ChangeImage(0);
        }
    }

    private void ChangeImage(int index)
    {
        if (index == 3)
        {
            myAudioSource.PlayOneShot(deathSound);
        }
        else
        {
            myAudioSource.PlayOneShot(newStageSound);
        }

        for (int i = 0; i < freezingStages.Count; i++)
        {
            if (index != i)
            {
                freezingStages[i].gameObject.SetActive(false);
            }
            else
            {
                freezingStages[i].gameObject.SetActive(true);
            }
        }
    }
    
    
    private void TimerEnded()
    {
        StartCoroutine(FadeOut(fadeDuration,waitDuration));
    }

    public void ResetTimer()
    {
        timer = timeLimit;
        foreach (Image image in freezingStages)
        {
            image.gameObject.SetActive(false);
        }
    }
    
    private IEnumerator FadeOut(float waitDuration, float fadeDuration)
    {
        timerIsZero = true;
        gameOverText.gameObject.SetActive(false);
        respawnButton.gameObject.SetActive(false);
        
        CanvasGroup canvasGroup = freezingStages[3].GetComponent<CanvasGroup>(); 
        canvasGroup.alpha = 0.7f;
        yield return new WaitForSeconds(waitDuration);
        
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0.7f,1,t / fadeDuration);

            yield return null;
        }
        canvasGroup.alpha = 1;
        
        gameOverText.gameObject.SetActive(true);
        respawnButton.gameObject.SetActive(true);
    }
}
