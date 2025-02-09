using UnityEngine;
using UnityEngine.UI;

// �����, �������������� ������ ���������
public class InventorySlot : MonoBehaviour
{
    public Image icon;  // ����������� ������
    private Item item;  // ������� �� ScriptableObject, ���������� � ������

    // ����� ��� ���������� ����������� ������������ �������� � ������ ���������
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
    }
}
