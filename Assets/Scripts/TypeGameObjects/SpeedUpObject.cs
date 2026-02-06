using UnityEngine;

public class SpeedUpObject : GameObjects
{
    [SerializeField] private float speedUpEvent;
    public override void ActivityObject()
    {
        SpeedUpEffect();
        textBuff.color = colorForText;
        base.ActivityObject();
    }
    private void SpeedUpEffect()
    {
        switch (player.currentlevelSpeed)
        {
            case Player.LevelSpeed.Normal:
                player.ChangeSpeed(Player.LevelSpeed.Fast);
                CheckLanguageForText("+2 скорости","+2 speed");
                break;
            case Player.LevelSpeed.Fast:
                CheckLanguageForText("+0 скорости","+0 speed");
                break;
            case Player.LevelSpeed.Slow:
                player.ChangeSpeed(Player.LevelSpeed.Normal);
                CheckLanguageForText("+2 скорости","+2 speed");
                break;
        }
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
