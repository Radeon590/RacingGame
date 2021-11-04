using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player_pref;
    [SerializeField] private Vector2 spawnPoint;
    [Space]
    [SerializeField] private HP_Controller hp_controller;
    [SerializeField] private CameraController cameraController;
    [Space]
    [SerializeField] private MapController mapController;
    [Space]
    public Sprite CarSprite;

    public void StartGame()
    {
        mapController.GenerateMap(RoadTypes.miami);
        hp_controller.Initializate_HP();
        //
        GameObject newPlayer = Instantiate(player_pref);
        newPlayer.transform.position = spawnPoint;
        //
        newPlayer.GetComponent<SpriteRenderer>().sprite = CarSprite;
        //
        DamageController new_damageController = newPlayer.GetComponent<DamageController>();
        new_damageController.cameraController = cameraController;
        new_damageController.hp_controller = hp_controller;
        //
        hp_controller.PlayerCar = newPlayer;
        cameraController.enabled = true;
        cameraController.carController = newPlayer.GetComponent<CarController>();
    }
}
