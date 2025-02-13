using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private UpgradeTray _upgradeTray;
    [SerializeField] private GameObject _windowCanvas;
    [SerializeField] private TMP_Text _upgradeText;

    private void Start()
    {
        if (_windowCanvas != null)
        {
            _windowCanvas.SetActive(false);
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void OnMouseDown()
    {
        if (_windowCanvas != null && !IsPointerOverUIObject())
        {
            bool isActive = _windowCanvas.activeSelf;
            _upgradeText.text = gameObject.name;
            _windowCanvas.SetActive(!isActive);
            _upgradeTray.UpdatePanel(gameObject.name);
        }
    }
}
