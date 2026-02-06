using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using YG;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestText;
    private int score;

    private int bestScore;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        ShowScore();
    }

    private void ShowScore()
    {
        if(YG2.lang == "en")
        {
            GetComponent<TextMeshProUGUI>().text = "Score: " + score;
            bestText.text = "BestScore: " + bestScore;
        }
        else if(YG2.lang == "ru")
        {
            GetComponent<TextMeshProUGUI>().text = "Счет: " + score;
            bestText.text = "Лучший рекорд: " + bestScore;
        }
    }
    public void AddScore()
    {      
        score++;
        ShowScore();
    }

    public void ShowBestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore",bestScore);
        }
    }
}
