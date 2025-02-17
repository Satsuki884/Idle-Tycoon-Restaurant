using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _sensitivity = 0.1f; // Новый параметр для чувствительности

    private Vector2 _touchStart;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = (touch.position.x - _touchStart.x) * _sensitivity; // Уменьшаем влияние свайпа
                float yRotation = transform.rotation.eulerAngles.y + deltaX * _rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, 0);

                _touchStart = touch.position;
            }
        }
        float rotation = transform.rotation.eulerAngles.y + Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotation, 0);
    }

}


/*using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _sensitivity = 0.1f; // Новый параметр для чувствительности

    private Vector2 _touchStart;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = (touch.position.x - _touchStart.x) * _sensitivity; // Уменьшаем влияние свайпа
                float yRotation = transform.rotation.eulerAngles.y + deltaX * _rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, 0);

                _touchStart = touch.position;
            }
        }
    }

}
*/