using UnityEngine;
using UnityEngine.UI;

// Класс, представляющий ячейку инвентаря
public class InventorySlot : MonoBehaviour
{
    public Image icon;  // Изображение ячейки
    private Item item;  // Предмет от ScriptableObject, хранящийся в ячейке

    // Метод для добавления изображения подобранного предмета в ячейку инвентаря
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
    }
}
