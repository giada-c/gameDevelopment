using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPopupHandler : MonoBehaviour
{

    public void Open()
    {
        gameObject.SetActive(true); //object on top of the window
        PauseControl.Instance.PauseGame(true);
    }

    public void Close()
    {
        gameObject.SetActive(false); //turn it off
        PauseControl.Instance.PauseGame(false);
    }
}
