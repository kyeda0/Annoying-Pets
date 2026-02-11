using UnityEngine;

public class LowSpeedObject : GameObjects
{
    [SerializeField] private float speedEvent;
    public override void ActivityObject()
    {
        SpeedLowEffect();
        textBuff.color = colorForText;
        base.ActivityObject();
    }

    private void SpeedLowEffect()
    {
        switch (player.currentlevelSpeed)
        {
            case Player.LevelSpeed.Normal:
                player.ChangeSpeed(Player.LevelSpeed.Slow);
                CheckLanguageForText("-2 скорости","-2 speed");
                break;
            case Player.LevelSpeed.Slow:
                CheckLanguageForText("-0 скорости","-0 speed");
                break;
            case Player.LevelSpeed.Fast:
                player.ChangeSpeed(Player.LevelSpeed.Normal);
                CheckLanguageForText("-2 скорости","-2 speed");
                break;
            case Player.LevelSpeed.UltraFast:
                player.ChangeSpeed(Player.LevelSpeed.Fast);
                CheckLanguageForText("-2 скорости","-2 speed");
                break;
        }
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
