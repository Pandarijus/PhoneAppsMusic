using System;

using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int _index = -1;
    public AudioSource audioSource;
    public static MusicManager instance;
    private int audioClipLengh;
    private LoadMusic loadMusic;
    private bool randomize,randomNext;

    public delegate void Changer(float volume,float pitch);
    
    public event Changer onValueChanged;
    void Awake()
    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        instance = this;
        audioSource = GetComponent<AudioSource>();
        //audioSource.playOnAwake = false;
    }

    private void Start()
    {
//Debug.Log(Application.persistentDataPath);
        loadMusic = LoadMusic.instance;
    }

    private bool stop;
    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (!stop)
            {
                TriggerNextSong();
            }
        }
    }

    public void TriggerNextSong()
    {
        stop = true;
        if (randomNext)
        {
            PlayRandomSong();
        }
        else
        {
            PlayNextSong();
        }
       
    }
    
    
    public void PlayNextSong()//call this
    {
     
        // CancelInvoke("PlayNextSong");
        _index++;
        if (_index >= audioClipLengh)
        {
            _index = 0;
        }

        PlayAndLoadSong(_index);
    }

    public void PlayAndLoadSong(int index)
    {
        _index = index;
        //DisplayManager.musicIndex.Display(index);
        DisplayManager.InformOfMusicChange(index);
        loadMusic.LoadClip(index);
        //  StartCoroutine(loadMusic.LoadClipCoroutine(index));
    }
    public void SetAudioClipLength(int lenght)
    {
//        Debug.Log("SET");
        audioClipLengh = lenght;
        
        if (!audioSource.isPlaying)
        {
            onValueChanged?.Invoke( audioSource.volume, audioSource.pitch);
            PlayRandomSong();
        }
        else
        {
            Debug.Log("Already playing");
        }
    }
    public void PlayRandomSong()
    {
//        Debug.Log("PlayRandom");
        _index = Random.Range(0, audioClipLengh);
        PlayAndLoadSong(_index);
     
    }

    private void DisplaySongName()
    {
        text.text = audioSource.clip.name;
    }
   

    public void PlayPrevSong()//call this
    {
        _index--;
        if (_index < 0)
        {
            _index = audioClipLengh- 1;
        }
        PlayAndLoadSong(_index);
    }

    
    public void RandomizeSound()
    {
        var minPitch = 0.9f;
       var maxPitch = 3f;
       audioSource.pitch = Random.Range(minPitch,maxPitch);
    //   audioSource.volume = Random.Range(0.9f, 1f);
        onValueChanged?.Invoke( audioSource.volume, audioSource.pitch);
        //mabe add some music filters here
    }

    public void SetVolume(float vol)
    {
        audioSource.volume = vol;
    }

    public bool SwapAndGetIsMuted()
    {
        audioSource.mute = !audioSource.mute;
        return audioSource.mute; 
    }
    public bool SwapAndGetIsPaused()
    {
        if (audioSource.isPlaying)
        {
            stop = true;
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
            stop = false;
        }
        return !audioSource.isPlaying; 
    }
    public void PlayClip(AudioClip clip)
    {
        if (randomize)
        {
            RandomizeSound();
        }
 
        audioSource.clip = clip;
        audioSource.Play();
        DisplaySongName();
        stop = false;
        //   CancelInvoke("PlayNextSong");
        //    Invoke("PlayNextSong", clip.length);// recursive loop
    }

    public bool ToggleRandomize()
    {
        randomize =  !randomize;
        return randomize;
    }
    public bool ToggleRandomNext()
    {
        randomNext =  !randomNext;
        return randomNext;
    }

    public void ResetSliders()
    {
        audioSource.volume = 1;
        SetPitch(1);
    }
    public void SetPitch(float pitch)
    {
        audioSource.pitch = pitch;
        onValueChanged.Invoke(audioSource.volume,pitch);
    }

    
   
}
