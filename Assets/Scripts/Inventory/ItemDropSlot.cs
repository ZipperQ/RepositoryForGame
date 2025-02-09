using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Класс, наследующий и реализующий интерфейс для отпускания предмета в слот
public class ItemDropSlot : MonoBehaviour, IDropHandler
{
    private Image slotImage; // Ссылка на изображение слота

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    // Метод, реализующий отпускание предмета из слота
    public void OnDrop(PointerEventData eventData)
    {
        var draggedItem = eventData.pointerDrag?.GetComponent<ItemDragHandler>();
        Debug.Log("Dropped " + gameObject.name);
        Debug.Log("Dropped " + draggedItem.gameObject.name);

        draggedItem.SetCanBeDropped(true);    // Установка в true флага, указывающего, был ли отпущен предмет вне какого-либо слота

        if (slotImage.sprite.name != "Слот_0")  // Если слот не пуст, производится обмен спрайтами
        {
            draggedItem.SetSprite(slotImage.sprite);    // Установка спрайта из ячейки, в которую перетащили предмет, для слота,
                                                        // из которого перетащили предмет
            slotImage.sprite = draggedItem.gameObject.GetComponent<Image>().sprite; // Изменение спрайта ячейки, в которую перетащили предмет,
                                                                                    // на спрайт, из которого перетащили предмет
            draggedItem.SetParentStatus(true);
            return;
        }
        
        draggedItem.SetParentStatus(false);

        // Установление спрайта принимающего слота на спрайт принятого предмета
        slotImage.sprite = draggedItem.gameObject.GetComponent<Image>().sprite;
    }
}
