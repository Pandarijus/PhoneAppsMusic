using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadFIle : MonoBehaviour
{
    private string path;
    public List<AudioClip> cliplist;
    public List<string> audionames;
    private string[] fileExtensions = { ".mp3", ".wav", ".ogg", ".aiff", ".flac", ".aac", ".m4a" };
    private AudioSource _audioSource;
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        path = Application.persistentDataPath;
        Debug.Log(path);
        if (Directory.Exists(path))
        {
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (var extension in fileExtensions)
            {
                foreach (FileInfo item in info.GetFiles("*"+extension))
                {
                    audionames.Add(item.Name);
                }
            }
        }
        StartCoroutine(LoadAudioFile());
    }
    private  IEnumerator LoadAudioFile()
    {
        for (int i = 0; i <audionames.Count; i++)
        {
            UnityWebRequest AudioFiles = UnityWebRequestMultimedia.GetAudioClip(path + string.Format("{0}", audionames[i]),AudioType.WAV);
            yield return AudioFiles.SendWebRequest();
            if(AudioFiles.isNetworkError)
            {
                Debug.Log(AudioFiles.error);
                Debug.Log(path + string.Format("{0}", audionames[i]));
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(AudioFiles);
                clip.name = audionames[i];
                cliplist.Add(clip);
                Debug.Log(path + string.Format("{0}", audionames[i]));
                _audioSource.clip = clip;
            }
        }
    }
}