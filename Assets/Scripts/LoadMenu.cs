using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public void ExitInMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
