using TMPro;
using UnityEngine;

public class CoinsText : MonoBehaviour
{
    
    public void UpdateCoinsText(int coinsPlayer)
    {
        GetComponent<TextMeshProUGUI>().text = $"x {coinsPlayer}";
    }
}

