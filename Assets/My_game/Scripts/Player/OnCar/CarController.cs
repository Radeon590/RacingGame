using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float speedMultiplier = 1;
    float timeMultiplier = 0.1f;
    //float timer = 0;

    float maxSpeed_y = 5;
    float accelerationMultiplier = 10f;

    float speed_y = 0;
    float speed_x = 0.1f;

    float frictionForce = 0.1f;

    public bool canMove = true;

    public float Speed_y
    {
        get
        {
            return speed_y * speedMultiplier;
        }
        set
        {
            speed_y = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float movement_x = Input.GetAxis("Horizontal");

        if (speed_y <= maxSpeed_y && canMove)
            speed_y += Input.GetAxis("Vertical") * Time.deltaTime * accelerationMultiplier;

        if (speed_y > 0)
        {
            speed_y -= frictionForce + Time.deltaTime;
            speedMultiplier += Time.deltaTime * timeMultiplier;
        }
        else
        {
            //movement_x = 0;
            speed_y = 0;
        }
            

        Vector2 movementVector = new Vector2(movement_x * speed_x * speed_y, speed_y);
        transform.Translate(movementVector * Time.deltaTime * speedMultiplier);
    }
}
