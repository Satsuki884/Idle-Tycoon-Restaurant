using System.Collections;
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
            _queuePoints[i].isFree = characters[i] == null;
        }
    }

    public Character DequeueCharacter()
    {
        if (_queuePoints.Length == 0 || _queuePoints[0].character == null)
            return null;

        Character firstCharacter = _queuePoints[0].character;
        _queuePoints[0].character = null;
        _queuePoints[0].isFree = true;

        if (_updateQueueCoroutine != null)
        {
            StopCoroutine(_updateQueueCoroutine);
        }

        _updateQueueCoroutine = StartCoroutine(UpdateQueue());

        return firstCharacter;
    }

    private IEnumerator UpdateQueue()
    {
        IsUpdateQueueInProgress = true;
        bool slotBecameAvailable = false;

        for (int i = 0; i < _queuePoints.Length - 1; i++)
        {
            if (_queuePoints[i].isFree && !_queuePoints[i + 1].isFree)
            {
                Character movingCharacter = _queuePoints[i + 1].character;
                _queuePoints[i].character = movingCharacter;
                _queuePoints[i + 1].character = null;

                _queuePoints[i].isFree = false;
                _queuePoints[i + 1].isFree = true;

                yield return StartCoroutine(MoveCharacterToPoint(movingCharacter, _queuePoints[i].transform.position));

                slotBecameAvailable = true;
            }
        }

        IsUpdateQueueInProgress = false;
    }

    private IEnumerator MoveCharacterToPoint(Character character, Vector3 targetPosition)
    {
        float duration = 0.25f;
        float elapsedTime = 0;
        Vector3 startPosition = character.transform.position;
        Animator animator = character.GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("walk", true);

        while (elapsedTime < duration)
        {
            character.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure precise positioning
        character.transform.position = targetPosition;
        if(animator != null)
            animator.SetBool("walk", false);
    }

    public Vector3 AddNewCharacterToQueue(Character character)
    {
        Vector3 pointPosition = Vector3.zero;
        for (int i = 0; i < _queuePoints.Length; i++)
        {
            if (_queuePoints[i].isFree)
            {
                _queuePoints[i].character = character;
                _queuePoints[i].isFree = false;

                pointPosition = _queuePoints[i].transform.position;

                break;
            }
        }

        return pointPosition;
    }
}
