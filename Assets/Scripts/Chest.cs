using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] AudioClip openSound; 
    [SerializeField] GameObject openVFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ChestOpen(){
        GetComponent<AudioSource>().PlayOneShot(openSound);
        Instantiate(openVFX,transform.position+ new Vector3(0,0.4f,0),Quaternion.identity);
    }
}
