using UnityEngine;

public class StandartObject : GameObjects
{

    public override void ActivityObject()
    {
        CheckLanguageForText("-1 здоровье","-1 health");
        textBuff.color = colorForText;
        base.ActivityObject();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
