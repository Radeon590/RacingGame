using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInputButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum ScreenSide
    {
        left,
        right
    }

    [SerializeField] private MobileInputController mobileInputContoller;
    [SerializeField] private ScreenSide screenSide = ScreenSide.left;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        SwitchMobileInput(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SwitchMobileInput(false);
    }

    private void SwitchMobileInput(bool switchType)
    {
        switch (screenSide)
        {
            case ScreenSide.left:
                mobileInputContoller.Left = switchType;
                break;
            case ScreenSide.right:
                mobileInputContoller.Right = switchType;
                break;
        }
    }
}
