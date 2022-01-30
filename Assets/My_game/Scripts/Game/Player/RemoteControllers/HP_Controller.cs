using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class HP_Controller : MonoBehaviour
{
    public GameObject PlayerCar;
    [SerializeField] private GameObject DeathNotification;
    [Space]
    [SerializeField] private GameObject[] hearts;
    [SerializeField]

    private int hp = -1;

    public void Initializate_HP()
    {
        StartCoroutine(HeartsInitialization());
    }

    public void GetDamage()
    {
        hearts[hp].GetComponent<Animator>().Play("HeartAlpha");
        hp--;

        if(hp < 0)
        {
            Death();
        }
    }

    private void Death()
    {
        PlayerCar.GetComponent<CarController>().canMove = false;
        PlayerCar.GetComponent<DamageController>().enabled = false;
        Destroy(PlayerCar);
        DeathNotification.SetActive(true);
    }

    IEnumerator HeartsInitialization()
    {
        for(int i = 0; i < 3; i++)
        {
            hp++;
            hearts[hp].GetComponent<Animator>().Play("HeartAlpha_Plus");
            yield return new WaitForSeconds(0.55f);
        }
    }
}
