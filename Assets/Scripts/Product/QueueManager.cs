using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    private Queue<Character> waitingQueue = new Queue<Character>();
    public Queue<Character> WaitingQueue => waitingQueue;
    public void AddToQueue(Character character)
    {
        waitingQueue.Enqueue(character);
        // TryAssignTray();
    }

    public void RemoveFromQueue()
    {
        waitingQueue.Dequeue();
        // TryAssignTray();
    }
}
