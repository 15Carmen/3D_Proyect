using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 85f;
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f);

    }
}
