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
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.3f));
        Vector3 targetPosition = new Vector3(target.x, transform.position.y, target.z);
        transform.LookAt(target);

        while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                new Vector3(targetPosition.x, 0, targetPosition.z)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }
        onComplete?.Invoke();
    }

    // private Vector3 GetTargetPosition(Vector3 target)
    // {
    //     Vector3 direction = (target - transform.position).normalized; // Нормализованный вектор направления
    //     float offsetDistance = transform.localScale.x; // Используем ширину объекта как смещение
    //     Vector3 targetPosition = target - direction * offsetDistance; // Смещаем цель назад

    //     return new Vector3(targetPosition.x, transform.position.y, targetPosition.z); // Оставляем текущую высоту
    // }


}