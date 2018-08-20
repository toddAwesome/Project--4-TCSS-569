using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ArduinoPlane : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM4", 115200);

    // Use this for initialization
    void Start()
    {
        serial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (!serial.IsOpen)
        {
            serial.Open();
        }
        string[] accelerometer = serial.ReadLine().Split(',');
        string[] gyroscope = serial.ReadLine().Split(',');
        string[] mag = serial.ReadLine().Split(',');

        transform.Translate(float.Parse(accelerometer[0]) / 100, //x
                            float.Parse(accelerometer[1]) / 100, //y
                            float.Parse(accelerometer[2]) / 100); //z

        Quaternion rot = Quaternion.Euler(new Vector3(float.Parse(gyroscope[0]),
                                                      float.Parse(gyroscope[1]),
                                                      float.Parse(gyroscope[2])));

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2.0f);
    }
}
