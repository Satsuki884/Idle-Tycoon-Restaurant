using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _characterDayPrefab;
    [SerializeField] private List<GameObject> _characterNightPrefab;
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
        List<GameObject> selectedPrefabs = IsNightTime() ? _characterNightPrefab : _characterDayPrefab;
        GameObject newCharacter = Instantiate(selectedPrefabs[UnityEngine.Random.Range(0, selectedPrefabs.Count)], _spawnZone.position, Quaternion.identity);
        newCharacter.transform.SetParent(_spawnList, true);
        Character character = newCharacter.GetComponent<Character>();
        return character;
    }

    private bool IsNightTime()
    {
        int hour = DateTime.Now.Hour;
        return hour >= 18 || hour < 6; // Ночь с 18:00 до 06:00
    }
}
