using UnityEngine;

public class StandartObjectForTutorial : GameObjects
{
   public override void ActivityObject()
    {
        CheckLanguageForText("-1 здоровье","-1 health");
        textBuff.color = colorForText;
        player.isMoving = true;
        base.ActivityObject();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
