using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    [SerializeField] LineRenderer circleRenderer;
    [SerializeField] int _steps;
    internal Events.RangeChanged rangeLevelChangedHandlers;
    void RedrawCircle(float radius)
    {
        circleRenderer.positionCount = _steps+1;
        for(int currentStep = 0; currentStep < _steps+1; currentStep++)
        { 
            float currentRadian = (float)currentStep / _steps * 2 * Mathf.PI;
            float xScaled = Mathf.Cos(currentRadian)*radius;
            float Yscaled = Mathf.Sin(currentRadian)*radius;
            circleRenderer.SetPosition(currentStep, new Vector3(xScaled, Yscaled, 0));
        }
    }
    internal void Initialize()
    {
        rangeLevelChangedHandlers += RedrawCircle;
    }
}
