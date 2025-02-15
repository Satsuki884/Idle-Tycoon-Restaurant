using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform rectTransform;
    Rect safeArea;
    Vector2 anchorMin;
    Vector2 anchorMax;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        anchorMin = safeArea.position;
        anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        safeArea = Screen.safeArea;

        anchorMin = safeArea.position;
        anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }

    void OnRectTransformDimensionsChange()
    {
        ApplySafeArea();
    }
}