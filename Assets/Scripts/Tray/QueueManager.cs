using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    private Queue<Character> _waitingQueue = new Queue<Character>();
    public Queue<Character> WaitingQueue => _waitingQueue;
    public void AddToQueue(Character character)
    {
        _waitingQueue.Enqueue(character);
    }

    public void RemoveFromQueue()
    {
        _waitingQueue.Dequeue();
    }
}
