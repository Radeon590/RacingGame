using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CarController carController;
    [Space]
    [SerializeField] private float differnece_y = 10;

    [HideInInspector]
    public bool crashed = false;
    

    void Update()
    {
        if(carController.transform.position.y > transform.position.y - differnece_y)
        {
            if (!crashed)
                transform.Translate(Vector2.up * carController.Speed_y * Time.deltaTime);
            else
                transform.Translate(Vector2.up * 20 * Time.deltaTime);
        }
        else if(crashed)
        {
            crashed = false;
        }
    }
}
