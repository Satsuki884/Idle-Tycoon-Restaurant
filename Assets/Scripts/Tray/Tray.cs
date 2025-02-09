using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    public Vector3[] availableSpots; // Массив точек у лотка
    private List<Vector3> freeSpots = new List<Vector3>();
    public int incomePerOrder = 10;
    public QueueManager queueManager;

    private void Start()
    {
        freeSpots.AddRange(availableSpots);
    }

    public bool HasFreeSpot()
    {
        return freeSpots.Count > 0;
    }

    public void AssignCharacter(CharacterAI character)
    {
        if (freeSpots.Count == 0) return;

        Vector3 targetSpot = freeSpots[0];
        freeSpots.RemoveAt(0);
        character.SetPath(new Vector3[] { targetSpot }, this, targetSpot);
    }

    public void CompleteOrder()
    {
        // Начисление денег
        // GameManager.Instance.AddMoney(incomePerOrder);
        freeSpots.Add(availableSpots[availableSpots.Length - freeSpots.Count - 1]);
        queueManager.NotifyTrayFreed();
    }
}
