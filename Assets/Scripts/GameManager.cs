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
                playerClone.isMoving = true;
                score.gameObject.SetActive(true);
                gameObjectSpawner.OnSpawnedBlock += HandleBlockSpawned;
                gameObjectSpawner.targetPlayer = playerClone;
                hud.SetActive(true);
                ChangeGameStage(StageGame.PlayningStage);
                break;
            case StageGame.PlayningStage:
                gameObjectSpawner.StartSpawnGameObject();
                break;
            case StageGame.GameOverStage:
                gameObjectSpawner.StopSpawnGameObject();
                panelForGameOver.SetActive(true);
                score.ShowBestScore();
                playerClone.isMoving = false;
                hud.SetActive(false);
                playerClone.ChangeSpeed(Player.LevelSpeed.ZeroSpeed);
                Debug.Log("GameOver");
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
        GameOverStage
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
        SceneManager.LoadScene("MenuScene");
    }

    private void HandleSetMusic(Player.LevelSpeed levelSpeed)
    {
        audioManager.SetMusic(levelSpeed);
    }

}
