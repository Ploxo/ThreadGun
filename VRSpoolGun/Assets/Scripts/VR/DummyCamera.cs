using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCamera : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject);
    }
}
