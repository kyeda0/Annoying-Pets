using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TextBuff : MonoBehaviour
{
    public void StartAnimationText(TextMeshPro textMesh,Player player)
    {
        var  textBuff = Instantiate(textMesh);
        textBuff.transform.position = player.transform.position;
        StartCoroutine(AnimationText(textBuff,new Vector3(player.transform.position.x,-3.5f),new Vector3(textBuff.transform.position.x,-2f,0f)));
    }

    private IEnumerator AnimationText (TextMeshPro startText,Vector3 startVector, Vector3 endVector)
    {
        float time = 0f;
        float timeDuration = 1f;
        Color startColor = startText.color;
        Color endColor = new Color( startText.color.a, startText.color.g, startText.color.b, 0f);
        while(time < timeDuration)
        {
            time += Time.deltaTime;
            float t = time/timeDuration;
            startText.transform.position = Vector3.Lerp(startVector,endVector,t);    
            startText.color = Color.Lerp(startColor,endColor,t);        
            yield return null;
        }
        Destroy(startText.gameObject);
    }
}
