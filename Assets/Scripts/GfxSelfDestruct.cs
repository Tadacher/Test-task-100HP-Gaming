using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GfxSelfDestruct : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 3);
    }
}
