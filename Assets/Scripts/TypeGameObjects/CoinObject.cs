using UnityEngine;

public class CoinObject : GameObjects
{
    [SerializeField] private int coinEvent;
    public override void ActivityObject()
    {
        player.coin = PlayerPrefs.GetInt("Coins");
        textBuff.color = colorForText;
        CheckLanguageForText("+1 монета","+1 coin");
        PlayerPrefs.SetInt("Coins",player.coin + coinEvent);
        player.ChangeCoins();
        base.ActivityObject();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
