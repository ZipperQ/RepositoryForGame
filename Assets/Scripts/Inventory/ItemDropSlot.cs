using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// �����, ����������� � ����������� ��������� ��� ���������� �������� � ����
public class ItemDropSlot : MonoBehaviour, IDropHandler
{
    private Image slotImage; // ������ �� ����������� �����

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    // �����, ����������� ���������� �������� �� �����
    public void OnDrop(PointerEventData eventData)
    {
        var draggedItem = eventData.pointerDrag?.GetComponent<ItemDragHandler>();
        Debug.Log("Dropped " + gameObject.name);
        Debug.Log("Dropped " + draggedItem.gameObject.name);

        draggedItem.SetCanBeDropped(true);    // ��������� � true �����, ������������, ��� �� ������� ������� ��� ������-���� �����

        if (slotImage.sprite.name != "����_0")  // ���� ���� �� ����, ������������ ����� ���������
        {
            draggedItem.SetSprite(slotImage.sprite);    // ��������� ������� �� ������, � ������� ���������� �������, ��� �����,
                                                        // �� �������� ���������� �������
            slotImage.sprite = draggedItem.gameObject.GetComponent<Image>().sprite; // ��������� ������� ������, � ������� ���������� �������,
                                                                                    // �� ������, �� �������� ���������� �������
            draggedItem.SetParentStatus(true);
            return;
        }
        
        draggedItem.SetParentStatus(false);

        // ������������ ������� ������������ ����� �� ������ ��������� ��������
        slotImage.sprite = draggedItem.gameObject.GetComponent<Image>().sprite;
    }
}
