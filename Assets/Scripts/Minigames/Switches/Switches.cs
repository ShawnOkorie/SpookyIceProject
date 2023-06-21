using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Switches : Singleton<Switches>, IMinigames
{
    public delegate void MinigameFail(bool solved);
    public event MinigameFail OnMinigameEnd;

    [SerializeField] private Canvas myCanvas;
    [SerializeField] private Toggle[] myToggles;
    [SerializeField] private bool[] solvingValues;
    
    private Image myImage;
    [SerializeField] private Sprite uvSprite;
    
    
    private Image toggleImage;
    [SerializeField] private Sprite trueSprite;
    [SerializeField] private Sprite falseSprite;
    [SerializeField] private float waitTime = 2;

    public void StartMinigame(int difficulty, int timeLimit)
    {
        myImage = GetComponentInChildren<Image>();
        
        if (ProgressManager.Instance.ContainsProgress(ProgressManager.Progress.UVLampUsed))
        {
            myImage.sprite = uvSprite;
        }
        
        myCanvas.gameObject.SetActive(true);
    }

    public void ExitCanvas()
    {
        myCanvas.gameObject.SetActive(false);
    }

    public void ChangeValue(Toggle toggle)
    {
        toggleImage = toggle.GetComponentInChildren<Image>();
        Switch currentSwitch = toggle.GetComponent<Switch>();

        switch (toggle.isOn)
        {
            case true:
                toggleImage.sprite = trueSprite;
                currentSwitch.bgTransform.transform.position = currentSwitch.startPos;
                break;
            case false:
                toggleImage.sprite = falseSprite;
                currentSwitch.bgTransform.transform.position += new Vector3(0,-1f);
                break;
        }
        
        if (CheckIfSolved())
        {
            StartCoroutine(OnSolved());
        }
    }
    
    private bool CheckIfSolved()
    {
        for (int i = 0; i < myToggles.Length; i++)
        {
            if (myToggles[i].isOn != solvingValues[i])
                return false;
        }
        return true;
    }

    private IEnumerator OnSolved()
    {
        
        //play sound
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForSeconds(waitTime);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnMinigameEnd?.Invoke(true);
            ExitCanvas();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
