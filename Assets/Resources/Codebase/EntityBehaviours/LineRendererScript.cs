using Events;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    public float Radius;

    [SerializeField] private LineRenderer circleRenderer;
    [SerializeField] private int _steps;
    private Events.OnRangeChanged rangeLevelChangedHandlers;
    private bool _inited;

    public void RedrawCircle(float radius)
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
}
