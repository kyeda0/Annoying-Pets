using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObjectSpawner gameObjectSpawner;
    [SerializeField] private TextMeshProUGUI textForTutorial;
    [SerializeField] private GameObjects[] blocksForTutorial;
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private AudioManager audioManager;
    private bool isTextFull = false;
    private StageTutorial stageTutorial;
    private Coroutine coroutineTextAnimation;
    public int isFirstGame = 0;

    public event Action OnFinishedTutorial;
    

    private IEnumerator TimerForStages(StageTutorial currentStage,float timer)
    {
        float time = 0f;
        while(time < timer)
        {
            time += Time.deltaTime;
            yield return null;
        }

        ChangeStageTutorial(currentStage);
    }

    private void MainStageTutorial()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
        switch (stageTutorial)
        {
            case StageTutorial.FirstStage:
                FirstStage();
                break;
            case StageTutorial.TwoStage:
                TwoStage();
                break;
            case StageTutorial.ThreeStage:
                ThreeStage();
                break;
            case StageTutorial.FourStage:
                FourStage();
                break;
            case StageTutorial.FiveStage:
                FiveStage();
                break;
        }
    }
    public void ChangeStageTutorial(StageTutorial currentStage)
    {
        stageTutorial = currentStage;
        MainStageTutorial();
    }

    public enum StageTutorial
    {
        FirstStage,
        TwoStage,
        ThreeStage,
        FourStage,
        FiveStage
    }

    private void FirstStage()
    {
        textForTutorial.gameObject.SetActive(true);
        ChangeLanguage("Нажми слева или справа, чтобы двигаться","Tap left or right to move",0);
    }
    private void TwoStage()
    {
        ChangeLanguage("Уворачивайся от объектов, чтобы получить Очки","Dodge objects to get a Score.",1);
        StartCoroutine(TimerForStages(StageTutorial.ThreeStage,5f));
    }

    private void ThreeStage()
    {
        gameObjectSpawner.targetPlayer.isMoving = false;
        ChangeLanguage("Пойманный объект отнимает здоровье","A trapped object takes away health",2);
        StartCoroutine(TimerForStages(StageTutorial.FourStage,5f));
    }

    private  void FourStage()
    {
        gameObjectSpawner.targetPlayer.isMoving = false;
        ChangeLanguage("Бонусы дают эффект, но они отнимают 1 жизнь","Power-ups have an effect, but they take 1 life.",3);
        StartCoroutine(TimerForStages(StageTutorial.FiveStage,5f));
    }

    private void FiveStage()
    {
        ChangeLanguage("Набери максимальное кол-во очков и стань номер один!","Get the maximum number of points and become number one!",4);
    }

    private void ChangeLanguage(string textForRussian,string textForEngland,int indexGameObjects)
    {
        if(coroutineTextAnimation != null)
        {
            StopCoroutine(coroutineTextAnimation);
        }
        if(YG2.lang == "ru")
        {
          coroutineTextAnimation = StartCoroutine(AnimationForText(0.05f,textForRussian,indexGameObjects));
        }
        if(YG2.lang == "en")
        {
          coroutineTextAnimation = StartCoroutine(AnimationForText(0.05f,textForEngland,indexGameObjects));
        }
    }

    private void Test(int indexGameObjects)
    {
        if(isTextFull == true && indexGameObjects < 4)
        {
            gameObjectSpawner.StartSpawnObjectForTutorial(blocksForTutorial[indexGameObjects]);
        }
        else if (isTextFull == true && indexGameObjects == 4)
        {
            StartCoroutine(TimerForFiveStage(4f));
        }
    }

    private IEnumerator AnimationForText(float duration,string text,int indexGameObjects)
    {
        textForTutorial.text = "";
        isTextFull = false;
        for (int i = 0; i < text.Length; i++)
        {
            textForTutorial.text += text[i];
            audioManager.AudioForText();
            yield return new WaitForSeconds(duration);
        }
        isTextFull = true;
        Test(indexGameObjects); 
    }
    private IEnumerator TimerForFiveStage(float timer)
    {
        float time = 0f;

        while(time < timer)
        {
            time += Time.deltaTime;
            yield return null;
        }
        OnFinishedTutorial?.Invoke();
        textForTutorial.gameObject.SetActive(false);
        PlayerPrefs.SetInt("IsFirstGame",isFirstGame = 1);
    }
}
