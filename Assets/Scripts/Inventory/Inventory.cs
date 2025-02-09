using UnityEngine;
using System.Linq;
using UnityEngine.UI;

// �����, �������������� ���������
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Transform slotsParent;   // �������� ���� ������ ���������

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AddItem(Item item)
    {
        // ��� ����� � ����� ����� � ����������� InventorySlot (������)
        InventorySlot[] slots = slotsParent.Cast<Transform>()
            .Select(t => t.childCount > 1 ? t.GetChild(1).GetComponent<InventorySlot>() : null)
            .Where(slot => slot != null)
            .ToArray();

        // ��� ����� � ����� ����� � ����������� Image
        Image[] slotsImage = slotsParent.Cast<Transform>()
            .Select(t => t.childCount > 1 ? t.GetChild(1).GetComponent<Image>() : null)
            .Where(Slot => Slot != null)
            .ToArray();

        // ����������� ���� ��������� � slotsImage � ������������ ���������� �������� � ��������������� ��������� ������� slots
        for (int i = 0; i < slotsImage.Length; i++)
        {
            slots[i].icon.sprite = slotsImage[i].sprite;
        }

        // ���������� �������� � ������ ��������� ������
        for (int i = 0; i < slotsImage.Length; i++)
        {
            if (slots[i].icon.sprite.name == "����_0")  // ����_0 - ��� �������, ������������� ������ ����
            {
                slots[i].AddItem(item);
                break;
            }
        }
    }

    // ����� ��� ��������� ���������� ��������� �����
    public int EmptySlotsCount()
    {
        // ��� ����� � ����� ����� � ����������� Image
        Image[] slotsImage = slotsParent.Cast<Transform>()
            .Select(t => t.childCount > 1 ? t.GetChild(1).GetComponent<Image>() : null)
            .Where(Slot => Slot != null)
            .ToArray();

        int n = 0;
        for (int i = 0; i < slotsImage.Length; i++)
        {
            if (slotsImage[i].sprite.name == "����_0") n++;
        }

        return n;
    }
}
