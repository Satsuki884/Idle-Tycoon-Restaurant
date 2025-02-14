using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Transform _inventoryItemsContainer;
    [SerializeField] private GameObject _inventoryItemPrefab;
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
        _trayData = SaveManager.Instance.TrayData;
        RefreshInventory();
    }

    float xOffset;

    public void RefreshInventory()
    {
        Debug.Log("CreateInventory");
        
        if (_inventoryItemsContainer.childCount > 0)
        {
            Destroy(_inventoryItemsContainer.GetChild(0).gameObject);
        }
        _inventoryItemsContainer.DetachChildren();
        _itemWidth = 0;

        xOffset = 0f;
        _itemWidth = _inventoryItemPrefab.GetComponent<RectTransform>().sizeDelta.x;

        foreach(var tray in _trayData.TrayData)
        {
            switch (tray.TrayData.ProductType)
            {
                case ProductType.BlueBottle:
                    SetInventoryItem(tray.TrayData.ItemCount, tray.TrayData.ProductImage);
                    break;
                case ProductType.RedBottle:
                    SetInventoryItem(tray.TrayData.ItemCount, tray.TrayData.ProductImage);
                    break;
                case ProductType.GreenBottle:
                    SetInventoryItem(tray.TrayData.ItemCount, tray.TrayData.ProductImage);
                    break;
                case ProductType.BrownBottle:
                    SetInventoryItem(tray.TrayData.ItemCount, tray.TrayData.ProductImage);
                    break;
                case ProductType.Mushrooms:
                    SetInventoryItem(tray.TrayData.ItemCount, tray.TrayData.ProductImage);
                    break;
                case ProductType.Chicken:
                    SetInventoryItem(tray.TrayData.ItemCount, tray.TrayData.ProductImage);
                    break;
            }
        }

        _inventoryItemsContainer.GetComponent<RectTransform>().sizeDelta =
            new Vector2(xOffset, _inventoryItemsContainer.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void SetInventoryItem(int count, Sprite image)
    {
        InventoryItem itemUI = Instantiate(_inventoryItemPrefab, _inventoryItemsContainer).GetComponent<InventoryItem>();
        // itemUI.SetItemData(count, image);
        itemUI.SetItemData(count);
        RectTransform itemRect = itemUI.GetComponent<RectTransform>();
        itemRect.anchoredPosition = new Vector2(xOffset, 0);
        xOffset += _itemWidth;
    }
}