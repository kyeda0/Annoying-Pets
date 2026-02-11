using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip audioClipForButton;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private GameObject buttonSwitch;
    [SerializeField] private Sprite[] imagesForButton;
    [SerializeField] private AudioClip[] musicForGameStage;
    public bool isMusicOn;


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
        switch (levelSpeed)
        {
            case Player.LevelSpeed.Slow:
                musicSource.clip = musicForGameStage[0];
                break;
            case Player.LevelSpeed.Normal:
                musicSource.clip = musicForGameStage[1];
                break;
            case Player.LevelSpeed.Fast:
                musicSource.clip = musicForGameStage[2];
                break;
            case Player.LevelSpeed.UltraFast:
                musicSource.clip = musicForGameStage[3];
                break;
            case Player.LevelSpeed.ZeroSpeed:

                break;
        }
        musicSource.Play();
    }
    private void OnAndOffMusic()
    {
        if(isMusicOn == true)
        {
            musicSource.Play();
            if(buttonSwitch != null)
            {
                buttonSwitch.GetComponent<Image>().sprite = imagesForButton[0];
            }
        }
        else if(isMusicOn == false)
        {
            musicSource.Pause();
            if(buttonSwitch != null)
            {
                buttonSwitch.GetComponent<Image>().sprite = imagesForButton[1];
            }    
        }
    }


    public void AudioForButton()
    {
        sfxSource.PlayOneShot(audioClipForButton);
    }

}
