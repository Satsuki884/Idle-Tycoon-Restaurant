using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{

    [SerializeField] private Button _button;
    private void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        Debug.Log("Button Clicked");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.ButtonClick);
    }
}