using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        Input.compass.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.gyro.gravity.ToString());
        //Debug.Log(MicInput.MicLoudnessinDecibels);
        //Debug.Log(Input.acceleration.sqrMagnitude.ToString("#####.##"));
        //Debug.Log(Input.compass.magneticHeading.ToString());
    }
}
