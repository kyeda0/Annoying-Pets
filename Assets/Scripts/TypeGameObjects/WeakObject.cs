using System;
using UnityEngine;

public class WeakObject : GameObjects
{

    public override void ActivityObject()
    {
        CheckLanguageForText("-0.5 здоровье","-0.5 health");
        textBuff.color = colorForText;
        base.ActivityObject();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
