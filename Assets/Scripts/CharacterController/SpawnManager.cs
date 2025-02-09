using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _characterPrefab;
    [SerializeField] private Transform _spawnList;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private QueueManager _queueManager;
    [SerializeField] private float _spawnInterval = 4f;

    void Start()
    {
        StartCoroutine(SpawnCharacters());
    }

    IEnumerator SpawnCharacters()
    {
        while (true)
        {
            GameObject newCharacter = Instantiate(_characterPrefab[Random.Range(0, _characterPrefab.Count)], _spawnPoint.position, Quaternion.identity);
            newCharacter.transform.SetParent(_spawnList, true);
            CharacterAI characterAI = newCharacter.GetComponent<CharacterAI>();
            _queueManager.AddToQueue(characterAI);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}