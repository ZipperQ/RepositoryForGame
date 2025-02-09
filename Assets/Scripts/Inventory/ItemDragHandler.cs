using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Класс, наследующий и реализующий интерфейсы для начала перетаскивания, процесса перетаскивания и конца перетаскивания предмета
// из слота в другой слот
public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;    // На каждый слот наложен компонент CanvasGroup, что необходимо для принятия предмета в слот
    private Transform originalParent;   // Родитель слота
    private Image itemImage;
    private readonly Sprite itemSprite;
    private bool shouldReturnToOriginalSlot = true; // Флаг, указывающий, остаётся ли предмет в исходном слоте или
                                                    // заменяется на спрайт пустого слота
    public Sprite emptySlot;    // Иконка пустого слота
    private Sprite originalSprite;
    private bool canBeDropped = false;    // Флаг, указывающий, был ли отпущен предмет вне какого-либо слота

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        itemImage = GetComponent<Image>();
    }

    // Метод, реализующий начало перетаскивания предмета из слота
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemImage.sprite.name == "Слот_0")  // Если слот имеет иконку пустого слота, то перетаскивать предмет из слота запрещается (т. к. он пуст)
        {
            eventData.pointerDrag = null;
            return;
        }

        originalParent = transform.parent;
        transform.SetParent(transform.root); // Перемещение в корень UI
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;   // Уменьшение перетаскиваемой иконки
        canvasGroup.blocksRaycasts = false;
    }

    // Метод, реализующий процесс перетаскивания предмета из слота
    public void OnDrag(PointerEventData eventData)
    {
        if (itemImage.sprite == itemSprite) return;

        // Получение коэффициента масштабирования по ширине экрана относительно 2560
        float scaleFactor = 2560f / Screen.width;

        // Корректировка движения с учётом разрешения экрана и следование предмета за курсором
        rectTransform.anchoredPosition += eventData.delta * scaleFactor;
    }

    // Метод, реализующий окончание перетаскивания предмета из слота
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (canBeDropped == false)  // Если предмет нельзя опустить в указанном месте...
        {
            transform.SetParent(originalParent);    // Установка оригинального родителя
            rectTransform.localPosition = Vector2.zero;
            return;
        }

        if (shouldReturnToOriginalSlot)
        {
            transform.SetParent(originalParent);
            transform.gameObject.GetComponent<Image>().sprite = originalSprite;
            rectTransform.localPosition = Vector2.zero;
        }
        else
        {
            transform.gameObject.GetComponent<Image>().sprite = emptySlot;
            transform.SetParent(originalParent);
            rectTransform.localPosition = Vector2.zero;
            shouldReturnToOriginalSlot = !shouldReturnToOriginalSlot;
        }

        canBeDropped = false;
    }

    // Метод для получения спрайта ячейки
    public Sprite GetItemSprite()
    {
        return itemSprite;
    }

    // Метод для установки спрайта от слота, в который был перенесён предмет
    public void SetSprite(Sprite sprite)
    {
        originalSprite = sprite;
    }

    // Метод, указывающий, какой родитель должен быть у переносимого предмета
    public void SetParentStatus(bool flag)
    {
        shouldReturnToOriginalSlot = flag;
    }

    // Метод, устанавливающий флаг, показывающий, был ли отпущен предмет вне какого-либо слота
    public void SetCanBeDropped(bool flag)
    {
        canBeDropped = flag;
    }
}
