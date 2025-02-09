using UnityEngine;
using System.Collections;

// Класс для управления меню (свитком и его содержимым)
public class GameMenu : MonoBehaviour
{
    public GameObject menuPanel; // Ссылка на меню (свиток)
    public GameObject UIPanel;  // Ссылка на меню (содержимое свитка)
    public Animator menuAnimator; // Ссылка на аниматор (анимация разворачивания и сворачивания свитка)
    public AudioSource sound;   // Звук разворачивания свитка

    private bool isMenuOpen = false; // Флаг состояния меню
    private float time = 0f;    // Переменная, необходимая для подсчёта времени с начала разворачивания или сворачивания свитка
    private bool canPressed = true; // Можно ли свернуть или развернуть свиток при нажатии клавиши

    private void Start()
    {
        // Скрытие всех компонентов меню при старте игры
        menuPanel.SetActive(false);
        menuAnimator.gameObject.SetActive(false);
        UIPanel.gameObject.SetActive(false);

        // Скрытие курсора (на текущий момент механизм скрытия/раскрытия разработан не до конца)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (((Input.GetKeyDown(KeyCode.I)) || (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)) && canPressed) // Открытие/закрытие меню по I/ESC
        {
            canPressed = false; // Можно ли уже нажимать
            ToggleMenu();   // Метод для анимаций сворачивания и разворачивания свитка
        }

        //*** TODO ***\\
        if (!canPressed)
        {
            time += Time.deltaTime;

            if (time > 0.42f)
            {
                time = 0f;
                canPressed = true;
            }
        }

        //*** End TODO ***\\

        if (isMenuOpen) // Если меню открыто...
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Метод для анимаций сворачивания и разворачивания свитка
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen)
        {
            menuPanel.SetActive(true);
            menuAnimator.gameObject.SetActive(true);
            menuAnimator.Play("MenuOpen"); // Проигрываем анимацию
            sound.Play();
            StartCoroutine(EnableAfterAnimation()); // Корутина для ожидания окончания анимации разворачивания
        }
        else
        {
            UIPanel.gameObject.SetActive(false);
            menuAnimator.Play("MenuClose");
            sound.Play();
            StartCoroutine(DisableAfterAnimation());    // Корутина для ожидания окончания анимации сворачивания
        }
    }

    private IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(menuAnimator.GetCurrentAnimatorStateInfo(0).length);    // Ожидание конца анимации

        // Отключение меню
        menuPanel.SetActive(false);
        menuAnimator.gameObject.SetActive(false);
    }

    private IEnumerator EnableAfterAnimation()
    {
        yield return new WaitForSeconds(menuAnimator.GetCurrentAnimatorStateInfo(0).length);    // Ожидание конца анимации
        UIPanel.gameObject.SetActive(true); // Включение содержимого свитка
    }
}
