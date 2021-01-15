using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    public GameObject p1;
    public GameObject p3;
    bool persona3 = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Joystick2Button9))
        {
            if (persona3)
            {
                camera.transform.position = p3.transform.position;
            }
            else {
                camera.transform.position = p1.transform.position;
            }
            persona3 = !persona3;
        }
    }
}
