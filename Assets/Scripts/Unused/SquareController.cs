using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SquareController : MonoBehaviour
{
    private Transform tf;
    public PolygonCollider2D boundaryCollider;  // Для прямоугольника
    private BoxCollider2D smallCollider;    // Для квадрата
    // Элементы интерфейса
    public TextMeshProUGUI coordinatesText;
    public TMP_InputField speedInputField;
    public Animator animator;

    public float speed = 5.0f;  // Скорость круга

    private void Start()
    {
        tf = GetComponent<Transform>();
        smallCollider = GetComponent<BoxCollider2D>();  // Маленький квадрат
        animator = GetComponent<Animator>();

        // Считывание скорости из поля
        if (speedInputField != null)
        {
            speedInputField.text = speed.ToString();
            speedInputField.onEndEdit.AddListener(UpdateSpeed);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Menu");
        }
        coordinatesText.text = $"Coordinates: ({tf.position.x:F2}, {tf.position.y:F2})";    // Координаты квадрата

        // Перемещение квадрата
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(moveX, moveY, 0).normalized * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + moveDirection;

        // Проверка, можно ли двигаться в обе стороны (если зажаты две клавиши)
        bool canMoveFull = IsInsideBoundary(newPosition);

        if (!canMoveFull)
        {
            // Проверка движения только по X
            Vector3 xOnlyPosition = transform.position + new Vector3(moveDirection.x, 0, 0);
            bool canMoveX = IsInsideBoundary(xOnlyPosition);

            // Проверка движения только по Y
            Vector3 yOnlyPosition = transform.position + new Vector3(0, moveDirection.y, 0);
            bool canMoveY = IsInsideBoundary(yOnlyPosition);

            if (canMoveX) newPosition = xOnlyPosition;
            if (canMoveY) newPosition = yOnlyPosition;
        }

        transform.position = newPosition;
    }

    // Проверка, находится ли хотя бы один угол внутри границ
    bool IsInsideBoundary(Vector3 position)
    {
        Vector3[] corners = GetBoxCorners(position);

        foreach (Vector3 corner in corners)
        {
            if (!boundaryCollider.OverlapPoint(corner))
            {
                return false; // Если хотя бы один угол вышел за границу, движение запрещается
            }
        }
        return true; // Движение разрешено, если все углы внутри
    }

    // Координаты всех 4 углов маленького квадрата
    Vector3[] GetBoxCorners(Vector3 position)
    {
        float halfWidth = smallCollider.bounds.extents.x;
        float halfHeight = smallCollider.bounds.extents.y;

        return new Vector3[]
        {
            new Vector3(position.x - halfWidth, position.y - halfHeight, 0), // Левый нижний
            new Vector3(position.x + halfWidth, position.y - halfHeight, 0), // Правый нижний
            new Vector3(position.x - halfWidth, position.y + halfHeight, 0), // Левый верхний
            new Vector3(position.x + halfWidth, position.y + halfHeight, 0)  // Правый верхний
        };
    }

    // Обновление скорости через поле ввода
    void UpdateSpeed(string input)
    {
        if (float.TryParse(input, out float newSpeed))
        {
            speed = Mathf.Clamp(newSpeed, 0f, 20f);
        }
    }
}
