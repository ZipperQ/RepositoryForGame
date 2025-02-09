using UnityEngine;

// Класс для подбирания персонажем предмета
public class PlayerPickup : MonoBehaviour
{
    public Inventory inventory;

    private void OnTriggerEnter2D(Collider2D collision) // Метод, вызывающийся при коллизии с предметом в локации
    {
        ItemPickup itemPickup = collision.GetComponent<ItemPickup>();
        if (itemPickup && inventory.EmptySlotsCount() != 0) // Если itemPickup не null количество свободных ячеек >0...
        {
            // Добавление предмета в инвентарь и удаление забранного предмета со сцены
            inventory.AddItem(itemPickup.item);
            Destroy(collision.gameObject);
        }
    }
}
