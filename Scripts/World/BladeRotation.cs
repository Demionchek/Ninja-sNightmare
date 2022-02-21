using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRotation : MonoBehaviour
{
    private float rotationSpeed;

    private void Start()
    {
        rotationSpeed = 5f;
    }

    void Update()
    {
        transform.Rotate(0, 0, -rotationSpeed, Space.Self);
    }
}
