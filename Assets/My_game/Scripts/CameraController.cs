using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CarController carController;
    [Space]
    [SerializeField] private float differnece_y = 5;

    void Update()
    {
        if(carController.transform.position.y > transform.position.y - differnece_y)
        {
            transform.Translate(Vector2.up * carController.Speed_y * Time.deltaTime);
        }
    }
}
