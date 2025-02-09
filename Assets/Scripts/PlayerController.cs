using UnityEngine;

// ����� ��� ���������� ����������
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;    // �������� ���������
    private Rigidbody2D rb; // Rigidbody2D ���������
    private Animator animator;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // GetAxisRaw ��������� ��������� ����������� �� �������� ���������� speed � ��������� �� 0f
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;   // normalized ������������� �������� �� ��������� ������ �������� �� ������ �����

        // Conditions ��� ��������
        animator.SetBool("isDiagonal", moveX != 0 && moveY != 0);
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);

        // ���� ��������
        bool isMoving = moveInput.magnitude > 0;
        if (isMoving)
        {
            animator.SetBool("isRear", moveY == 1);
            animator.SetBool("isFront", moveY == -1);
            animator.SetBool("isLeftSide", moveX == -1);
            animator.SetBool("isRightSide", moveX == 1);
        }
        animator.SetBool("isMoving", isMoving);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;  // ����������� ���������
    }
}
