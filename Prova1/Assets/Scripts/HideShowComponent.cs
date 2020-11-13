using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowComponent : MonoBehaviour
{
    public bool isVisible = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState()
    {
        isVisible ^= isVisible;
        gameObject.SetActive(isVisible); //object on top of the window
    }
}
