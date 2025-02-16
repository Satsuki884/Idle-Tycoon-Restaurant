using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemCount;
    [SerializeField] private Image _productImage;
    // [SerializeField] private TMP_Text _itemName;

    public void SetItemData(TrayData trayData)
    {
        _itemCount.text = FormatNumber(trayData.ItemCount);
        _productImage.sprite = trayData.ProductImage;
        // _itemName.text = trayData.TrayName;
    }

    private string FormatNumber(int num)
    {
        if (num >= 1_000_000)
            return (num / 1_000_000f).ToString("0.#") + "m";
        if (num >= 1_000)
            return (num / 1_000f).ToString("0.#") + "k";

        return num.ToString();
    }
}