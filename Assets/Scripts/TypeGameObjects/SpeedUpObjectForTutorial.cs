using UnityEngine;

public class SpeedUpObjectForTutorial : GameObjects
{

    public override void ActivityObject()
    {
        textBuff.color = colorForText;
        CheckLanguageForText("+2 скорости","+2 speed");
        player.isMoving = true;
        base.ActivityObject();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
