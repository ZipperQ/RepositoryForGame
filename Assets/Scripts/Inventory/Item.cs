using UnityEngine;

// �����, ����������� �� ScriptableObject ��� ��������� ������ � ����������, ���������� ����������� � ����� ���������
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // ��� ��������
    public Sprite icon; // ������ ��������

    // TODO: ���������� �������� �������� �������� � ��� ��������, ��������:
    // public string description;
    // public string itemType;
}
