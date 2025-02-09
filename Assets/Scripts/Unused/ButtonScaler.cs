using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Vector3 originalScale;
    public float scaleFactor = 1.1f; // Во сколько раз увеличивать кнопку

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale; // Запоминаем исходный размер
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale * scaleFactor; // Увеличиваем кнопку
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale; // Возвращаем к исходному размеру
    }
}
