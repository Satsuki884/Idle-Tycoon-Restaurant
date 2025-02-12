using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private bool hasReachedSpot = false;

    public void MoveTo(Vector3 target, Action onComplete = null)
    {
        StartCoroutine(MoveRoutine(target, onComplete));
    }

    public void MoveToSpot(Vector3 target, Action onComplete = null)
    {
        StartCoroutine(MoveRoutineToSpot(target, onComplete));
    }

    private IEnumerator MoveRoutine(Vector3 target, Action onComplete)
    {
        yield return new WaitForSeconds(2f);
        Vector3 targetPosition = new Vector3(target.x, transform.position.y, target.z); // Игнорируем Y

        while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                new Vector3(targetPosition.x, 0, targetPosition.z)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2);
            yield return null;

            // if (hasReachedSpot) // Если коллайдер сработал
            // {
                
            //     break;
            // }
        }
        onComplete?.Invoke();
    }

    private IEnumerator MoveRoutineToSpot(Vector3 target, Action onComplete)
    {
        yield return new WaitForSeconds(2f);

        Vector3 targetPosition = new Vector3(target.x, transform.position.y, target.z); // Игнорируем Y

        while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                new Vector3(targetPosition.x, 0, targetPosition.z)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2);
            yield return null;

            if (hasReachedSpot) // Если коллайдер сработал
            {
                
                break;
            }
        }
        onComplete?.Invoke();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spot"))
        {
            hasReachedSpot = true;
        }
    }
}