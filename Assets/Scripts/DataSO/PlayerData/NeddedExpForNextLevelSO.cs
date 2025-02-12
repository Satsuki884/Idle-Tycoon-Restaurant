using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NeededExpForNextLevel", menuName = "ScriptableObjects/NeededExpForNextLevelSO", order = 1)]
public class NeededExpForNextLevelSO : ScriptableObject
{
    [SerializeField] private List<LevelExp> _neededExpForNextLevel;
    public List<LevelExp> NeededExpForNextLevel => _neededExpForNextLevel;
}

[System.Serializable]

public class LevelExp
{
    [SerializeField] private int _level;
    public int Level => _level;
    [SerializeField] private int _exp;
    public int Exp => _exp;
}