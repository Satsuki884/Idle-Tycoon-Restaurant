using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CreationManager : MonoBehaviour
{

    public static CreationManager _instance;
    public static CreationManager Instance => _instance;
    [SerializeField] private GameObject[] _addTray;
    [SerializeField] private Tray[] _trays;

    private TrayDataSO trayData;

    public async Task Initialize(params object[] param)
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;


        await Task.Delay(100);
    }

    public void Start()
    {
        foreach (var tray in _trays)
        {
            if (tray.IsActive)
            {
                tray.gameObject.SetActive(true);
                foreach (var tr in _addTray)
                {
                    if (tr.name == tray.TrayName)
                    {
                        tr.SetActive(false);
                    }
                }
                _addTray.Where(x => x.name == tray.TrayName).First().SetActive(false);
            }
            else
            {
                tray.gameObject.SetActive(false);
                foreach (var tr in _addTray)
                {
                    if (tr.name == tray.TrayName)
                    {
                        tr.SetActive(true);
                    }
                }
            }
        }
    }

    public int GetTrayCost(string trayName)
    {
        foreach (var tray in _trays)
        {
            if (tray.TrayName == trayName)
            {
                return tray.Cost;
            }
        }
        return 0;
    }
}
