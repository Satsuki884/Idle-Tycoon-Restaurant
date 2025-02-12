using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    public void MoveTo(Vector3 target, Action onComplete = null)
    {
        StartCoroutine(MoveRoutine(target, onComplete));
    }

    private IEnumerator MoveRoutine(Vector3 target, Action onComplete)
    {
        yield return new WaitForSeconds(2f);
        Vector3 targetPosition = new Vector3(target.x, transform.position.y, target.z);

        while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                new Vector3(targetPosition.x, 0, targetPosition.z)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2);
            yield return null;
        }
        onComplete?.Invoke();
    }

}