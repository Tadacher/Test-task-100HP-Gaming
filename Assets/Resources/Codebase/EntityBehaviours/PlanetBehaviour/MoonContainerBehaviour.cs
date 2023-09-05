using UnityEngine;

public class MoonContainerBehaviour : MonoBehaviour
{
    [SerializeField] float orbitalSpeed;
    [SerializeField] Transform moonSpriteTransform;

    private void Update()
    {
        transform.localEulerAngles += new Vector3(0, 0, Time.deltaTime * orbitalSpeed);
        moonSpriteTransform.eulerAngles = Vector3.zero;
    }
}
