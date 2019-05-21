using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
public class Interactable : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
            
        
    }

    // Update is called once per frame

    void OnTriggerStay(Collider other) {        
        if(other.transform.tag == "Player"){
            Debug.Log("trigger stay");
            if(OVRInput.Get(OVRInput.Button.Two)){
                Debug.Log("player enter trigger");
                animator.SetBool("Open",true);
            }
        }    
    }

}
