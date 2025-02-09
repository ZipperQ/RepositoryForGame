using UnityEngine;

// Класс для следования камеры за персонажем
public class Follow2D : MonoBehaviour
{
    public Transform target;           // Ссылка на цель (персонаж), за которым нужно следовать
    public float heightOffset = 1f;    // Отступ по Y (выше или ниже по оси Y)
    public float followSpeed = 5f;      // Скорость следования

    private void LateUpdate()
    {
        if (target != null)
        {
            // Рассчитываем новую позицию, но фиксируем Z на -10 для orthographic camera
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y + heightOffset, -10f);

            // Интерполяция для плавного следования
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
