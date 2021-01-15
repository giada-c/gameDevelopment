using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverManager : MonoBehaviour
{
    public MusicManager musicManager;
    public PauseControl playPauseManager;
    public HideShowComponent gameoverCanvas;
    public AudioSource gameoverSound;
    private bool firstCall = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        if (firstCall){
            firstCall = false;
            playPauseManager.PauseGame(true);
            musicManager.setMusicOn(false);
            gameoverCanvas.Show();
            gameoverSound.Play();
        }

    }
}
