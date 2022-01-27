using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [HideInInspector] public MobileInputController MobileInputController;
    //
    float speedMultiplier = 1;
    float timeMultiplier = 0.05f;
    //float timer = 0;

    float maxSpeed_y = 2.5f;
    float accelerationMultiplier = 5f;

    float speed_y = 0.01f;
    float speed_x = 2f;

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
        //TODO:changes for different platforms
        //float movement_x = Input.GetAxis("Horizontal");
        //
        float movement_x = MobileInputController.XInput;

        if (speed_y <= maxSpeed_y && canMove)
            speed_y += /*Input.GetAxis("Vertical")*/ 0.5f * Time.deltaTime * accelerationMultiplier;

        if (speed_y > 0)
        {
            speed_y -= frictionForce * Time.deltaTime;
            speedMultiplier += Time.deltaTime * timeMultiplier;
        }
        else
        {
            //movement_x = 0;
            speed_y = 0;
        }

        Vector2 movementVector = new Vector2(movement_x * speed_x, speed_y);
        transform.Translate(movementVector * Time.deltaTime * speedMultiplier);
    }
}
