using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueController : MonoBehaviour
{
    public bool IsUpdateQueueInProgress = false;
    public bool IsQueueEmpty => _queuePoints[0].isFree;
    public bool IsQueueSlotAvailableToAdd => _queuePoints[_queuePoints.Length - 1].isFree;
    
    [SerializeField] private QueuePoint[] _queuePoints;

    private Coroutine _updateQueueCoroutine;
    
    public void InitializeQueuePoints(Character[] characters)
    {
        for (int i = 0; i < _queuePoints.Length; i++)
        {
            _queuePoints[i].character = characters[i];
            // If there's no character, the point is free
            _queuePoints[i].isFree = characters[i] == null; 
        }
    }

    /// <summary>
    /// Get the first character in the queue, frees the position, and updates the queue.
    /// </summary>
    public Character DequeueCharacter()
    {
        // If the queue is empty, return null
        if (_queuePoints.Length == 0 || _queuePoints[0].character == null)
            return null; 

        Character firstCharacter = _queuePoints[0].character;
        _queuePoints[0].character = null;
        _queuePoints[0].isFree = true;

        if (_updateQueueCoroutine != null)
        {
            // Stop the queue update coroutine if it's running
            StopCoroutine(_updateQueueCoroutine); 
        }
            
        // Start queue update
        _updateQueueCoroutine = StartCoroutine(UpdateQueue()); 

        return firstCharacter;
    }

    /// <summary>
    /// Moves all characters forward when a position becomes available.
    /// </summary>
    private IEnumerator UpdateQueue()
    {
        IsUpdateQueueInProgress = true;
        bool slotBecameAvailable = false;

        for (int i = 0; i < _queuePoints.Length - 1; i++)
        {
            // If the current point is free and the next one is occupied
            if (_queuePoints[i].isFree && !_queuePoints[i + 1].isFree) 
            {
                Character movingCharacter = _queuePoints[i + 1].character;
                _queuePoints[i].character = movingCharacter;
                _queuePoints[i + 1].character = null;

                _queuePoints[i].isFree = false;
                _queuePoints[i + 1].isFree = true;

                // Smoothly move the character to the new position
                yield return StartCoroutine(MoveCharacterToPoint(movingCharacter, _queuePoints[i].transform.position));

                // A slot was freed up
                slotBecameAvailable = true; 
            }
        }
        
        IsUpdateQueueInProgress = false;
    }

    /// <summary>
    /// Smoothly moves a character to a new queue position.
    /// </summary>
    private IEnumerator MoveCharacterToPoint(Character character, Vector3 targetPosition)
    {
        float duration = 0.25f; // Movement duration
        float elapsedTime = 0;
        Vector3 startPosition = character.transform.position;

        while (elapsedTime < duration)
        {
            character.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure precise positioning
        character.transform.position = targetPosition; 
    }
    
    public Vector3 AddNewCharacterToQueue(Character character)
    {
        Vector3 pointPosition = Vector3.zero;
        // Start from the last index
        for (int i = 0; i < _queuePoints.Length; i++) 
        {
            if (_queuePoints[i].isFree)
            {
                _queuePoints[i].character = character;
                _queuePoints[i].isFree = false;
                
                pointPosition = _queuePoints[i].transform.position;
            
                // Stop after placing the character
                break; 
            }
        }

        return pointPosition;
    }
}
