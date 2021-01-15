using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private bool musicOn;
    private float musicVolume;

    public bool ambientOn { get; private set; } = true;
    public float ambientVolume { get; private set; } = 0.3f;

    public AudioSource audioStart;
    public AudioSource audioBackground;

    // Start is called before the first frame update
    void Start()
    {
        musicOn = false;
        musicVolume = 1.0f;

        //stop start music

        // start the music
        audioBackground.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (!musicOn)
            audioBackground.volume = 0.0f;
        else
            audioBackground.volume = musicVolume;
    }

    
    public void setMusicOn(bool value)
    {
        musicOn = value;
    }
    public void setVolumeMusic(float value)
    {
        musicVolume = value;
    }
    


    public void setAmbientOn(bool value)
    {
        ambientOn = value;
    }
    public void setVolumeAmbient(float value)
    {
        ambientVolume = value;
    }

    public float getVolumeAmbient()
    {
        return ambientVolume;
    }

    public void stopStartMusic()
    {
        audioStart.Stop();
    }


}
