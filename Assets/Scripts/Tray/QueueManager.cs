using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    private Queue<Character> _waitingQueue = new Queue<Character>();
    public Queue<Character> WaitingQueue => _waitingQueue;
    private List<Character> _waitingQueueList = new List<Character>();
    public List<Character> WaitingQueueList => _waitingQueueList;
    public void AddToQueue(Character character)
    {
        _waitingQueue.Enqueue(character);
        _waitingQueueList.Add(character);
    }

    public void RemoveFromQueueList(Character character)
    {
        _waitingQueueList.Remove(character);
    }


}
