using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPotion : MonoBehaviour
{
    [SerializeField] int shieldValue = 30;
    [SerializeField] GameObject brokenVFX;
    [SerializeField] AudioClip brokenSound;
     UIDisplay ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIDisplay>();
    }

    // Update is called once per frame
    
    public void GenerateShield(){
        // player generate health
        FindObjectOfType<PlayerCondition>().AddShield(shieldValue);
        Instantiate(brokenVFX,transform.position+new Vector3(0,0.2f,0),Quaternion.identity);
        gameObject.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(brokenSound,1f);
        //OPTIONAL add VFX
        
    }    

}
