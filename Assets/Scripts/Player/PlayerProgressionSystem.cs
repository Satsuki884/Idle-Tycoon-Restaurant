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

    public void AddExperience(int exp)
    {
        _playerData.PlayerExperience = _playerData.PlayerExperience + exp;
        SaveManager.Instance.SavePlayerData(_playerData);
        CheckNewLevel();
    }

    public void CheckNewLevel()
    {
        _playerData = SaveManager.Instance.PlayerData;
        if (_isMaxLevel)
        {
            return;
        }

        while (_playerData.PlayerExperience >= GetExpForNextLevel(_playerData.PlayerLevel + 1) && !_isMaxLevel)
        {
            Debug.Log(_playerData.PlayerExperience + " " + GetExpForNextLevel(_playerData.PlayerLevel + 1));
            _playerData.PlayerExperience -= GetExpForNextLevel(_playerData.PlayerLevel + 1);
            Debug.Log(_playerData.PlayerExperience + "now");
            if (_playerData.PlayerLevel == _playerData.MaxLevel)
            {
                _playerData.PlayerExperience = GetExpForNextLevel(_playerData.MaxLevel);
                _expBar.value = _playerData.PlayerExperience;
                _isMaxLevel = true;
            }
            else
            {
                _playerData.PlayerLevel++;
            }
            SaveManager.Instance.SavePlayerData(_playerData);
            _LevelUpPanel.gameObject.SetActive(true);
            _levelText.text = _playerData.PlayerLevel.ToString();
            _currentLevel.text = _playerData.PlayerLevel.ToString();
            _expBar.maxValue = GetExpForNextLevel(_playerData.PlayerLevel + 1);
        }

        _expBar.value = _playerData.PlayerExperience;
    }

    private int GetExpForNextLevel(int level)
    {
        var levelData = _neededExpForNextLevelSO.NeededExpForNextLevel.Find(x => x.Level == level);
        return levelData.Exp;
    }
}