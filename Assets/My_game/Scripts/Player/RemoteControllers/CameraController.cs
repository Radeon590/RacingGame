using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CarController carController;
    [Space]
    [SerializeField] private float differnece_y = 10;
    [SerializeField] private float difference_x = 6;

    [HideInInspector]
    public bool crashed = false;
    

    void Update()
    {
        //y controlling
        if(carController.transform.position.y > transform.position.y - differnece_y)
        {
            if (!crashed)
            {
                transform.Translate(Vector2.up * carController.Speed_y * Time.deltaTime);
            }
            else
                transform.Translate(Vector2.up * 20 * Time.deltaTime);
        }
        else if(crashed)
        {
            crashed = false;
        }

        //x border
        if(carController.transform.position.x > transform.position.x + difference_x
            || carController.transform.position.x < transform.position.x - difference_x)
        {
            carController.gameObject.GetComponent<DamageController>().GetDamage();
        }
    }
}
