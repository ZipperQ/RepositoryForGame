using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ����� ��� ����� ��������� �����
public class IncreaseSkills : MonoBehaviour
{
    public TextMeshProUGUI skill;   // ����� � ����������� ����� ������
    public TextMeshProUGUI freePoints;  // ����� � ����������� ��������� �����
    public Button button;   // ������, �� ������� ����� ��������, ������� ���� ������

    // ����� ��� ���������� ����� ������
    public void Increase()
    {
        if (Convert.ToInt32(freePoints.text) > 0)   // ���� ���������� ��������� ����� > 0...
        {
            skill.text = Convert.ToString(Convert.ToInt32(skill.text) + 1);
            freePoints.text = Convert.ToString(Convert.ToInt32(freePoints.text) - 1);
        }
        else
        {
            Debug.Log("Points spent");
        }
        button.gameObject.GetComponentInParent<AudioSource>().Play();   // ���� ������� �� ������
    }
}
