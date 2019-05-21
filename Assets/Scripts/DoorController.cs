using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{   
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void OnCollisionEnter(Collision other){
        if (other.transform.tag == "Player"){
            animator.SetBool("Open", true);
        }
    }
}
