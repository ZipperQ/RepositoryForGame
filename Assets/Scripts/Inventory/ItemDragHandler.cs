using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// �����, ����������� � ����������� ���������� ��� ������ ��������������, �������� �������������� � ����� �������������� ��������
// �� ����� � ������ ����
public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;    // �� ������ ���� ������� ��������� CanvasGroup, ��� ���������� ��� �������� �������� � ����
    private Transform originalParent;   // �������� �����
    private Image itemImage;
    private readonly Sprite itemSprite;
    private bool shouldReturnToOriginalSlot = true; // ����, �����������, ������� �� ������� � �������� ����� ���
                                                    // ���������� �� ������ ������� �����
    public Sprite emptySlot;    // ������ ������� �����
    private Sprite originalSprite;
    private bool canBeDropped = false;    // ����, �����������, ��� �� ������� ������� ��� ������-���� �����

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        itemImage = GetComponent<Image>();
    }

    // �����, ����������� ������ �������������� �������� �� �����
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemImage.sprite.name == "����_0")  // ���� ���� ����� ������ ������� �����, �� ������������� ������� �� ����� ����������� (�. �. �� ����)
        {
            eventData.pointerDrag = null;
            return;
        }

        originalParent = transform.parent;
        transform.SetParent(transform.root); // ����������� � ������ UI
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;   // ���������� ��������������� ������
        canvasGroup.blocksRaycasts = false;
    }

    // �����, ����������� ������� �������������� �������� �� �����
    public void OnDrag(PointerEventData eventData)
    {
        if (itemImage.sprite == itemSprite) return;

        // ��������� ������������ ��������������� �� ������ ������ ������������ 2560
        float scaleFactor = 2560f / Screen.width;

        // ������������� �������� � ������ ���������� ������ � ���������� �������� �� ��������
        rectTransform.anchoredPosition += eventData.delta * scaleFactor;
    }

    // �����, ����������� ��������� �������������� �������� �� �����
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (canBeDropped == false)  // ���� ������� ������ �������� � ��������� �����...
        {
            transform.SetParent(originalParent);    // ��������� ������������� ��������
            rectTransform.localPosition = Vector2.zero;
            return;
        }

        if (shouldReturnToOriginalSlot)
        {
            transform.SetParent(originalParent);
            transform.gameObject.GetComponent<Image>().sprite = originalSprite;
            rectTransform.localPosition = Vector2.zero;
        }
        else
        {
            transform.gameObject.GetComponent<Image>().sprite = emptySlot;
            transform.SetParent(originalParent);
            rectTransform.localPosition = Vector2.zero;
            shouldReturnToOriginalSlot = !shouldReturnToOriginalSlot;
        }

        canBeDropped = false;
    }

    // ����� ��� ��������� ������� ������
    public Sprite GetItemSprite()
    {
        return itemSprite;
    }

    // ����� ��� ��������� ������� �� �����, � ������� ��� �������� �������
    public void SetSprite(Sprite sprite)
    {
        originalSprite = sprite;
    }

    // �����, �����������, ����� �������� ������ ���� � ������������ ��������
    public void SetParentStatus(bool flag)
    {
        shouldReturnToOriginalSlot = flag;
    }

    // �����, ��������������� ����, ������������, ��� �� ������� ������� ��� ������-���� �����
    public void SetCanBeDropped(bool flag)
    {
        canBeDropped = flag;
    }
}
