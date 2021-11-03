using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public HP_Controller hp_controller;
    public CarController carController;

    bool crashed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");

        if (!crashed) //&& collision.gameObject.tag == "alienBody")
        {
            //нужно очистить все вблизи от препятствий
            crashed = true;
            hp_controller.GetDamage();
        }
    }
}
