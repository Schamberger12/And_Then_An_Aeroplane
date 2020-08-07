using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    // Movement Variables
    [SerializeField]
    private float maxSpeed = 200f;
    [SerializeField]
    private float minSpeed = 0f; 
    [SerializeField]
    private float rotSpeed = 100f;
    [SerializeField]
    private float currSpeed = 0f; 
    [SerializeField]
    private float accel = 2.0f; 

    private float roll = 0f;
    private float pitch = 0f;
    private float yaw = 0f;

    private Rigidbody rb; 

    private Quaternion plusRot = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        roll = Input.GetAxis("Roll") * (Time.fixedDeltaTime * rotSpeed);
        pitch = Input.GetAxis("Pitch") * (Time.fixedDeltaTime * rotSpeed);
        yaw = Input.GetAxis("Yaw") * (Time.fixedDeltaTime * rotSpeed);
        currSpeed = Input.GetAxis("Accelerate") * (Time.fixedDeltaTime * accel);
        if (currSpeed > maxSpeed)
        {
            currSpeed = maxSpeed;
        }
        if (currSpeed < minSpeed)
        {
            currSpeed = minSpeed; 
        }
        // Every frame, apply 
        plusRot.eulerAngles = new Vector3(pitch, yaw, roll);
        rb.rotation *= plusRot;
        Vector3 plusPos = Vector3.forward;
        plusPos = rb.rotation * plusPos;
        rb.AddForce(plusPos * currSpeed);
        //rb.velocity = plusPos * currSpeed; 
        //rb.velocity = plusPos * (Time.fixedDeltaTime * currSpeed); 
        
    }
}
