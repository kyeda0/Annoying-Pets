using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Player playerOriginal;
    [SerializeField] private GameObjectSpawner gameObjectSpawner;
    [SerializeField] private Score score;
    [SerializeField] private GameObject panelForGameOver;
    [SerializeField] private UIHealthPlayer uIHealthPlayer;
    [SerializeField] private GameObject hud;
    [SerializeField] private TextBuff textBuff;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject panelForPause;
    [SerializeField] private CoinsText coinsText;
    private Player playerClone;
    private  StageGame stage;
    private void Start()
    {
        ChangeGameStage(StageGame.StartStage);
    }

    private void MainStageGame()
    {
        switch (stage)
        {
            case StageGame.StartStage:
                panelForGameOver.SetActive(false);
                playerClone = Instantiate(playerOriginal,new Vector2(0f,-4f),Quaternion.identity);
                playerClone.OnGameOverEvent += HandleGameOverStage;
                playerClone.OnTakeDamage += HandleTakeDamageUI; 
                playerClone.OnCheckSpeed += HandleSetMusic;
                playerClone.OnCheckCoins += HandleUpdateCoins;
                score.gameObject.SetActive(true);
                gameObjectSpawner.OnSpawnedBlock += HandleBlockSpawned;
                gameObjectSpawner.targetPlayer = playerClone;
                ChangeGameStage(StageGame.PlayningStage);
                break;
            case StageGame.PlayningStage:
                playerClone.isMoving = true;
                gameObjectSpawner.StartSpawnGameObject();
                gameObjectSpawner.StartSpawnedObject();
                panelForPause.SetActive(false);
                hud.SetActive(true);
                break;
            case StageGame.GameOverStage:
                gameObjectSpawner.StopSpawnGameObject();
                gameObjectSpawner.StopSpawnedObject();
                panelForGameOver.SetActive(true);
                score.ShowBestScore();
                playerClone.isMoving = false;
                hud.SetActive(false);
                playerClone.ChangeSpeed(Player.LevelSpeed.ZeroSpeed);
                Debug.Log("GameOver");
                break;
            case StageGame.PauseStage:
                gameObjectSpawner.StopSpawnGameObject();
                gameObjectSpawner.StopSpawnedObject();
                panelForPause.SetActive(true);
                playerClone.isMoving = false;
                hud.SetActive(false);
                break;
            case StageGame.TutorialStage:
                
                break;
        }
    }

    private void ChangeGameStage(StageGame currentStage)
    {
        stage = currentStage;
        MainStageGame();
    }

    private void HandleBlockSpawned(GameObjects block)
    {
        block.OnAddScore += HandleAddScore;
        block.onTextBuff += HandleTextBuff;
    }
    private void HandleGameOverStage()
    {
        ChangeGameStage(StageGame.GameOverStage);
    }    
    public enum StageGame
    {
        StartStage,
        PlayningStage,
        GameOverStage,
        PauseStage,
        TutorialStage
    }

    private void HandleAddScore()
    {
        score.AddScore();
    }

    private void HandleTakeDamageUI(float health,float maxHealth)
    {
        uIHealthPlayer.UpdateHealthUI(health,maxHealth);
    }
    private void HandleTextBuff(TextMeshPro textMeshPro)
    {
        textBuff.StartAnimationText(textMeshPro,playerClone);
    }

    public void RestartGame()
    {
        playerClone.OnGameOverEvent -= HandleGameOverStage;
        playerClone.OnTakeDamage -= HandleTakeDamageUI; 
        gameObjectSpawner.OnSpawnedBlock -= HandleBlockSpawned;
        SceneManager.LoadScene("MainScene");
    }

    public void ExitInMenu()
    {
        playerClone.OnGameOverEvent -= HandleGameOverStage;
        playerClone.OnTakeDamage -= HandleTakeDamageUI; 
        gameObjectSpawner.OnSpawnedBlock -= HandleBlockSpawned;
        SceneManager.LoadScene("MenuScene");
    }

    private void HandleSetMusic(Player.LevelSpeed levelSpeed)
    {
        if(audioManager.isMusicOn == true)
        {
            audioManager.SetMusic(levelSpeed);
        }
    }

    public void HandleStagePause()
    {
        ChangeGameStage(StageGame.PauseStage);
    }

    public void HandlePlayningStage()
    {
        ChangeGameStage(StageGame.PlayningStage);
    }

    public void HandleUpdateCoins(int coin)
    {
        coinsText.UpdateCoinsText(coin);
    }
}
