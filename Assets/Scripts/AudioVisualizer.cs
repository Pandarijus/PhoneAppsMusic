using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioVisualizer : MonoBehaviour
{
 //   private float clipLoudness;
    private int sampleDataLength = 1024;
    private float currentUpdateTime = 1f;
    private float[] clipSampleData;   
    private AudioSource audioSourceForTriggerVolume;
    private  float updateStep = 0.1f;
//    [SerializeField] float minSize = 0;
  //  [SerializeField] float maxSize = 20;
   // [SerializeField] float triggerVolume = 1;
    private const int TRIGGER_VOLUME = 1,STEP_AMOUNT = 3;
    public const float MUSIC_START_DELAY = 1f;

    [SerializeField] AudioSource audioSourceForMusic;
    [SerializeField] Transform backgroundAudioBall;
    //[SerializeField] private float cooldown=0.3f;
    private float[] loudnessArray;
    [SerializeField]  private float[] loudnessSteps= new float[STEP_AMOUNT];

    public static AudioVisualizer instance;
   // [FormerlySerializedAs("triggerVolumeSlider")] [SerializeField] private SliderGetter triggerVolumeSliderGetter; //SLIDER
    private void Awake()
    {
        instance = this;
        isPaused = false;
        clipSampleData = new float[sampleDataLength];
        audioSourceForTriggerVolume = GetComponent<AudioSource>();
        AudioClip audioClip;
        if (PathBoy.instance == null)
        {
            audioClip = audioSourceForTriggerVolume.clip;
        }
        else
        {
            audioClip = Saver.instance.clip;
        }
        SetMusic(audioClip);
        audioSourceForTriggerVolume.Play();
        Invoke(nameof(StartMusic), MUSIC_START_DELAY);
    }

    private void OnEnable()
    {
        Saver.OnVolumeChange += UpdateVolume;
    }

    private void OnDestroy()
    {
        Saver.OnVolumeChange -= UpdateVolume;
    }

    private void UpdateVolume(float newVolume,bool isMusicVolume)
    {
        if (isMusicVolume)
        {
            audioSourceForMusic.volume = newVolume;
        }
        else
        {
            SoundManager.SetVolume(newVolume);
        }
    }

    private void Start()
    {
     //   triggerVolumeSliderGetter.value = triggerVolume;
      
        loudnessArray= new float[Spawner.COLLIMN_COUNT];
    }

    public void SetMusic(AudioClip clip)
    {
        audioSourceForTriggerVolume.clip = clip;
        
        audioSourceForMusic.clip = clip;
        audioSourceForMusic.volume = Saver.instance.musicVolume;
    }
    private void StartMusic()
    {
        audioSourceForMusic.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
            
        }

        if (isPaused) return;

        if (audioSourceForTriggerVolume.isPlaying == false)
        {
            if (audioSourceForMusic.isPlaying == false) // If music is over
            {
                OnSongEnd();
            }
           
            return;
        }
        for (int i = 0; i < loudnessArray.Length; i++)
        {
          loudnessArray[i] = 0;
        }
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
           // currentUpdateTime = 0f;
           currentLoudness = 0;
            CalculateCurrentLoudness(audioSourceForTriggerVolume.clip, audioSourceForTriggerVolume.timeSamples);
            
            //backgroundAudioBall.localScale =  currentLoudness*ballScaleFactor * Vector3.one;
            SetBallInFuture(currentLoudness);
            combinedLoudness += currentLoudness;
            loudnessCount++;
            avgLoudness = combinedLoudness / loudnessCount;
            //triggerVolume = avgLoudness*triggerVolumeToAverageLoudnessFactor;
          //  ScaleClipLoudness();
            VisualizeLoudness();
        }
    }

    private float timer,loudnessSum;
    private int loudnessDelayedCount;
    private Vector3 startPos, goalPos;

    public static float averageLoudness;
    

    [SerializeField] private float ballDelay = 0.15f;
    public async void SetBallInFuture(float loudness)
    {
        await Task.Delay(TimeSpan.FromSeconds(MUSIC_START_DELAY));
        try//when song is over and still some tasks are executing
        {
            //Debug.Log($"loudness:{loudness}    time:{Time.time}");
            if (timer == 0)
            {
              //  float averageLoudness;
                if (loudnessSum == 0)
                {
                    averageLoudness = loudness;
                }
                else
                {
                    averageLoudness = (loudnessSum / loudnessDelayedCount);
                }
                startPos = backgroundAudioBall.localScale;
                goalPos = averageLoudness * ballScaleFactor * Vector3.one;
                timer += Time.deltaTime;
                loudnessSum = 0;
                loudnessDelayedCount = 0;
                //  Debug.Log(timer);
                //Debug.Log(startPos);
                //Debug.Log(goalPos);
            }
            else
            {
                loudnessDelayedCount++;
                loudnessSum  += loudness;
                float percent = timer / ballDelay;
                backgroundAudioBall.localScale =  Vector3.Lerp(startPos,goalPos,percent);
                timer += Time.deltaTime;
                if (timer >= ballDelay)
                {
                    //  Debug.Log("timer >= staticStartDelay");
                    timer = 0;
                }
            }
        }
        catch (Exception e)
        {
        }
        
    }

    [SerializeField] private AudioMixerGroup muffleAudioMixerGroup;
    public async void MuffleSoundForSecounds(float muffleTime)
    {
        audioSourceForMusic.outputAudioMixerGroup = muffleAudioMixerGroup;
        await Task.Delay(TimeSpan.FromSeconds(muffleTime));
        if (audioSourceForMusic != null)
        {
            audioSourceForMusic.outputAudioMixerGroup = null;
        }
       
    }
    
    // public async void SetBallInFuture(float loudness)
    // {
    //     float timer = 0;
    //     Vector3 startPos, goalPos;
    //     while (timer < staticStartDelay)
    //     {
    //         timer += Time.deltaTime;
    //         float percent = timer / staticStartDelay;
    //         startPos = backgroundAudioBall.localScale;
    //         goalPos = loudness * ballScaleFactor * Vector3.one;
    //         backgroundAudioBall.localScale =  Vector3.Lerp(startPos,goalPos,percent);
    //         await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
    //     }
    //    
    //   
    // }

    private bool songEnded;


    private void OnSongEnd()
    {
        if (songEnded == false)
        {
           
            songEnded = true;
          //  MyReferences.instance.misClickPanel.SetActive(false); TEMP COULD CAUSE ISSUES WHEN PANEL ACTIVE AGAIN
            ScoreManager.instance.SaveHigscore();
            FirebaseManager.DecideWhatPanelToActivate();
        }
    }

    [SerializeField] private float ballScaleFactor = 1;//,triggerVolumeToAverageLoudnessFactor = 1;
    private float currentLoudness, avgLoudness,combinedLoudness;
    private int loudnessCount;
    
    //backgroundAudioBall
    
    public void CalculateCurrentLoudness(AudioClip clip, int currentTimeIndex)
    {
     
        clip.GetData(clipSampleData, currentTimeIndex); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.

        for (int i = 0; i < clipSampleData.Length; i++)
        {
            for (int j = 0; j < loudnessArray.Length; j++)
            {
                if (i < loudnessSteps[j])
                {
                    var oneLoundness = Mathf.Abs(clipSampleData[i]);
                    loudnessArray[j] += oneLoundness;
                    currentLoudness += oneLoundness;
                    //break;
                }
            }

        }
    }

    // private void ScaleClipLoudness()
    // {
    //     clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
    //     clipLoudness *= sizeMuliplier;
    //     clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);
    // }

   // [SerializeField] private TextMeshProUGUI textMeshProUGUI;
 //   public float debugLoudness;
    private float GetClipLoudnessAndWorkWithIt(int index)
    {
        float clipLoudness = loudnessArray[index];
        //loudnessArray[index]=0;//reset the array value
        
        //clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
       // clipLoudness *= sizeMuliplier;
        //clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);

       //clipLoudness =  triggerVolumeSliderGetter.value * (clipLoudness / avgLoudness);
       clipLoudness =  7.2f * (clipLoudness / avgLoudness);
       // clipLoudness /= (triggerVolumeSliderGetter.value * avgLoudness);// 
   //     textMeshProUGUI.text = clipLoudness.ToString();
       // debugLoudness = clipLoudness;
        // if (cubeVisualizer != null)
        // {
        //     var s = clipLoudness/sizeMuliplier;
        //     cubeVisualizer.transform.localScale = Vector3.one*s;
        // }

//        Debug.Log(clipLoudness);
        return clipLoudness;
    }

    private void VisualizeLoudness()
    {
        for (int i = 0; i < loudnessArray.Length; i++)
        {
            if (Spawner.instance.CanSpawnNextBlock())
            {
                if (GetClipLoudnessAndWorkWithIt(i) >TRIGGER_VOLUME)// triggerVolumeSliderGetter.value)
                {
                   // int collumn = Spawner.instance.GetRandomCollumn();//make this determend by the music
                    Spawner.instance.SpawnMusicBlock(i);//
                } 
            }
         
        }
      
    }

    public static bool isPaused;
    public void Pause()
    {
        Player.trailRenderer.enabled = false;
        MisclickManager.isPaused = true;
        MyReferences.instance.pausePanel.SetActive(true);
        ScreenShake.instance.StopShaking();
        Time.timeScale = 0;
        isPaused = true;
        audioSourceForTriggerVolume.Pause();
        audioSourceForMusic.Pause();
    }
    public void Resume()
    {
        Player.trailRenderer.enabled = true;
        MisclickManager.isPaused = false;
        MyReferences.instance.pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        audioSourceForTriggerVolume.Play();
        audioSourceForMusic.Play();
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    // private void VisualizeLoudness()
    // {
    //     if (clipLoudness > triggerVolume && Spawner.instance.CanSpawnNextBlock())
    //     {
    //         int collumn = Spawner.instance.GetRandomCollumn();//make this determend by the music
    //         Spawner.instance.SpawnMusicBlock(collumn);//
    //     }
    //     if(cubeVisualizer !=null)
    //         cubeVisualizer.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
    // }
}