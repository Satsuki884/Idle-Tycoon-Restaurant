using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;

    void Update()
    {
        float yRotation = transform.rotation.eulerAngles.y + Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, 0);
    }
}
