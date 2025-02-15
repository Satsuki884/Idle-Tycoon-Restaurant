using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemCount;
    [SerializeField] private Image _productImage;
    [SerializeField] private TMP_Text _itemName;

    public void SetItemData(TrayData trayData)
    {
        _itemCount.text = PlayerProgressionSystem.Instance.FormatNumber(trayData.ItemCount);
        _productImage.sprite = trayData.ProductImage;
        _itemName.text = trayData.TrayName;
    }
}