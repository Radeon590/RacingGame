using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInputController : MonoBehaviour
{
    public bool Left = false;
    public bool Right = false;
    
    public int XInput
    {
        get
        {
            return xInput;
        }
    }

    private int xInput = 0;

    private void Update()
    {
        if (!Left && Right)
        {
            xInput = 1;
        }
        else if(Left && !Right)
        {
            xInput = -1;
        }
        else
        {
            xInput = 0;
        }
    }
}
