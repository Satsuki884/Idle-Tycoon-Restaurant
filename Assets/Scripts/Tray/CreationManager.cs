using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CreationManager : MonoBehaviour
{

    public static CreationManager _instance;
    public static CreationManager Instance => _instance;
    [SerializeField] private TrayDataSO _trayDataSo;
    public async Task Initialize(params object[] param)
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;


        await Task.Delay(100);
    }

    public void Awake(){
        
    }
}
