using System;
using System.Threading.Tasks;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public static Saver instance;
    public ColorPalette colorPalette;
    [NonSerialized]
    public AudioClip clip;
    public float musicVolume=1f,popVolume=1f;
  //  [SerializeField] private AudioClip[] baseMusicsAudioClips;
   public ColorPalette[] colorPalettes;
   private int currentTemmplateIndex;

    void Awake()
    {
        CheckForOthers();
        currentTemmplateIndex =  PlayerPrefs.GetInt("ColorTemplate",0);
        musicVolume =  PlayerPrefs.GetInt("MusicVolume",1);
        popVolume =  PlayerPrefs.GetInt("PopVolume",1);
        SetColorPalette(currentTemmplateIndex);
        OnVolumeChange += SetVolume;
    }

    private void CheckForOthers()
    {
        if (instance !=null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public string GetAudioClipName()
    {
        return (clip!=null)?clip.name:"";
    }

    // public bool IsCustomMusic() TEMP
    // {
    //     foreach (var c in baseMusicsAudioClips)
    //     {
    //         if (c == clip)
    //         {
    //             return false;
    //         }
    //     }
    //     return true;
    // }
    public static event Void OnColorChange;
    public delegate void Void(ColorPalette cp);

    public void SetColorPalette(int index)
    {
        currentTemmplateIndex = index;
        colorPalette = colorPalettes[index];
        PlayerPrefs.SetInt("ColorTemplate",currentTemmplateIndex);

        GradientManager.UpdateGradient(colorPalette.darkestColor, colorPalette.mainColor);//timingColor);
       OnColorChange?.Invoke(colorPalette);
    }
    // public static event Floid OnMusicVolumeChange;
    // public static event Floid OnPopVolumeChange;
    // public delegate void Floid(float f);
    
    public delegate void Noid(float f,bool isMusicVolume);
    public static event Noid OnVolumeChange;

    public void SetVolume(float newVolume,bool isMusicVolume)
    {
        if (isMusicVolume)
        {
            musicVolume = newVolume;
            PlayerPrefs.SetFloat("MusicVolume",musicVolume);

        }
        else
        {
//            Debug.Log($"pop:{newVolume}");
            popVolume = newVolume;
            PlayerPrefs.SetFloat("PopVolume",popVolume);
        }
    }

    public static void InvokeVolumeChange(float newVolume,bool isMusicVolume)
    {
        OnVolumeChange?.Invoke(newVolume,isMusicVolume);
    }
    // public void SetMusicVolume(float newVolume)
    // {
    //     musicVolume = newVolume;
    //     OnMusicVolumeChange?.Invoke(newVolume);
    // }
    // public void SetPopVolume(float newVolume)
    // {
    //     popVolume = newVolume;
    //     OnMusicVolumeChange?.Invoke(newVolume);
    // }

    public Task SaveScore()
    {
        ScoreManager.instance.SaveHigscore();
        var levelName = clip.name;
        int score = ScoreManager.GetHighScore();//FirebaseManager.GetScore(levelName).Result;
        try
        {
            FirebaseManager.SaveMyScoreOnThisLevelToMyProfile(levelName,score);
            return FirebaseManager.SaveMyScoreOnThisLevelToLevels(levelName,score);
        }
        catch (Exception e)
        {
            return null;
        }
      
    }
}
