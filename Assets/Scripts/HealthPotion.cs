using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] int healthValue = 30;
    [SerializeField] AudioClip brokenSound;
    [SerializeField] GameObject brokenVFX;
    
    UIDisplay ui;
    // Start is called before the first frame update
    void Start()
    {   
        ui = FindObjectOfType<UIDisplay>();
        
    }

    // Update is called once per frame
    
    public void GenerateHealth(){
        //object deactivate
        FindObjectOfType<PlayerCondition>().AddHealth(healthValue);
        Instantiate(brokenVFX,transform.position+new Vector3(0,0.2f,0),Quaternion.identity);
        gameObject.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(brokenSound,1f);
        
        //OPTIONAL add VFX
    }    

}
