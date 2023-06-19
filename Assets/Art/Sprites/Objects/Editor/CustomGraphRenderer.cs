using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GraphRenderer))]

public class CustomGraphRenderer :  Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GraphRenderer script = (GraphRenderer) target;

        script.Draw();
    }
}
