using UnityEngine;
using System.Linq;
using UnityEngine.UI;

// Êëàññ, ïðåäñòàâëÿþùèé èíâåíòàðü
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Transform slotsParent;   // Ðîäèòåëü âñåõ ñëîòîâ èíâåíòàðÿ

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AddItem(Item item)
    {
        // Âñå ñëîòû â äåòÿõ äåòåé ñ êîìïîíåíòîì InventorySlot (ñêðèïò)
        InventorySlot[] slots = slotsParent.Cast<Transform>()
            .Select(t => t.childCount > 1 ? t.GetChild(1).GetComponent<InventorySlot>() : null)
            .Where(slot => slot != null)
            .ToArray();

        // Âñå ñëîòû â äåòÿõ äåòåé ñ êîìïîíåíòîì Image
        Image[] slotsImage = slotsParent.Cast<Transform>()
            .Select(t => t.childCount > 1 ? t.GetChild(1).GetComponent<Image>() : null)
            .Where(Slot => Slot != null)
            .ToArray();

        // Ïðîõîæäåíèå âñåõ ýëåìåíòîâ â slotsImage è óñòàíîâëåíèå èäåíòè÷íûõ ñïðàéòîâ â ñîîòâåòñòâóþùèõ ýëåìåíòàõ ìàññèâà slots
        for (int i = 0; i < slotsImage.Length; i++)
        {
            slots[i].icon.sprite = slotsImage[i].sprite;
        }

        // Äîáàâëåíèå ïðåäìåòà â ïåðâóþ ñâîáîäíóþ ÿ÷åéêó
        for (int i = 0; i < slotsImage.Length; i++)
        {
            if (slots[i].icon.sprite.name == "Ñëîò_0")  // Ñëîò_0 - èìÿ ñïðàéòà, îáîçíà÷àþùåãî ïóñòîé ñëîò
            {
                slots[i].AddItem(item);
                break;
            }
        }
    }

    // Ìåòîä äëÿ ïîëó÷åíèÿ êîëè÷åñòâà ñâîáîäíûõ ÿ÷ååê
    public int EmptySlotsCount()
    {
        // Âñå ñëîòû â äåòÿõ äåòåé ñ êîìïîíåíòîì Image
        Image[] slotsImage = slotsParent.Cast<Transform>()
            .Select(t => t.childCount > 1 ? t.GetChild(1).GetComponent<Image>() : null)
            .Where(Slot => Slot != null)
            .ToArray();

        int n = 0;
        for (int i = 0; i < slotsImage.Length; i++)
        {
            if (slotsImage[i].sprite.name == "Ñëîò_0") n++;
        }

        return n;
    }
}
