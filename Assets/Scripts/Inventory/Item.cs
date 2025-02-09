using UnityEngine;

// Класс, наследуемый от ScriptableObject для упрощения работы с предметами, способными добавляться в слоты инвентаря
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Имя предмета
    public Sprite icon; // Иконка предмета

    // TODO: Необходимо добавить описание предмета и тип предмета, например:
    // public string description;
    // public string itemType;
}
