using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMaxSpeedController : MonoBehaviour
{
    [SerializeField] private List<float> maxSpeed_y_values;
    [SerializeField] private List<float> maxSpeed_y_StartTime;
    //
    private CarController _carController;
    //
    private int timeIndex = 0;
    private float timer = 0;

    private void Start()
    {
        _carController = this.GetComponent<CarController>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxSpeed_y_StartTime[timeIndex])
        {
            _carController.maxSpeed_y = maxSpeed_y_values[timeIndex];
            timeIndex++;
        }
    }
}
