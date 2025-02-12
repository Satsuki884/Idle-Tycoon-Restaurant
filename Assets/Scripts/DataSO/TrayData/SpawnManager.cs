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
    // [SerializeField] private float _spawnInterval = 4f;
    // [SerializeField] public Transform _spawnZone;

    public Character SpawnCharacters(ProductType productType, Transform _spawnZone)
    {
        GameObject newCharacter = Instantiate(_characterPrefab[UnityEngine.Random.Range(0, _characterPrefab.Count)], _spawnZone.position, Quaternion.identity);
        newCharacter.transform.SetParent(_spawnList, true);
        Character character = newCharacter.GetComponent<Character>();
        character.Initialize(productType);
        return character;
    }
}