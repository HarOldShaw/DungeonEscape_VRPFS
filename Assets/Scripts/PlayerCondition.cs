using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int shield = 50;
    [SerializeField] int gold;
    [SerializeField] UIDisplay ui;
    
    [SerializeField] GameController controller;
    bool invincibleFlag = false;

    void Start(){
        controller = FindObjectOfType<GameController>();
        ui = FindObjectOfType<UIDisplay>();
        ui.ShowGold(gold);
        ui.ShowHealthBar(health);
        ui.ShowShieldBar(shield);
    }

    public void AddShield( int value){
        shield += value;
        if(shield >= 100){
            shield = 100;
        }
        ui.ShowShieldBar(shield);
    }


    public void AddHealth( int value){
        health += value;
        if(health >= 100){
            health = 100;
        }
        ui.ShowHealthBar(health);
    }

    private void OnCollisionEnter(Collision other)
    {   
        if(!invincibleFlag){
            int damage = 0;
            print(other.collider.gameObject.name);
            if (other.collider.gameObject.tag == "Boss Weapon")
            {
                damage = 20;
            }else if(other.collider.gameObject.tag == "Enemy Weapon")
            {   
                Debug.Log("collided with enemy weapon");
                damage = 10;
            }else if (other.collider.gameObject.tag == "Trap"){
                damage = 30;
            }
            GetHit(damage);
            StartCoroutine(SetInvincible());
        }
    }

    IEnumerator SetInvincible(){
        invincibleFlag = true;
        yield return new WaitForSeconds(2f);
        invincibleFlag = false;
    }

    public void GetHit(int damage){
        if(damage != 0){
            if(shield >= damage){
                //reduce shield only
                shield -= damage;
            }else if (shield != 0){
                shield = 0;
                int tmp = damage - shield;
                health -= tmp;    
                //check if die and show remain health
                CheckHealth();
            }else{
                health -= damage;
                CheckHealth();    
            }
            ui.ShowShieldBar(shield);
            ui.ShowHealthBar(health);
        }
       

    }

    void CheckHealth(){
        if (health <= 0){
            // player die, show die canvas and restart game in 3 seconds
            Debug.Log("You die!");
            ui.ShowDeathPanel();
            controller.ReloadGame();
        }
    }

}
