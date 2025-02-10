using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _characterPrefab;
    [SerializeField] private Transform _spawnList;
    [SerializeField] private List<Tray> _trays; // Список всех лотков
    [SerializeField] private float _spawnInterval = 4f;
    [SerializeField] private TrayDataSO _trayDataSo;

    public event Action QueueAvailable;

    void Start()
    {
        StartCoroutine(SpawnCharacters());
    }

    IEnumerator SpawnCharacters()
    {
        while (true)
        {
            while (HasAvailableQueue())
            {

                GameObject newCharacter = Instantiate(_characterPrefab[UnityEngine.Random.Range(0, _characterPrefab.Count)]);
                newCharacter.transform.SetParent(_spawnList, true);
                Character character = newCharacter.GetComponent<Character>();
                // character.AddCharacterToQueue();
                character.Initialize();
                // yield return StartCoroutine(character.InitializeAsync());

                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
    

    private bool HasAvailableQueue()
    {
        foreach (Tray tray in _trays)
        {
            if (!tray.IsQueueFool)
            {
                return true;
            }
        }
        return false;
    }
}