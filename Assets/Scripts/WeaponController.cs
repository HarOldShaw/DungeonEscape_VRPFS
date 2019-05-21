using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    public float attack;
    private float damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Attack");
        if (other.collider.gameObject.tag == "Player")
        {
            damage = attack;
            Debug.Log("Health -%f"+attack);
        }
    }
}
