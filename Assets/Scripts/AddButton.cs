using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using YG;
public class AddButton : MonoBehaviour
{
    [SerializeField] private Button adButton;
    [SerializeField] private string idReward = "coin";
    [SerializeField] private int coinsReward;
    [SerializeField] private ManagerShop managerShop;


    public  void ShowAd()
    {
        YG2.RewardedAdvShow(idReward,Reward);
    }

    private void Reward()
    {
        PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins",managerShop.coinsPlayer += coinsReward);
        managerShop.UpdateCoinsText();
    }
}
