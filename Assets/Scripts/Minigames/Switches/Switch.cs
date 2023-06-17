using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
   private Toggle myToggle;
   public RectTransform bgTransform;
   public Vector3 startPos;

   private void Start()
   {
      myToggle = GetComponent<Toggle>();

      bgTransform = myToggle.graphic.GetComponent<RectTransform>();

      startPos = bgTransform.transform.position;
   }
}
