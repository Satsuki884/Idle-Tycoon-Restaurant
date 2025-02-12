using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    private ProductType _selectedProductType;
    private Tray _selectedTray;
    public float moveSpeed = 2f;

    public void Initialize(ProductType productType)
    {
        _selectedProductType = productType;
        _selectedTray = FindTray();
        // SetPath();
        _selectedTray.AddCharacterToQueue(this);

    }

    public void MoveTo(Vector3 target, Action onComplete = null)
    {
        StartCoroutine(MoveRoutine(target, onComplete));
    }

    private IEnumerator MoveRoutine(Vector3 target, Action onComplete)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 2);
            yield return null;
        }
        onComplete?.Invoke();
    }

    // private void SetPath()
    // {
    //     List<Vector3> newPath = new List<Vector3>
    //     {
    //         _selectedTray.StartPositions.transform.position,
    //         _selectedTray.QueuePoints.transform.position
    //     };
    //     StartCoroutine(MoveTo(newPath.ToArray()));
    // }

    // IEnumerator MoveTo(Vector3[] path)
    // {
    //     foreach (Vector3 point in path)
    //     {
    //         while (Vector3.Distance(transform.position, point) > 0.1f)
    //         {
    //             transform.position = Vector3.MoveTowards(transform.position, point, moveSpeed * Time.deltaTime);
    //             yield return null;
    //         }
    //     }
    // }

    private Tray FindTray()
    {
        Tray[] trays = FindObjectsOfType<Tray>();
        foreach (Tray tray in trays)
        {
            if (tray.ProductType == _selectedProductType)
            {
                return tray;
            }
        }
        return null;
    }
}