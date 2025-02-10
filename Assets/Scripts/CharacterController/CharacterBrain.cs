using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterBrain : MonoBehaviour
{
    private Character _character;
    [SerializeField] private TrayDataSO _trayDataSo;

    public void Initialize(Character character)
    {
        _character = character;
    }

    public ProductType SelectProduct(){
        var activeTrays = GetActiveTrays();

        int randomIndex = Random.Range(0, activeTrays.Count);
        return (ProductType)randomIndex;
    }

    private List<TraySO> GetActiveTrays(){
        return _trayDataSo.TrayData.Where(tray => tray.TrayData.IsActive).ToList();
    }
}