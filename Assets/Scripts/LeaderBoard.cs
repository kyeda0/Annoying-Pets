using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject leaderBoard;

    private bool isClicked;


    public void ToggleLeaderBoard()
    {
        isClicked = !isClicked;
        OnAndOffLeaderBoard();
    }
    private void OnAndOffLeaderBoard()
    {
        if(isClicked == true)
        {
            hud.SetActive(false);
            leaderBoard.SetActive(true);
        }
        else
        {
            hud.SetActive(true);
            leaderBoard.SetActive(false);
        }
    }
}
