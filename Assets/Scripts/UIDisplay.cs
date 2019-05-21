using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Image healthBar;
    private int healthCapacity = 100;
    [SerializeField] Image shieldBar;
    private int shieldCapacity = 100;
    private int gold = 0;
    [SerializeField] Text goldText;

    [SerializeField] GameObject deathPanel;

    public void ShowHealthBar(int currentHealth){
        float percentage = (float) currentHealth/healthCapacity;
        // Debug.Log("health: "+currentHealth+ "percentage:"+percentage);
        healthBar.fillAmount = percentage;
    }

    public void ShowShieldBar(int currentShield){
        float percentage = (float) currentShield/healthCapacity;
        shieldBar.fillAmount = percentage;
    }

    public void ShowGold(int goldValue){
        gold += goldValue;
        goldText.text = "Gold: "+gold;
    }

    public void ShowDeathPanel(){
        deathPanel.SetActive(true);
    }

}
