using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject menuMode;
    [SerializeField] private GameObject game_Ui;
    [Space]
    [SerializeField] private GameObject menu_UI;
    [SerializeField] private GameObject carSelectionMenu;

    public Sprite CarSprite
    {
        set
        {
            gameController.CarSprite = value;
        }
    }

    public void StartGame()
    {
        gameController.gameObject.SetActive(true);
        game_Ui.SetActive(true);
        gameController.StartGame();

        menuMode.SetActive(false);
        menu_UI.SetActive(false);
    }

    #region CarsSelection
    public void ChooseCars_openMenu()
    {
        carSelectionMenu.SetActive(true);
    }

    public void ChooseCars_closeMenu()
    {
        carSelectionMenu.SetActive(false);
    }
    #endregion
}
