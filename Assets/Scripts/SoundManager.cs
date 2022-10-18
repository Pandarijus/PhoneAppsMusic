using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
  //  [SerializeField] private SliderGetter slider,popVolume;
    public static SoundManager instance;
    private static float globalVolume = 1f;

    [SerializeField] private bool survive;
    [SerializeField] AudioClip[] audioClips;
    
    private Stack<AudioSource> stack = new Stack<AudioSource>();
    

    void Awake()
    {
        CheckForOthers();
    }

    private void CheckForOthers()
    {
        if (survive)
        {
            if (instance)
            {
                Destroy(instance.gameObject);
            }
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        } //kill but no dont destroy
     
    }
    

    public void PlayBlockCrunchySounds()
    {
        AudioClip clip = audioClips[0];
        PlayClip(clip,Saver.instance.popVolume*AudioVisualizer.averageLoudness*0.01f);
       // PlaySound(Sound.blockCrunsh);
        // int overlayAmount = Random.Range(0,audioClips.Length);
        // Debug.Log("overlayAmount:"+overlayAmount);
        // for (int i = 0; i < overlayAmount; i++)
        // {
        //   PlayClip(audioClips[Random.Range(0,audioClips.Length)]);
        // }
        
    }
    public void PlaySound(Sound index,float volume = 1)
    {
        Debug.Log("INDEX:"+index);
        AudioClip clip = audioClips[(int)index];
        PlayClip(clip,volume);
    }

    // if (stack.Peek() == null)//if stack is null reset it
    // {
    //     stack.Clear();
    //     PlayClip(clip,volume);
    //     return;
    // }

    private void PlayClip( AudioClip clip,float volume = 1)
    {
        GameObject g;
        AudioSource aud;
        
        if (stack.Count == 0)
        {
            g = new GameObject();
            aud = g.AddComponent(typeof(AudioSource)) as AudioSource;
        }
        else
        {
 
            aud = stack.Pop();
            aud.gameObject.SetActive(true);
            g = aud.gameObject;
        }
        
        g.name = "[Sound:" + clip.name + "]";
        float dRand = Random.Range(-0.10f,0.10f);
//        Debug.Log(transform);
        g.transform.parent = transform;
        g.transform.position =  Camera.main.transform.position;
        aud.volume = globalVolume*(volume*0.2f+dRand*0.1f);
        //aud.pitch += dRand;
        aud.clip = clip;
        aud.Play();
        StartCoroutine(DisableSoundAfterPlayed(aud));

    }

    public IEnumerator DisableSoundAfterPlayed(AudioSource aud)
    {
        yield return new WaitForSeconds(aud.clip.length);
        aud.gameObject.SetActive(false);
        stack.Push(aud);
    }

    public static void SetVolume(float newVolume)
    {
        Debug.Log("global Volume{");
        globalVolume = newVolume;
    }
}


public enum Sound
{
    click,blockCrunsh
}
