using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player_pref;
    [SerializeField] private Vector2 spawnPoint;
    [Space]
    [SerializeField] private HP_Controller hp_controller;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private MobileInputController mobileInputController;
    [Space]
    [SerializeField] private MapController mapController;
    [Space]
    public Sprite[] CarSprites;
    //
    private Vector2 nextMapStartCoordinates;
    private Transform playerTransform;

    public void StartGame()
    {
        mapController.mapStartCoordinates = mapController.mapStartCoordinates_defaultValue;
        nextMapStartCoordinates = mapController.GenerateMap(RoadTypes.miami);
        hp_controller.Initializate_HP();
        //
        GameObject newPlayer = Instantiate(player_pref);
        playerTransform = newPlayer.transform;
        playerTransform.position = spawnPoint;
        //
        newPlayer.GetComponent<SpriteRenderer>().sprite = CarSprites[Random.Range(0, CarSprites.Length)];
        newPlayer.GetComponent<CarController>().MobileInputController = mobileInputController;
        //
        DamageController new_damageController = newPlayer.GetComponent<DamageController>();
        new_damageController.cameraController = cameraController;
        new_damageController.hp_controller = hp_controller;
        //
        hp_controller.PlayerCar = newPlayer;
        cameraController.enabled = true;
        cameraController.carController = newPlayer.GetComponent<CarController>();
    }

    public void RestartGame()
    {
        cameraController.transform.position = new Vector3(0, 0, -10);
        mapController.ClearMap();
        if(playerTransform)
            Destroy(playerTransform.gameObject);
    }

    private void Update()
    {
        if (playerTransform)
        {
            if (playerTransform.position.y > nextMapStartCoordinates.y - 10)
            {
                OnPlayerFinishedMapPart();
            }
        }
    }

    public void OnPlayerFinishedMapPart()
    {
        mapController.mapStartCoordinates = nextMapStartCoordinates;
        nextMapStartCoordinates = mapController.GenerateMap(RoadTypes.miami);
    }
}
