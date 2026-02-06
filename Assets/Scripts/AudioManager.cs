using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private GameObject buttonSwitch;
    [SerializeField] private Sprite[] imagesForButton;
    [SerializeField] private AudioClip[] musicForGameStage;
    private bool isMusicOn;


    private void Start()
    {
        isMusicOn = PlayerPrefs.GetInt("Music",1) == 1;
        OnAndOffMusic();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("Music",isMusicOn ? 1:0);
        PlayerPrefs.Save();
        OnAndOffMusic();
    }

    public void SetMusic(Player.LevelSpeed levelSpeed)
    {
        musicSource.Pause();
        switch (levelSpeed)
        {
            case Player.LevelSpeed.Slow:
                musicSource.resource = musicForGameStage[0];
                break;
            case Player.LevelSpeed.Normal:
                musicSource.resource = musicForGameStage[1];
                break;
            case Player.LevelSpeed.Fast:
                musicSource.resource = musicForGameStage[2];
                break;
        }
        musicSource.Play();
    }
    private void OnAndOffMusic()
    {
        if(isMusicOn == true)
        {
            musicSource.Play();
            buttonSwitch.GetComponent<Image>().sprite = imagesForButton[0];
        }
        else if(isMusicOn == false)
        {
            musicSource.Pause();
            buttonSwitch.GetComponent<Image>().sprite = imagesForButton[1];
        }
    }

}
