using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ManagerShop : MonoBehaviour
{
    [SerializeField] private Sprite[] allSkin;
    [SerializeField] private SpriteRenderer[] mannequins;
    [SerializeField] private int spriteIndex = 0;
    [SerializeField] private int prevIndex;
    [SerializeField] private int nextIndex;
    [SerializeField] private int coinsPlayer;
    [SerializeField] private int priceForSkin;
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private TextMeshProUGUI textButtonBuy;
    [SerializeField] private TextMeshProUGUI textCoins;
    private bool[] isPurchased;
    private void Start()
    {
        allSkin = Resources.LoadAll<Sprite>("Skins");
        coinsPlayer = PlayerPrefs.GetInt("Coins");
        textCoins.text = "x" + coinsPlayer;
        PlayerPrefs.GetInt("SkinsIndex");
        isPurchased = new bool[allSkin.Length];
        for (int i = 0; i < isPurchased.Length; i++)
        {
           isPurchased[i] = PlayerPrefs.GetInt($"PurchaseSkin{i}", i == 0 ? 1 : 0 ) == 1;
        }
        StartCoroutine(AnimationShowSkin(0.3f, new Vector2(0f,0f),new Vector2(0f,1f)));
    }
    



    private void ShowSkins()
    {   
        prevIndex = (spriteIndex - 1 + allSkin.Length) % allSkin.Length;
        nextIndex = (spriteIndex + 1) % allSkin.Length;
        mannequins[0].sprite = allSkin[prevIndex];
        mannequins[1].sprite = allSkin[spriteIndex];
        mannequins[2].sprite = allSkin[nextIndex];
        priceForSkin = 5 * spriteIndex;
        if(isPurchased[spriteIndex] == false)
        {
            LanguageText("Цена: " + priceForSkin,"Price: " + priceForSkin,textPrice);
            LanguageText("Купить ","Buy",textButtonBuy);
        }
        if(spriteIndex == 0 || isPurchased[spriteIndex] == true)
        {
            LanguageText("Экипировать","Equip ",textButtonBuy);
            LanguageText("У вас есть ","You have ",textPrice);
        }
    }


    public void NextSkin()
    {
        spriteIndex = (spriteIndex + 1) % allSkin.Length;
        StartCoroutine(AnimationShowSkin(0.3f, new Vector2(0f,1f),new Vector2(0f,0f)));

    }

    public void PrevSkin()
    {   
        spriteIndex = (spriteIndex - 1 + allSkin.Length) % allSkin.Length;
        StartCoroutine(AnimationShowSkin(0.3f, new Vector2(0f,1f),new Vector2(0f,0f)));
    }

    
    public void BuySkin()
    {
        if(priceForSkin <= coinsPlayer && isPurchased[spriteIndex] == false)
        {
            coinsPlayer -= priceForSkin;
            PlayerPrefs.SetInt("Coins",coinsPlayer);
            PlayerPrefs.SetInt("SkinsIndex",spriteIndex);
            PlayerPrefs.SetInt($"PurchaseSkin{spriteIndex}", 1);
            isPurchased[spriteIndex] = true;
            PlayerPrefs.Save();
            LanguageText("Экипировать","Equip ",textButtonBuy);
            LanguageText("У вас есть ","You have ",textPrice);
            StartCoroutine(AnimationShowSkin(0.3f, new Vector2(0f,1f),new Vector2(0f,0f)));
        }
        else if (isPurchased[spriteIndex] == true)
        {
            PlayerPrefs.SetInt("SkinsIndex",spriteIndex);
            PlayerPrefs.Save();
            LanguageText("Экипировать","Equip ",textButtonBuy);
            LanguageText("У вас есть ","You have ",textPrice);
            StartCoroutine(AnimationShowSkin(0.3f, new Vector2(0f,1f),new Vector2(0f,0f)));        }
    }

    private IEnumerator AnimationShowSkin(float timeAnim,Vector2 startVector,Vector2 endVector)
    {
        float time = 0f;
        Color startColor = new Color(1f,1f,1f,1f);
        Color endColor = new Color (1f,1f,1f,0f);
        startVector = new Vector2(0f,0f);
        endVector = new Vector2(0f,1f);
        for (int i = 0; i < mannequins.Length; i++)
        {
            mannequins[i].color = startColor;
        }
        while(time <= timeAnim)
        {
            time += Time.deltaTime;
            float t = time/timeAnim;

                mannequins[0].color = Color.Lerp(startColor,endColor,t);
                mannequins[1].color = Color.Lerp(startColor,endColor,t);
                mannequins[2].color = Color.Lerp(startColor,endColor,t);
                mannequins[1].transform.position = Vector2.Lerp(startVector,endVector,t);
                textPrice.color = Color.Lerp(startColor,endColor,t);
                if(mannequins[0].color == endColor && mannequins[1].color == endColor && mannequins[2].color == endColor)
                {
                    ShowSkins();
                    mannequins[0].color = Color.Lerp(endColor,startColor,t);
                    mannequins[1].color = Color.Lerp(endColor,startColor,t);
                    mannequins[2].color = Color.Lerp(endColor,startColor,t);
                    textPrice.color = Color.Lerp(endColor,startColor,t);
                }
        
            yield return null;
        }
    }


    private void LanguageText(string textForRussian, string textForEngland, TextMeshProUGUI text)
    {
        if(YG2.lang == "ru" )
        {
            text.text = textForRussian;
        }
        else if(YG2.lang == "en" )
        {
            text.text = textForEngland;
        }
    }
}
