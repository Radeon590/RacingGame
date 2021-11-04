using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarTypes
{
    standart
}
public class CarSelector : MonoBehaviour
{
    [SerializeField] private MenuController menuController;
    [SerializeField] private Sprite carSprite;
    /*[Space]
    [SerializeField] private CarTypes carType;*/

    public void ChooseCar()
    {
        menuController.CarSprite = carSprite;
    }
}
