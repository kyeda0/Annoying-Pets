using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    
    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
