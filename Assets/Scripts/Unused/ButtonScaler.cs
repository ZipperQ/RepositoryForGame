using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Vector3 originalScale;
    public float scaleFactor = 1.1f; // �� ������� ��� ����������� ������

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale; // ���������� �������� ������
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale * scaleFactor; // ����������� ������
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale; // ���������� � ��������� �������
    }
}
