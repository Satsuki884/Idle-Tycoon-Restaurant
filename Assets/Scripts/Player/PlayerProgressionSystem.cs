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
    [SerializeField] private TMP_Text _moneyText;
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
        SetUI();
        UpdatedPlayerMoney();
        
    }

    public void BuySmth(int money){
        _playerData.PlayerCoins -= money;
        SaveManager.Instance.SavePlayerData(_playerData);
        UpdatedPlayerMoney();
    }

    public int GetPlayerMoney(){
        return _playerData.PlayerCoins;
    }

    public int GetPlayerLevel(){
        return _playerData.PlayerLevel;
    }

    public void BuyProduct(int exp, int money, ProductType productType)
    {
        // _playerData = SaveManager.Instance.PlayerData;
        _playerData.PlayerExperience = _playerData.PlayerExperience + exp;
        CheckNewLevel();
        GetMoneyAndProduct(money, productType);
        // SaveManager.Instance.SavePlayerData(_playerData);

    }

    public void CheckNewLevel()
    {

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
            // SaveManager.Instance.SavePlayerData(_playerData);
            CreationManager.Instance.UpdateView();
        }

        _expBar.value = _playerData.PlayerExperience;
    }

    private void UpdateUI()
    {
        _LevelUpPanel.gameObject.SetActive(true);
        SetUI();
    }

    private void SetUI()
    {
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
        _expBar.value = _playerData.PlayerExperience;
    }

    private int GetExpForNextLevel(int level)
    {
        return _neededExpForNextLevelSO.NeededExpForNextLevel.Find(x => x.Level == level).Exp;
    }

    public void UpdatedPlayerMoney()
    {
        int coins = _playerData.PlayerCoins;
        _moneyText.text = FormatNumber(coins);
    }
    public string FormatNumber(int num)
    {
        if (num >= 1_000_000)
            return (num / 1_000_000f).ToString("0.#") + "m";
        if (num >= 1_000)
            return (num / 1_000f).ToString("0.#") + "k";

        return num.ToString();
    }

    private void GetMoneyAndProduct(int money, ProductType productType)
    {
        _playerData.PlayerCoins += money;
        switch (productType)
        {
            case ProductType.BlueBottle:
                _playerData.BlueBottle++;
                break;
            case ProductType.GreenBottle:
                _playerData.GreenBottle++;
                break;
            case ProductType.RedBottle:
                _playerData.RedBottle++;
                break;
            case ProductType.BrownBottle:
                _playerData.BrounBottle++;
                break;
            case ProductType.Chicken:
                _playerData.Chicken++;
                break;
            case ProductType.Mushrooms:
                _playerData.Mushrooms++;
                break;
        }
        SaveManager.Instance.SavePlayerData(_playerData);
        UpdatedPlayerMoney();
        InventorySystem.Instance.RefreshInventory();
    }
}