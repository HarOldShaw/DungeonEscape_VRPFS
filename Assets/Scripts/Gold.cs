using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] int goldValue = 5;
    UIDisplay ui;
    // Start is called before the first frame update
    void Start(){
        ui = FindObjectOfType<UIDisplay>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player"){
            //gold disappear
            gameObject.SetActive(false);
            //OPTIONAL VFX

            //Player Get coins;
            ui.ShowGold(goldValue);
                        
        }
    }

}
