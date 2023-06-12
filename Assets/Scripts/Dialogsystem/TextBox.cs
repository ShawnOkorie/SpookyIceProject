using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem;
using UnityEngine;

public class TextBox : Singleton<DialogManager>
{
    private void Awake()
    {
        Instance.OnDialogStart += DialogueStart;
        Instance.OnDialogEnd += DialogueEnd;
        Instance.OnTextChanged += TextChange;
        Instance.OnSpeakerChanged += SpeakerChange;
    }

    private void DialogueStart()
    {
        
    }

    private void DialogueEnd()
    {
        
    }

    private void TextChange(string text)
    {
        
    }

    private void SpeakerChange(string text)
    {
        
    }
}
