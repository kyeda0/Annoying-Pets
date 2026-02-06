using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGameMenu : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private TextMeshProUGUI[] textsMenu;
    [SerializeField] private GameObject[] buttonsMenu;

    private void Start()
    {
        StartCoroutine(StartMenu(time,textsMenu,buttonsMenu));
    }

    private IEnumerator StartMenu(float timeDuration,TextMeshProUGUI[] texts, GameObject[] buttons)
    {
        Color startColor = new Color(1f,1f,1f,0f);
        Color endColor = new Color(1f,1f,1f,1f);
        float time = 0f;
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = startColor;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = startColor;
        }
        while (time < timeDuration)
        {
            time += Time.deltaTime;
            float t = time/timeDuration;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = Color.Lerp(startColor,endColor,t);
            }
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = Color.Lerp(startColor,endColor,t);
            }
            yield return null;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = endColor;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = endColor;
        }
    }

}
