using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    
    [SerializeField] float lookSpeed = 100f;
    
    [SerializeField] float jumpForce = 3f;

    [SerializeField] float upDownRange = 35f;
        
    private float rotY = 0;
    float nextJump;
    float jumpRate = 1f;

    private Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        Debug.Log(("connected:" + OVRInput.GetConnectedControllers()));
    }

    // Update is called once per frame
   void Update ()
    {
        OVRInput.Update();
        //float rotX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
        //rotY = rotY + Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
        float rotX = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x * lookSpeed * Time.deltaTime;
        rotY = rotY + OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y * lookSpeed * Time.deltaTime;
       
        rotY = Mathf.Clamp(rotY, -upDownRange, upDownRange);

        //rotate
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0f, rotX, 0f)));
        
        //walk
        //rb.MovePosition(transform.position + ((transform.forward * Input.GetAxis("Vertical") * speed ) + (transform.right * Input.GetAxis("Horizontal") *speed)) * Time.deltaTime);
        rb.MovePosition(transform.position + ((transform.forward * OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y * speed ) + (transform.right * OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x * speed)) * Time.deltaTime);
        
        // jump
        //if (Input.GetKeyDown(KeyCode.Space))
        if (OVRInput.Get(OVRInput.Button.One)&& Time.time > nextJump)
        {   
            nextJump = Time.time + jumpRate;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        //sprint
        //if(Input.GetKeyDown(KeyCode.LeftShift)){
        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)){
            Debug.Log("Run");
            speed = 2f;
        }else{
               speed = 1f;
        }
        // if(Input.GetKeyUp(KeyCode.LeftShift)){

        
    }
}