using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SquareController : MonoBehaviour
{
    private Transform tf;
    public PolygonCollider2D boundaryCollider;  // ��� ��������������
    private BoxCollider2D smallCollider;    // ��� ��������
    // �������� ����������
    public TextMeshProUGUI coordinatesText;
    public TMP_InputField speedInputField;
    public Animator animator;

    public float speed = 5.0f;  // �������� �����

    private void Start()
    {
        tf = GetComponent<Transform>();
        smallCollider = GetComponent<BoxCollider2D>();  // ��������� �������
        animator = GetComponent<Animator>();

        // ���������� �������� �� ����
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
        coordinatesText.text = $"Coordinates: ({tf.position.x:F2}, {tf.position.y:F2})";    // ���������� ��������

        // ����������� ��������
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(moveX, moveY, 0).normalized * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + moveDirection;

        // ��������, ����� �� ��������� � ��� ������� (���� ������ ��� �������)
        bool canMoveFull = IsInsideBoundary(newPosition);

        if (!canMoveFull)
        {
            // �������� �������� ������ �� X
            Vector3 xOnlyPosition = transform.position + new Vector3(moveDirection.x, 0, 0);
            bool canMoveX = IsInsideBoundary(xOnlyPosition);

            // �������� �������� ������ �� Y
            Vector3 yOnlyPosition = transform.position + new Vector3(0, moveDirection.y, 0);
            bool canMoveY = IsInsideBoundary(yOnlyPosition);

            if (canMoveX) newPosition = xOnlyPosition;
            if (canMoveY) newPosition = yOnlyPosition;
        }

        transform.position = newPosition;
    }

    // ��������, ��������� �� ���� �� ���� ���� ������ ������
    bool IsInsideBoundary(Vector3 position)
    {
        Vector3[] corners = GetBoxCorners(position);

        foreach (Vector3 corner in corners)
        {
            if (!boundaryCollider.OverlapPoint(corner))
            {
                return false; // ���� ���� �� ���� ���� ����� �� �������, �������� �����������
            }
        }
        return true; // �������� ���������, ���� ��� ���� ������
    }

    // ���������� ���� 4 ����� ���������� ��������
    Vector3[] GetBoxCorners(Vector3 position)
    {
        float halfWidth = smallCollider.bounds.extents.x;
        float halfHeight = smallCollider.bounds.extents.y;

        return new Vector3[]
        {
            new Vector3(position.x - halfWidth, position.y - halfHeight, 0), // ����� ������
            new Vector3(position.x + halfWidth, position.y - halfHeight, 0), // ������ ������
            new Vector3(position.x - halfWidth, position.y + halfHeight, 0), // ����� �������
            new Vector3(position.x + halfWidth, position.y + halfHeight, 0)  // ������ �������
        };
    }

    // ���������� �������� ����� ���� �����
    void UpdateSpeed(string input)
    {
        if (float.TryParse(input, out float newSpeed))
        {
            speed = Mathf.Clamp(newSpeed, 0f, 20f);
        }
    }
}
