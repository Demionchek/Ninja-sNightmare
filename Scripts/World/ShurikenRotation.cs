using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed =4f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0, Space.Self);
    }
}
