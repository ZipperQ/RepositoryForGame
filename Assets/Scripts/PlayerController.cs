using UnityEngine;

//  ласс дл€ управлени€ персонажем
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;    // —корость персонажа
    private Rigidbody2D rb; // Rigidbody2D персонажа
    private Animator animator;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // GetAxisRaw позвол€ет мгновенно разгон€тьс€ до значени€ переменной speed и тормозить до 0f
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;   // normalized устанавливает скорость по диагонали равной скорости по пр€мой линии

        // Conditions дл€ анимаций
        animator.SetBool("isDiagonal", moveX != 0 && moveY != 0);
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);

        // ‘лаг движени€
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
        rb.linearVelocity = moveInput * speed;  // ѕеремещение персонажа
    }
}
