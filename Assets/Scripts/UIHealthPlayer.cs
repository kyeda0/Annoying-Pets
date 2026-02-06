using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthPlayer : MonoBehaviour
{
    private Image healthUiImage;

    private void Start()
    {
        healthUiImage =  GetComponent<Image>();
    }
    public void UpdateHealthUI(float healthPlayer , float maxHealthPlayer)
    {
        healthUiImage.fillAmount = healthPlayer / maxHealthPlayer;
    }
}

