using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DamageController : MonoBehaviour
{
    [SerializeField] private float blindingLength = 2f;
    [SerializeField] private float spawnPos_x = 0;
    [SerializeField] private float spawnPos_difference_y = 10;
    [Space]
    [SerializeField] private ObstaclesTrigger obstaclesTrigger;
    [SerializeField] private Animator animtor;
    [Space]
    public HP_Controller hp_controller;
    public CarController carController;
    public CameraController cameraController;

    bool crashed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!crashed) //&& collision.gameObject.tag == "alienBody")
        {
            //нужно очистить все вблизи от препятствий
            crashed = true;
            hp_controller.GetDamage();
            GetDamage();
        }
    }

    private void GetDamage()
    {
        cameraController.crashed = true;
        carController.Speed_y = 0;
        obstaclesTrigger.gameObject.SetActive(true);
        animtor.Play("CarBlinding");
        //StartCoroutine(CarBlindingAnimation());
        float oldPos_y = transform.position.y;
        transform.position = new Vector2(spawnPos_x, oldPos_y + spawnPos_difference_y);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        obstaclesTrigger.gameObject.SetActive(false);
        crashed = false;
        /*if (obstaclesTrigger.clear)
        {
            obstaclesTrigger.gameObject.SetActive(false);
            crashed = false;
        }*/
    }

    /*IEnumerator CarBlindingAnimation()
    {
        bool playAnimation = true;

        while (playAnimation)
        {
            animtor.Play("CarBlinding");
            yield return new WaitForSeconds(blindingLength);
            playAnimation = false;
        }
    }*/
}
