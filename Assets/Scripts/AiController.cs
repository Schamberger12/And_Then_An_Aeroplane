﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{

    //Order of Precedence:
    //1) Avoid obstacles
    //2) Stay behind player
    //Stay behind player as often as possible
    //If an obstacle enters the AI's spherecollider,
    //perform a raycast from the middle, tip of each wing
    //top of fin, and bottom of wheels to check if will run into obstacle
    //If it wont, continue to stay behind player

    private bool isChecking = false;
    private bool isAvoiding = false;
    private bool isMovingToFollow = false;
    private bool isFollowing = false;

    [SerializeField]
    private float moveSpeed = 140f; 

    [SerializeField]
    private PlaneController planeScript; 

    [SerializeField]
    private Transform followPoint;

    [SerializeField]
    private GameObject[] rayArr = new GameObject[4];

    LayerMask mask; 

    // Ontriggerenter if obstacle enters sphereCollider, raycast to see if need
    // to avoid, if need to avoid, do that!

    // OnTriggerExit if player exits sphereCollider
    // Move to follow player again

    private void OnTriggerEnter(Collider other)
    {
        isChecking = true; 
    }
    private void OnTriggerStay(Collider other)
    {
        isChecking = true; 
    }
    private void OnTriggerExit(Collider other)
    {
        isChecking = false;
    }

    void Start()
    {
        mask = LayerMask.GetMask("Obstacles");
    }

    void LevelRotation()
    {
        if (this.transform.rotation.x > 0.5f)
        {
            planeScript.MovePlane("up");
        }
        if(this.transform.rotation.z > 0.5f)
        {
            planeScript.MovePlane("r_left"); 
        }

    }

    void MoveToFollow()
    {
        float step = moveSpeed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, followPoint.transform.position, step);
    }

    void MoveToAvoid()
    {
        planeScript.MovePlane("up"); 
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (planeScript.CheckAcceleration())
        {
            planeScript.MovePlane("thrust");
        }
        float distance = Vector3.Distance(this.transform.position, followPoint.position);
        if (distance > 15f && !isAvoiding)
        {
            MoveToFollow();
        }
        if (isChecking)
        {
            // Check if we're running into collider
            
            RaycastHit hit; 
            foreach(GameObject point in rayArr)
            {
                if (Physics.Raycast(point.transform.position, point.transform.forward, out hit, 8f, mask))
                {
                    isAvoiding = true; 
                    MoveToAvoid(); 
                }
            }
            
        }

    }
}
