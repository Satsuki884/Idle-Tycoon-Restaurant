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
    [Header("Info Panel")]
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private TMP_Text _expirienceText;
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
        UpdateUI();
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

        while (!_isMaxLevel && _playerData.PlayerExperience >= GetExpForNextLevel(_playerData.PlayerLevel + 1))
        {
            int nextLevelExp = GetExpForNextLevel(_playerData.PlayerLevel + 1);
            _playerData.PlayerExperience -= nextLevelExp;

            if (_playerData.PlayerLevel == _playerData.MaxLevel - 1)
            {
                _playerData.PlayerExperience = nextLevelExp;
                _isMaxLevel = true;
            }
            else
            {
                _playerData.PlayerLevel++;
            }

            UpdateUI();
            SaveManager.Instance.SavePlayerData(_playerData);
        }

        _expBar.value = _playerData.PlayerExperience;
    }

    private void UpdateUI()
    {
        _LevelUpPanel.gameObject.SetActive(true);
        _levelText.text = _playerData.PlayerLevel.ToString();
        _currentLevel.text = _playerData.PlayerLevel.ToString();
        _expBar.maxValue = GetExpForNextLevel(_playerData.PlayerLevel + 1);
        _currentLevelText.text = _playerData.PlayerLevel.ToString();
        if (_isMaxLevel)
        {
            _expirienceText.text = "Max Level. Congratulations!";
            return;
        }
        else
        {
            _expirienceText.text = _playerData.PlayerExperience.ToString() + " / " + GetExpForNextLevel(_playerData.PlayerLevel + 1);
        }

    }

    private int GetExpForNextLevel(int level)
    {
        return _neededExpForNextLevelSO.NeededExpForNextLevel.Find(x => x.Level == level).Exp;
    }
}