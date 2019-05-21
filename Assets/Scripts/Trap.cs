using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{   
    [SerializeField] int damage = 30;
    bool isWorking = true;

    private void OnCollisionEnter(Collision other) {
         
        if (other.collider.gameObject.tag == "Enemy"){
            other.collider.gameObject.GetComponent<Enemy>().GetHit(damage);
            Debug.Log("trap Enemy");
        }
    }


}
