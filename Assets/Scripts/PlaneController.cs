using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField]
    private bool isPlayer = true; 

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

    // To emulate value of Input.GetAxis(), lerping between current and next
    private float currRoll = 0f; 
    private float rollVal = 0f;
    private float currPitch = 0f; 
    private float pitchVal = 0f;
    private float currYaw = 0f; 
    private float yawVal = 0f;
    private float thrustVal = 0f;
    private float accelVal = 0f; 
    // Rolling over time
    public float moveTime = 0.3f; 
    public float rollTime = 0f;
    public float pitchTime = 0f;
    public float yawTime = 0f;


    private bool isAccelerating = false; 
    



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    public bool CheckAcceleration()
    {
        return isAccelerating;
    }

    // Input sends direction plane needs to move in
    public void MovePlane(string dir)
    {
        // Roll left is positive, right is negative
        if (dir == "r_left")
        {
            rollVal = 1f;
            rollTime += Time.fixedDeltaTime;
            
        }
        else if (dir == "r_right")
        {
            rollVal = -1f; 
            rollTime += Time.fixedDeltaTime;
        }
        else { rollVal = 0f; rollTime = 0f; }
        // Pitch up is positive, down is negative
        if (dir == "up")
        {
            pitchVal = 1f;
            pitchTime += Time.fixedDeltaTime;
        }
        else if (dir == "down")
        {
            pitchVal = -1f;
            pitchTime += Time.fixedDeltaTime;
        }
        else { pitchVal = 0f; pitchTime = 0f; }
        // Yaw left is negative, right is positive
        if (dir == "b_left")
        {
            yawVal = -1f;
            yawTime += Time.fixedDeltaTime;
        }
        else if(dir == "b_right")
        {
            yawVal = 1f;
            yawTime += Time.fixedDeltaTime;
        }
        else { yawVal = 0f; yawTime = 0f; }
        if (dir == "thrust")
        {
            accelVal = 1f; 
        }
        else if(dir == "stop")
        {
            accelVal = -1f; 
        }
        else
        {
            accelVal = 0f; 
        }
    }

    void GetInput()
    {
        string dir = ""; 
        if (Input.GetKey(KeyCode.A))
        {
            dir = "r_left";

        }
        if (Input.GetKey(KeyCode.D))
        {
            dir = "r_right";
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir = "down";
        }
        if (Input.GetKey(KeyCode.W))
        {
            dir = "up"; 
        }
        if (Input.GetKey(KeyCode.Q))
        {
            dir = "b_left";
        }
        if (Input.GetKey(KeyCode.E))
        {
            dir = "b_right";
        }

        MovePlane(dir); 

       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        GetInput();
        //roll = Mathf.Lerp(currRoll, rollVal, rollTime / moveTime) * (Time.fixedDeltaTime*rotSpeed);
        //pitch = Mathf.Lerp(currPitch, pitchVal, pitchTime / moveTime)* (Time.fixedDeltaTime * rotSpeed);
        //yaw = Mathf.Lerp(currYaw, yawVal, yawTime / moveTime) * (Time.fixedDeltaTime * rotSpeed);

        if (isPlayer)
        {
            roll = Input.GetAxis("Roll") * (Time.fixedDeltaTime * rotSpeed);
            pitch = Input.GetAxis("Pitch") * (Time.fixedDeltaTime * rotSpeed);
            yaw = Input.GetAxis("Yaw") * (Time.fixedDeltaTime * rotSpeed);
            currSpeed = Input.GetAxis("Accelerate") * (Time.fixedDeltaTime*accel); 
        }
        else
        {
            roll = rollVal * (Time.fixedDeltaTime * rotSpeed);
            pitch = pitchVal * (Time.fixedDeltaTime * rotSpeed);
            yaw = yawVal * (Time.fixedDeltaTime * rotSpeed);
            currSpeed = accelVal * (Time.fixedDeltaTime * accel);
        }

        if (currSpeed > 0f)
        {
            isAccelerating = true; 
        }
        else
        {
            isAccelerating = false; 
        }
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
        
    }
}
