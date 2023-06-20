using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem;
using UnityEngine;
using TMPro;

public class TextBox : Singleton<TextBox>
{
    [SerializeField] private TextMeshProUGUI textBox;
    private string speakerName;
    [SerializeField] private TextMeshProUGUI speakerText;
    private TMP_FontAsset currentFont;
    
    private void Start()
    {
        DialogManager.Instance.OnDialogStart += DialogueStart;
        DialogManager.Instance.OnDialogEnd += DialogueEnd;
        DialogManager.Instance.OnTextChanged += TextChange;
        DialogManager.Instance.OnSpeakerChanged += SpeakerChange;
        
        gameObject.SetActive(false);
    }

    private void DialogueStart()
    {
        textBox.text = null;
        gameObject.SetActive(true);
        Inventory.Instance.gameObject.SetActive(false);
    }

    private void DialogueEnd()
    {
        gameObject.SetActive(false);
    }

    private void TextChange(string text)
    {
        textBox.text = text;
    }

    private void SpeakerChange(string text)
    {
        currentFont = Resources.Load<TMP_FontAsset>("Fonts/" + text);
        textBox.font = currentFont;
        speakerText.font = currentFont;
        
        speakerText.text = text;
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
