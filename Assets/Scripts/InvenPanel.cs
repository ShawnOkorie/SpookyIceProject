using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenPanel : MonoBehaviour
{
    
    public RectTransform myRectTransform;
    private float move = 1000;
    private Vector3 startPos;
    private Toggle myToggle;


    private void Awake()
    {
       myToggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        startPos = myRectTransform.localPosition;
        myRectTransform.localPosition += new Vector3(move,0);
    }

    public void ShowInv()
    {
        switch (myToggle.isOn)
        {
            case true:
                myRectTransform.localPosition = startPos;
                break;
            case false:
                myRectTransform.localPosition += new Vector3(move,0);
                break;
        }
    }
}
