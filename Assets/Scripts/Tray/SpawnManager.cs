using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _characterPrefab;
    [SerializeField] private Transform _spawnList;
    public static SpawnManager _instance;
    public static SpawnManager Instance => _instance;

    public async Task Initialize(params object[] param)
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;


        await Task.Delay(100);
    }

    public Character SpawnCharacters(Transform _spawnZone)
    {
        GameObject newCharacter = Instantiate(_characterPrefab[UnityEngine.Random.Range(0, _characterPrefab.Count)], _spawnZone.position, Quaternion.identity);
        newCharacter.transform.SetParent(_spawnList, true);
        Character character = newCharacter.GetComponent<Character>();
        return character;
    }
}