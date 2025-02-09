using UnityEngine;

// ����� ��� ���������� ���������� ��������
public class PlayerPickup : MonoBehaviour
{
    public Inventory inventory;

    private void OnTriggerEnter2D(Collider2D collision) // �����, ������������ ��� �������� � ��������� � �������
    {
        ItemPickup itemPickup = collision.GetComponent<ItemPickup>();
        if (itemPickup && inventory.EmptySlotsCount() != 0) // ���� itemPickup �� null ���������� ��������� ����� >0...
        {
            // ���������� �������� � ��������� � �������� ���������� �������� �� �����
            inventory.AddItem(itemPickup.item);
            Destroy(collision.gameObject);
        }
    }
}
