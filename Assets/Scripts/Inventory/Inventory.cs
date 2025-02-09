using UnityEngine;
using System.Linq;
using UnityEngine.UI;

// Класс, представляющий инвентарь
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Transform slotsParent;   // Родитель всех слотов инвентаря

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AddItem(Item item)
    {
        // Все слоты в детях детей с компонентом InventorySlot (скрипт)
        InventorySlot[] slots = slotsParent.Cast<Transform>()
            .Select(t => t.childCount > 1 ? t.GetChild(1).GetComponent<InventorySlot>() : null)
            .Where(slot => slot != null)
            .ToArray();

        // Все слоты в детях детей с компонентом Image
        Image[] slotsImage = slotsParent.Cast<Transform>()
            .Select(t => t.childCount > 1 ? t.GetChild(1).GetComponent<Image>() : null)
            .Where(Slot => Slot != null)
            .ToArray();

        // Прохождение всех элементов в slotsImage и установление идентичных спрайтов в соответствующих элементах массива slots
        for (int i = 0; i < slotsImage.Length; i++)
        {
            slots[i].icon.sprite = slotsImage[i].sprite;
        }

        // Добавление предмета в первую свободную ячейку
        for (int i = 0; i < slotsImage.Length; i++)
        {
            if (slots[i].icon.sprite.name == "Слот_0")  // Слот_0 - имя спрайта, обозначающего пустой слот
            {
                slots[i].AddItem(item);
                break;
            }
        }
    }

    // Метод для получения количества свободных ячеек
    public int EmptySlotsCount()
    {
        // Все слоты в детях детей с компонентом Image
        Image[] slotsImage = slotsParent.Cast<Transform>()
            .Select(t => t.childCount > 1 ? t.GetChild(1).GetComponent<Image>() : null)
            .Where(Slot => Slot != null)
            .ToArray();

        int n = 0;
        for (int i = 0; i < slotsImage.Length; i++)
        {
            if (slotsImage[i].sprite.name == "Слот_0") n++;
        }

        return n;
    }
}
