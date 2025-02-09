using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    private Queue<CharacterAI> waitingQueue = new Queue<CharacterAI>();
    public Tray[] trays; // Список всех лотков

    public void AddToQueue(CharacterAI character)
    {
        waitingQueue.Enqueue(character);
        TryAssignTray();
    }

    private void TryAssignTray()
    {
        if (waitingQueue.Count == 0) return;

        foreach (var tray in trays)
        {
            if (tray.HasFreeSpot())
            {
                CharacterAI character = waitingQueue.Dequeue();
                tray.AssignCharacter(character);
                break;
            }
        }
    }

    public void NotifyTrayFreed()
    {
        TryAssignTray();
    }
}
