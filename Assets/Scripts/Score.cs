using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Canvas canvas;
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
        var text = Instantiate(textMeshPro,transform);
        text.transform.localPosition = new Vector2(80f,0f);
        StartCoroutine(AnimationScore(1f,text));
    }

    public void ShowBestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore",bestScore);
            PlayerPrefs.Save();
            YG2.SetLeaderboard("BestPets",bestScore);
            ShowScore();
        }
    }

    private IEnumerator AnimationScore(float timeAnim,TextMeshProUGUI textMesh)
    {
        float time = 0f;
        Vector2 startVector = new Vector2(200f,0f);
        Vector2 endVector = new Vector2(85f,0f);
        Color startColor = new Color(1f,1f,1f,1f);
        Color endColor = new Color(1f,1f,1f,0f);
        textMesh.text = "+1";
        while(time < timeAnim)
        {
            time += Time.deltaTime;
            float t = time/timeAnim;
            textMesh.transform.localPosition = Vector2.Lerp(startVector,endVector,t);
            textMesh.color = Color.Lerp(startColor,endColor,t);
            
            yield return null;
        }
        score++;
        ShowScore();
        Destroy(textMesh.gameObject);
    }
}
