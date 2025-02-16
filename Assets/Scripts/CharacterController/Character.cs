using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveTo(Vector3 target, Action onComplete = null)
    {
        StartCoroutine(MoveRoutine(target, onComplete));
    }

    private IEnumerator MoveRoutine(Vector3 target, Action onComplete)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.3f));
        // Debug.Log(_animator);

        _animator.SetBool("walk", true);
        Vector3 targetPosition = new Vector3(target.x, transform.position.y, target.z);
        transform.LookAt(target);

        while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                new Vector3(targetPosition.x, 0, targetPosition.z)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            yield return null;

        }
        _animator.SetBool("walk", false);
        onComplete?.Invoke();
    }


}