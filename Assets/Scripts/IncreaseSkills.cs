using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Класс для траты свободных очков
public class IncreaseSkills : MonoBehaviour
{
    public TextMeshProUGUI skill;   // Текст с количеством очков навыка
    public TextMeshProUGUI freePoints;  // Текст с количеством свободных очков
    public Button button;   // Кнопка, на которую можно нажимать, повышая очки навыка

    // Метод для увеличения очков навыка
    public void Increase()
    {
        if (Convert.ToInt32(freePoints.text) > 0)   // Если количество свободных очков > 0...
        {
            skill.text = Convert.ToString(Convert.ToInt32(skill.text) + 1);
            freePoints.text = Convert.ToString(Convert.ToInt32(freePoints.text) - 1);
        }
        else
        {
            Debug.Log("Points spent");
        }
        button.gameObject.GetComponentInParent<AudioSource>().Play();   // Звук нажатия на кнопку
    }
}
