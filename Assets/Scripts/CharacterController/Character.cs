using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    public void MoveTo(Vector3 target, Vector3 offset, Action onComplete = null)
    {
        StartCoroutine(MoveRoutine(target, offset, onComplete));
    }

    private IEnumerator MoveRoutine(Vector3 target, Vector3 offset, Action onComplete)
    {
        yield return new WaitForSeconds(2f);
        Vector3 targetPosition = GetTargetPosition(target,offset);

        // Vector3 direction = (targetPosition - transform.position).normalized;
        // if (direction != Vector3.zero)
        // {
        //     Quaternion targetRotation = Quaternion.LookRotation(direction);
        //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);
        // }
        transform.LookAt(target);

        while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                new Vector3(targetPosition.x, 0, targetPosition.z)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2);
            yield return null;
        }
        onComplete?.Invoke();
    }
    
    private Vector3 GetTargetPosition(Vector3 target, Vector3 offset)
    {

        //TODO : Play offset to target position
        Vector3 targetPosition = new Vector3(target.x + offset.x , transform.position.y, target.z + offset.z);

        return targetPosition;
    }
    

}