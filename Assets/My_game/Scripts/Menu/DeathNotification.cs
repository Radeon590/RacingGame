using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNotification : MonoBehaviour
{
    [SerializeField] private MenuController menuController;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject deathNotifScreen;

    public void Replay()
    {
        deathNotifScreen.SetActive(false);
        gameController.RestartGame();
        menuController.StartGame();
    }
}
