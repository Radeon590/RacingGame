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
    [SerializeField] private CarController carController;
    [SerializeField] private ObstaclesTrigger obstaclesTrigger;
    [SerializeField] private Animator animtor;
    [Space] 
    [SerializeField] private GameObject explosionObjectPref;
    [Space]
    public HP_Controller hp_controller;
    public CameraController cameraController;

    bool crashed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!crashed) //&& collision.gameObject.tag == "alienBody")
        {
            GetDamage();
        }
    }

    public void GetDamage()
    {
        StartCoroutine(GettingDamage());
    }

    private IEnumerator GettingDamage()
    {
        //explosion visualizing
        Instantiate(explosionObjectPref).transform.position = transform.position;
        //set vars (on)
        crashed = true;
        this.GetComponent<Collider2D>().enabled = false;
        carController.Speed_y = 0;
        obstaclesTrigger.gameObject.SetActive(true);
        //start functions
        animtor.Play("CarBlinding");
        StartCoroutine(CarBlindingCooldown());
        hp_controller.GetDamage();
        //change position
        float oldPos_y = transform.position.y;
        transform.position = new Vector2(spawnPos_x, oldPos_y + spawnPos_difference_y);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        //set vars (off)
        obstaclesTrigger.gameObject.SetActive(false);
        crashed = false;
        //need to see explosion. so make a little delay
        yield return new WaitForSeconds(0.2f);
        cameraController.crashed = true;
    }

    IEnumerator CarBlindingCooldown()
    {
        bool playAnimation = true;

        while (playAnimation)
        {
            yield return new WaitForSeconds(blindingLength);
            playAnimation = false;
            this.GetComponent<Collider2D>().enabled = true;
        }
    }
}
