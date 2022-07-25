using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Action _action;
    private MusicManager music;
    private References r;
    private Button button;
    private Image _image;
    private static Image playButtonImage;
    private bool isMuted, isPaused;
 //   public delegate void Void();
  //  public static event Void OnStopAnimate;


    private void Awake()
    {
        button = GetComponent<Button>();
        _image = GetComponent<Image>();
        if (_action == Action.PausePlay)
        {
            playButtonImage = _image;
        }
    }

    void Start()
    {
        music = MusicManager.instance;
        r = References._instance;
    }

    private void Update()
    {
        if (_action == Action.BlackScreenOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               gameObject.SetActive(false);
            }
        }
       
    }


    void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }
 
    private void OnClick()
    {
        if (_action == Action.PlayPrev)
        {
            music.PlayPrevSong();
            playButtonImage.sprite = r.canPause;
        }
        else if (_action == Action.PlayNext)
        {  music.TriggerNextSong();
            playButtonImage.sprite = r.canPause;
        }
      //  else if (_action == Action.MuteUnmute)
       // {
         //   _image.sprite = (music.SwapAndGetIsMuted()) ? r.isMuted : r.isNotMuted;
        //}
        else if (_action == Action.PausePlay)
        {
            _image.sprite = (music.SwapAndGetIsPaused()) ? r.canPlay : r.canPause;
        } else if (_action == Action.Refresh)
        {
          //  Debug.Log("Refresh");
          //  StartSpinning();
           LoadMusic.instance.RefreshList();
        }
        else if (_action == Action.Quit)
        {
           Application.Quit();
        }
        else if (_action == Action.AddPath)
        {
            PathManager.instance.SavePath();
        }
        else if (_action == Action.Randomize)
        {
           MusicManager.instance.RandomizeSound();
        }
        else if (_action == Action.ToggleLoop)
        {
            bool isOn =  !MusicManager.instance.audioSource.loop;
            MusicManager.instance.audioSource.loop = isOn;
            _image.color = isOn? r.on:r.off; 
        }
        else if (_action == Action.ToggleRandomize)
        {
            bool isOn = MusicManager.instance.ToggleRandomize();
            _image.color = isOn? r.on:r.off; 
        }
        else if (_action == Action.ResetSliders)
        {
          MusicManager.instance.ResetSliders();
        }
        else if (_action == Action.RenameMusic)
        {
           // Debug.Log("Rename");
        //    StartSpinning();
            LoadMusic.instance.ReanameFiles();
        }
         else if (_action == Action. SnapPitchUp)
                {
                    MusicManager.instance.SetPitch(1.3f);
                }
        else if (_action == Action.RandomNextSong)
        {
            bool isOn = MusicManager.instance.ToggleRandomNext();
            _image.color = isOn? r.on:r.off; 
        }
    }
   
/*
    private void StartSpinning()
    {
        GetComponent<Animator>().SetBool("spin", true);
        OnStopAnimate += StopSpin;
    }
    private void StopSpin()
    {
        GetComponent<Animator>().SetBool("spin", false);
        OnStopAnimate -= StopSpin;
    }

    public static void InvokeStopSpinningAnimation()
    {
        OnStopAnimate?.Invoke();
    }
    */

 enum Action
{
    PlayPrev,
    PlayNext,
    PausePlay,
    RandomNextSong,
    Refresh,
    Quit,
    AddPath,
    Randomize,
    ToggleRandomize,
    ToggleLoop,
    ResetSliders,
    RenameMusic,
    SnapPitchUp,
    BlackScreenOn
}
}

