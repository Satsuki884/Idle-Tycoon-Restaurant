using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QueuePoint
{
    public int index;
    public bool isFree = true;
    public Character character;
    public Transform transform;
}
