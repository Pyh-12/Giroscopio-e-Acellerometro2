using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    [SerializeField] float barHeight = 400f; // altura da sua barra

    RectTransform rectTransform;

    void Awake() { rectTransform = GetComponent<RectTransform>(); Apply(); }

    void Apply()
    {
        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width; anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width; anchorMax.y /= Screen.height;

        // ancora na base do safe area, largura permanece como está
        rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, anchorMin.y);
        rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x, anchorMin.y);
        rectTransform.pivot = new Vector2(0.5f, 0f);

        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, barHeight); // usa 400 por padrão
    }
}
