using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    public float ampX;
    public float ampY;
    public float ampZ;
    public float speed;
    Vector3 pos;

    float tempX;
    float tempY;
    float tempZ;

    void Start()
    {
        tempX = transform.position.x;
        tempY = transform.position.y;
        tempZ = transform.position.z;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos.x = tempX + ampX * Mathf.Sin(speed * Time.time);
        pos.y = tempY + ampY * Mathf.Sin(speed * Time.time);
        pos.z = tempZ + ampZ * Mathf.Sin(speed * Time.time);
        transform.position = pos;
    }
}
