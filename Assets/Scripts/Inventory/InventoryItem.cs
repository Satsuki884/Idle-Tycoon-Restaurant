using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemCount;
    [SerializeField] private Image _productImage;

    public void SetItemData(int count, Sprite productImage)
    {
        _itemCount.text = PlayerProgressionSystem.Instance.FormatNumber(count);
        _productImage.sprite = productImage;
    }
}