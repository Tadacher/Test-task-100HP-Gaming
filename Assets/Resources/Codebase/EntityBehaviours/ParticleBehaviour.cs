using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 3);
    }
}
