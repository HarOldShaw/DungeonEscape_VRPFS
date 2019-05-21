using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] GameObject body;
    [SerializeField] GameObject explosion;
    // Start is called before the first frame update

    public void GetHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(explosion,transform.position,Quaternion.identity);
            body.SetActive(false);            
        }
    }

   
}
