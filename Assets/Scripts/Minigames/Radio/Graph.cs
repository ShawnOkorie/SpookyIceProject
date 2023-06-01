using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Graph : MonoBehaviour
{
    private LineRenderer myLineRenderer;

    public GraphType graphType;
    public enum GraphType
    {
        Sin,
        Cos,
        Tan
    }
    
    public int points;
    public float amplitude  = 1;
    public float amplitudeProperty
    {
        get { return amplitude; }
        set { amplitude = value; }
    }
    public float amplitudeScalar;
    
    public float frequency = 1;
    public float frequencyProperty
    {
        get { return amplitude; }
        set { amplitude = value; }
    }
    public float movementSpeed = 1;
    
    
    public Vector2 xLimits = new Vector2(0,1);
    [Range(0,2*Mathf.PI)]
    public float radians;
    void Start()
    {
        myLineRenderer = GetComponent<LineRenderer>();
    }
    
    void Draw()
    {
        float xStart = xLimits.x;
        float Tau = 2* Mathf.PI;
        float xFinish = xLimits.y;
 
        myLineRenderer.positionCount = points;
        
        for(int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float)currentPoint/(points-1);
            float x = Mathf.Lerp(xStart,xFinish,progress);

            switch (graphType)
            {
                case GraphType.Sin:
                    float y1;
                    y1 = (amplitude * amplitudeScalar) * Mathf.Sin((Tau * frequency * x)+(Time.timeSinceLevelLoad * movementSpeed));
                    myLineRenderer.SetPosition(currentPoint, new Vector3(x,y1,0));
                    break;
                case GraphType.Cos:
                    float y2;
                    y2 = (amplitude * amplitudeScalar) * Mathf.Cos((Tau * frequency * x)+(Time.timeSinceLevelLoad * movementSpeed));
                    myLineRenderer.SetPosition(currentPoint, new Vector3(x,y2,0));
                    break;
                /*case GraphType.Tan:
                    float y3;
                    y3 = (amplitude * amplitudeScalar) * Mathf.Tan((Tau * frequency * x)+(Time.timeSinceLevelLoad * movementSpeed));
                    myLineRenderer.SetPosition(currentPoint, new Vector3(x,y3,0));
                    break;*/
            }
            
        }
    }
 
    void Update()
    {
        Draw();
    }
}