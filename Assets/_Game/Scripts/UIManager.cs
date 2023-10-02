using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text cointText;

    private void Awake() {
        instance = this;
    }

  
    public void SetCoin(int coin) 
    {
        cointText.text = coin.ToString();
    }
}
