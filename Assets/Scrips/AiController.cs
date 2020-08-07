using System.Collections;
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


    // Keep AI within Outer Boundary
    // O

    [SerializeField]
    private SphereCollider outerBoundary;
    [SerializeField]
    private SphereCollider innerBoundary; 

    [SerializeField]
    private Transform playerLocation; 

    // Ontriggerenter if obstacle enters sphereCollider, raycast to see if need
    // to avoid, if need to avoid, do that!

    // OnTriggerExit if player exits sphereCollider
    // Move to follow player again


    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
