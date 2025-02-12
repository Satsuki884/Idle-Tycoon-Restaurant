using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgressionSystem : MonoBehaviour
{
    [SerializeField] private NeededExpForNextLevelSO _neededExpForNextLevelSO;
    private PlayerData _playerData;
    [SerializeField] private Transform _LevelUpPanel;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private Slider _expBar;
    private bool _isMaxLevel = false;

    public static PlayerProgressionSystem Instance { get; private set; }

    public async Task Initialize(params object[] param)
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        await Task.Delay(100);
    }

    void Start()
    {
        _playerData = SaveManager.Instance.PlayerData;
        _expBar.minValue = 0;
        _expBar.maxValue = GetExpForNextLevel(_playerData.PlayerLevel + 1);
        _expBar.value = _playerData.PlayerExperience;
        _currentLevel.text = _playerData.PlayerLevel.ToString();
    }

    public void CheckNewLevel()
    {
        if (_isMaxLevel)
        {
            return;
        }

        if (_playerData.PlayerLevel >= _playerData.MaxLevel)
        {
            _playerData.PlayerExperience = GetExpForNextLevel(_playerData.PlayerLevel);
            _isMaxLevel = true;
            return;
        }

        while (_playerData.PlayerExperience >= GetExpForNextLevel(_playerData.PlayerLevel + 1)
            && _playerData.PlayerLevel < _playerData.MaxLevel)
        {
            _playerData.PlayerExperience -= GetExpForNextLevel(_playerData.PlayerLevel+1);
            _playerData.PlayerLevel++;
            _LevelUpPanel.gameObject.SetActive(true);
            _levelText.text = _playerData.PlayerLevel.ToString();
            _currentLevel.text = _playerData.PlayerLevel.ToString();
            SaveManager.Instance.SavePlayerData(_playerData);
            _expBar.maxValue = GetExpForNextLevel(_playerData.PlayerLevel);
        }

        _expBar.value = _playerData.PlayerExperience;
    }

    private int GetExpForNextLevel(int level)
    {
        var levelData = _neededExpForNextLevelSO.NeededExpForNextLevel.Find(x => x.Level == level);
        return levelData != null ? levelData.Exp : int.MaxValue;
    }
}