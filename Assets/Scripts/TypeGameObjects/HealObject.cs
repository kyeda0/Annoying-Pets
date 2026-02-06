using UnityEngine;

public class HealObject : GameObjects
{
    [SerializeField] private float healthEvent;
    public override void ActivityObject()
    {   
        Heal();
        textBuff.color = colorForText;
        base.ActivityObject();
    }

    private void Heal()
    {
        if(player.maxHealth > player.health)
        {
            player.health += healthEvent;
            if(player.health > player.maxHealth)
            {
                player.health = player.maxHealth;
            }
            CheckLanguageForText("+1 здоровье","+1 health");
        }
        else
        {
            CheckLanguageForText("+0 здоровье","+0 health");
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
