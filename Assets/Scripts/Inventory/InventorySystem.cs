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
        // RefreshInventory();
    }

    float xOffset;

    public void RefreshInventory()
    {
        Debug.Log("CreateInventory");

        // Удаляем все дочерние элементы
        foreach (Transform child in _inventoryItemsContainer)
        {
            Destroy(child.gameObject);
        }

        _inventoryItemsContainer.DetachChildren();
        xOffset = 0f;

        if (_inventoryItemPrefab != null)
        {
            _itemWidth = _inventoryItemPrefab.GetComponent<RectTransform>().sizeDelta.x;
        }
        else
        {
            Debug.LogError("InventoryItemPrefab is null!");
            return;
        }

        // Проходим по всем элементам инвентаря
        foreach (var tray in _trayData.TrayData)
        {
            SetInventoryItem(tray.TrayData);
        }

        // Устанавливаем новый размер контейнера
        RectTransform rectTransform = _inventoryItemsContainer.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(xOffset, rectTransform.sizeDelta.y);
    }


    public void SetInventoryItem(TrayData trayData)
    {
        InventoryItem itemUI = Instantiate(_inventoryItemPrefab, _inventoryItemsContainer).GetComponent<InventoryItem>();
        itemUI.SetItemData(trayData);
        // itemUI.SetItemData(count);
        RectTransform itemRect = itemUI.GetComponent<RectTransform>();
        itemRect.anchoredPosition = new Vector2(xOffset, 0);
        xOffset += _itemWidth;
    }
}