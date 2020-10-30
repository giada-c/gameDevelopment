using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameController : MonoBehaviour
{
    // Start is called before the first frame update
    public int life = 5;
    private static int point = 0; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Debug.Log("Punti");
        Application.Quit();
    }

    public static void addPoint()
    {
        ++point;
    }
}
