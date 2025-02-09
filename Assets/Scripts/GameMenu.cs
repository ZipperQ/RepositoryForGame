using UnityEngine;
using System.Collections;

// ����� ��� ���������� ���� (������� � ��� ����������)
public class GameMenu : MonoBehaviour
{
    public GameObject menuPanel; // ������ �� ���� (������)
    public GameObject UIPanel;  // ������ �� ���� (���������� ������)
    public Animator menuAnimator; // ������ �� �������� (�������� �������������� � ������������ ������)
    public AudioSource sound;   // ���� �������������� ������

    private bool isMenuOpen = false; // ���� ��������� ����
    private float time = 0f;    // ����������, ����������� ��� �������� ������� � ������ �������������� ��� ������������ ������
    private bool canPressed = true; // ����� �� �������� ��� ���������� ������ ��� ������� �������

    private void Start()
    {
        // ������� ���� ����������� ���� ��� ������ ����
        menuPanel.SetActive(false);
        menuAnimator.gameObject.SetActive(false);
        UIPanel.gameObject.SetActive(false);

        // ������� ������� (�� ������� ������ �������� �������/��������� ���������� �� �� �����)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (((Input.GetKeyDown(KeyCode.I)) || (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)) && canPressed) // ��������/�������� ���� �� I/ESC
        {
            canPressed = false; // ����� �� ��� ��������
            ToggleMenu();   // ����� ��� �������� ������������ � �������������� ������
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

        if (isMenuOpen) // ���� ���� �������...
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

    // ����� ��� �������� ������������ � �������������� ������
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen)
        {
            menuPanel.SetActive(true);
            menuAnimator.gameObject.SetActive(true);
            menuAnimator.Play("MenuOpen"); // ����������� ��������
            sound.Play();
            StartCoroutine(EnableAfterAnimation()); // �������� ��� �������� ��������� �������� ��������������
        }
        else
        {
            UIPanel.gameObject.SetActive(false);
            menuAnimator.Play("MenuClose");
            sound.Play();
            StartCoroutine(DisableAfterAnimation());    // �������� ��� �������� ��������� �������� ������������
        }
    }

    private IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(menuAnimator.GetCurrentAnimatorStateInfo(0).length);    // �������� ����� ��������

        // ���������� ����
        menuPanel.SetActive(false);
        menuAnimator.gameObject.SetActive(false);
    }

    private IEnumerator EnableAfterAnimation()
    {
        yield return new WaitForSeconds(menuAnimator.GetCurrentAnimatorStateInfo(0).length);    // �������� ����� ��������
        UIPanel.gameObject.SetActive(true); // ��������� ����������� ������
    }
}
