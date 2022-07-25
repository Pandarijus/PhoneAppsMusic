using System;
using UnityEngine;
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
    [SerializeField]  float updateStep = 0.1f;
    [SerializeField]  float sizeMuliplier = 1f;
    [SerializeField] float minSize = 0;
    [SerializeField] float maxSize = 20;
    [SerializeField] float triggerVolume = 1;
    public float startDelay = 1.5f;
    public static float staticStartDelay;

    [SerializeField] AudioSource audioSourceForMusic;
    //[SerializeField] private float cooldown=0.3f;
    private float[] loudnessArray;
    [SerializeField]  private float[] loudnessSteps= new float[6];

    [FormerlySerializedAs("triggerVolumeSlider")] [SerializeField] private SliderGetter triggerVolumeSliderGetter;
    private void Awake()
    {
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
        // if (audioClip == null)
        // {
        //     SetMusic(audioSourceForTriggerVolume.clip);
        // }
        // else
        // {
            SetMusic(audioClip);
        //}
        audioSourceForTriggerVolume.Play();
        Invoke(nameof(StartMusic), startDelay);
        staticStartDelay = startDelay;
    }

    private void Start()
    {
        triggerVolumeSliderGetter.value = triggerVolume;
      
        loudnessArray= new float[Spawner.instance.collumnCount];
    }

    public void SetMusic(AudioClip clip)
    {
        audioSourceForTriggerVolume.clip = clip;
        audioSourceForMusic.clip = clip;
    }
    private void StartMusic()
    {
        audioSourceForMusic.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (paused) return;

        if (audioSourceForTriggerVolume.isPlaying == false)
        {
            if (audioSourceForMusic.isPlaying == false)
            {
                if (FirebaseManager.isCoustomMusic)
                {
                    MyReferences.instance.winPanel.SetActive(true);
                }
                else
                {
                    if (FirebaseManager.instance.HasNoName())
                    {
                        MyReferences.instance.namePanel.SetActive(true);
                    }
                    else
                    {
                        MyReferences.instance.leaderboardPanel.SetActive(true);
                    }
                    
                }

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
            CalculateCurrentLoudness(audioSourceForTriggerVolume.clip, audioSourceForTriggerVolume.timeSamples);
          //  ScaleClipLoudness();
            VisualizeLoudness();
        }
    }
    
    public void CalculateCurrentLoudness(AudioClip clip, int currentTimeIndex)
    {
     
        clip.GetData(clipSampleData, currentTimeIndex); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.

        for (int i = 0; i < clipSampleData.Length; i++)
        {
            for (int j = 0; j < loudnessArray.Length; j++)
            {
                if (i < loudnessSteps[j])
                {
                    loudnessArray[j] += Mathf.Abs(clipSampleData[i]);
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

    private float GetClipLoudnessAndWorkWithIt(int index)
    {
        float clipLoudness = loudnessArray[index];
        //loudnessArray[index]=0;//reset the array value
        
        clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
        clipLoudness *= sizeMuliplier;
        clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);

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
                if (GetClipLoudnessAndWorkWithIt(i) > triggerVolumeSliderGetter.value)//triggerVolume)
                {
                   // int collumn = Spawner.instance.GetRandomCollumn();//make this determend by the music
                    Spawner.instance.SpawnMusicBlock(i);//
                } 
            }
         
        }
      
    }

    private bool paused;
    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
        audioSourceForTriggerVolume.Pause();
        audioSourceForMusic.Pause();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        paused = false;
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