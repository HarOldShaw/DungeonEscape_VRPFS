using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;

public class RaycastShoot : MonoBehaviour
{
    
    [SerializeField] int damage = 10;
    [SerializeField] float fireRate = 0.25f;
    [SerializeField] float weaponRange = 50f;
    [SerializeField] float hitForce = 100f;
    [SerializeField] Transform gunEnd;
    [SerializeField] AudioClip shotSound;
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.05f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        // audiosource
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        // if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && Time.time > nextFire)
        {   
            Debug.Log("fire");
            nextFire = Time.time + fireRate;
            GetComponent<AudioSource>().PlayOneShot(shotSound,0.7f);
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = gunEnd.transform.position;
            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);
            if(Physics.Raycast(rayOrigin, gunEnd.forward, out hit, weaponRange))
            {//set the end of the line to the hit object
                laserLine.SetPosition(1, hit.point);
                Debug.Log("hit collider:" + hit.collider.gameObject.name);
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {   
                    Debug.Log("find enemy");
                    enemy.GetHit(damage);
                    hit.collider.gameObject.GetComponentInParent<DrawArc>().gotHit = true;
                }
                if(hit.collider.GetComponent<HealthPotion>()){
                    hit.collider.GetComponent<HealthPotion>().GenerateHealth();
                    GetComponent<PlayerCondition>();
                }
                if(hit.collider.GetComponent<ShieldPotion>()){
                    hit.collider.GetComponent<ShieldPotion>().GenerateShield();
                }
                
                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else{
                laserLine.SetPosition(1, gunEnd.forward * weaponRange);
            }
            
        }
    }


    private IEnumerator ShotEffect()

    {
        //gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }


}
