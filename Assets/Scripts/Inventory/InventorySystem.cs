using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Transform _inventoryItemsContainer;
    [SerializeField] private GameObject _inventoryItemPrefab;
    [SerializeField] private Sprite _blueBottleImage;
    [SerializeField] private Sprite _redBottleImage;
    [SerializeField] private Sprite _greenBottleImage;
    [SerializeField] private Sprite _brownBottleImage;
    [SerializeField] private Sprite _chikenImage;
    [SerializeField] private Sprite _mushroomsImage;
    private PlayerData _playerData;
    private TrayDataSO _trayData;
    private float _itemWidth;

    public static InventorySystem _instance;
    public static InventorySystem Instance => _instance;
    public async Task Initialize(params object[] param)
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;


        await Task.Delay(100);
    }

    void Start()
    {
        _playerData = SaveManager.Instance.PlayerData;
        _trayData = SaveManager.Instance.TrayData;
        CreateInventory();
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        _playerData = SaveManager.Instance.PlayerData;
        CreateInventory();
    }

    float xOffset;

    private void CreateInventory()
    {
        if (_inventoryItemsContainer.childCount > 0)
        {
            Destroy(_inventoryItemsContainer.GetChild(0).gameObject);
        }
        _inventoryItemsContainer.DetachChildren();
        _itemWidth = 0;

        xOffset = 0f;
        _itemWidth = _inventoryItemPrefab.GetComponent<RectTransform>().sizeDelta.x;


        if (_playerData.BlueBottle != 0)
        {
            SetInventoryItem(_playerData.BlueBottle, _blueBottleImage);
            // InventoryItem itemUI = Instantiate(_inventoryItemPrefab, _inventoryItemsContainer).GetComponent<InventoryItem>();
            // itemUI.SetItemData(_playerData.BlueBottle, _blueBottleImage);
            // RectTransform itemRect = itemUI.GetComponent<RectTransform>();
            // itemRect.anchoredPosition = new Vector2(xOffset, 0);
            // xOffset += _itemWidth;
        }
        else if (_playerData.RedBottle != 0)
        {
            SetInventoryItem(_playerData.RedBottle, _redBottleImage);
            // InventoryItem itemUI = Instantiate(_inventoryItemPrefab, _inventoryItemsContainer).GetComponent<InventoryItem>();
            // itemUI.SetItemData(_playerData.RedBottle, _redBottleImage);
            // RectTransform itemRect = itemUI.GetComponent<RectTransform>();
            // itemRect.anchoredPosition = new Vector2(xOffset, 0);
            // xOffset += _itemWidth;
        }
        else if (_playerData.GreenBottle != 0)
        {
            SetInventoryItem(_playerData.GreenBottle, _greenBottleImage);
            // InventoryItem itemUI = Instantiate(_inventoryItemPrefab, _inventoryItemsContainer).GetComponent<InventoryItem>();
            // itemUI.SetItemData(_playerData.GreenBottle, _greenBottleImage);
            // RectTransform itemRect = itemUI.GetComponent<RectTransform>();
            // itemRect.anchoredPosition = new Vector2(xOffset, 0);
            // xOffset += _itemWidth;
        }
        else if (_playerData.BrounBottle != 0)
        {
            SetInventoryItem(_playerData.BrounBottle, _brownBottleImage);
            // InventoryItem itemUI = Instantiate(_inventoryItemPrefab, _inventoryItemsContainer).GetComponent<InventoryItem>();
            // itemUI.SetItemData(_playerData.BrounBottle, _brownBottleImage);
            // RectTransform itemRect = itemUI.GetComponent<RectTransform>();
            // itemRect.anchoredPosition = new Vector2(xOffset, 0);
            // xOffset += _itemWidth;
        }
        else if (_playerData.Chicken != 0)
        {
            SetInventoryItem(_playerData.Chicken, _chikenImage);
            // InventoryItem itemUI = Instantiate(_inventoryItemPrefab, _inventoryItemsContainer).GetComponent<InventoryItem>();
            // itemUI.SetItemData(_playerData.Food, _chikenImage);
            // RectTransform itemRect = itemUI.GetComponent<RectTransform>();
            // itemRect.anchoredPosition = new Vector2(xOffset, 0);
            // xOffset += _itemWidth;
        }
        else if (_playerData.Mushrooms != 0)
        {
            SetInventoryItem(_playerData.Mushrooms, _mushroomsImage);
            // InventoryItem itemUI = Instantiate(_inventoryItemPrefab, _inventoryItemsContainer).GetComponent<InventoryItem>();
            // itemUI.SetItemData(_playerData.Mushrooms, _mushroomsImage);
            // RectTransform itemRect = itemUI.GetComponent<RectTransform>();
            // itemRect.anchoredPosition = new Vector2(xOffset, 0);
            // xOffset += _itemWidth;
        }




        _inventoryItemsContainer.GetComponent<RectTransform>().sizeDelta =
            new Vector2(xOffset, _inventoryItemsContainer.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void SetInventoryItem(int count, Sprite image)
    {
        InventoryItem itemUI = Instantiate(_inventoryItemPrefab, _inventoryItemsContainer).GetComponent<InventoryItem>();
        itemUI.SetItemData(count, image);
        RectTransform itemRect = itemUI.GetComponent<RectTransform>();
        itemRect.anchoredPosition = new Vector2(xOffset, 0);
        xOffset += _itemWidth;
    }
}