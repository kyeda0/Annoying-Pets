using System;
using TMPro;
using UnityEngine;
using YG;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class GameObjects : MonoBehaviour
{
    public Player player;
    public float damage;
    public TextMeshPro textBuff;
    public Color colorForText;
    public event Action OnAddScore;
    public event Action<TextMeshPro> onTextBuff;

    public event Action OnTutorialObjectActivated;

    public virtual void ActivityObject()
    {
        onTextBuff?.Invoke(textBuff);
    }

    protected virtual void CheckLanguageForText(string textForRussian, string textForEngland)
    {
        if(YG2.lang == "en")
        {
            textBuff.text = textForEngland;
        }
        else if(YG2.lang == "ru")
        {
            textBuff.text = textForRussian;
        }
    }
    protected virtual void  OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            ActivityObject();
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Garbage"))
        {
            Destroy(gameObject);  
            OnAddScore?.Invoke();  
        }
    }
}
