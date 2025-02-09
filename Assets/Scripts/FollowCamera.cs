using UnityEngine;

// ����� ��� ���������� ������ �� ����������
public class Follow2D : MonoBehaviour
{
    public Transform target;           // ������ �� ���� (��������), �� ������� ����� ���������
    public float heightOffset = 1f;    // ������ �� Y (���� ��� ���� �� ��� Y)
    public float followSpeed = 5f;      // �������� ����������

    private void LateUpdate()
    {
        if (target != null)
        {
            // ������������ ����� �������, �� ��������� Z �� -10 ��� orthographic camera
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y + heightOffset, -10f);

            // ������������ ��� �������� ����������
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
