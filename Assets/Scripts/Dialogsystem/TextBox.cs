using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem;
using UnityEngine;
using TMPro;

public class TextBox : MonoBehaviour
{
    private TextMeshProUGUI textBox;
    private Canvas textBoxCanvas;
    
    private void Awake()
    {
        textBox = GetComponentInChildren<TextMeshProUGUI>();
        textBoxCanvas = GetComponent<Canvas>();
        
        DialogManager.Instance.OnDialogStart += DialogueStart;
        DialogManager.Instance.OnDialogEnd += DialogueEnd;
        DialogManager.Instance.OnTextChanged += TextChange;
        DialogManager.Instance.OnSpeakerChanged += SpeakerChange;
    }

    private void Start()
    {
        textBoxCanvas.sortingOrder = 0;
    }

    private void DialogueStart()
    {
        textBox.text = null;
        textBoxCanvas.sortingOrder = 9;
    }

    private void DialogueEnd()
    {
        textBoxCanvas.sortingOrder = 0;
    }

    private void TextChange(string text)
    {
        textBox.text = text;
    }

    private void SpeakerChange(string text)
    {
        
    }

    /*public IEnumerator PlayDialogue(List<int> pidList)
    {
        foreach (int pid in pidList)
        {
            Instance.StartDialog(pid);
            
            while (!Input.GetMouseButtonDown(0))
                yield return null;
        }
        yield return null;
    }*/
}
