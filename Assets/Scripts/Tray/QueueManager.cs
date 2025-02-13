using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    private Queue<Character> _waitingQueue = new Queue<Character>();
    public Queue<Character> WaitingQueue => _waitingQueue;

    // public static QueueManager _instance;
    // public static QueueManager Instance => _instance;

    // public async Task Initialize(params object[] param)
    // {
    //     if (_instance != null)
    //     {
    //         return;
    //     }

    //     _instance = this;


    //     await Task.Delay(100);
    // }
    public void AddToQueue(Character character)
    {
        _waitingQueue.Enqueue(character);
    }

    public void RemoveFromQueue()
    {
        _waitingQueue.Dequeue();
    }
}
