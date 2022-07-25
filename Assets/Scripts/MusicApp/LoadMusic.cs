using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using TMPro;
using UnityEngine;

public class LoadMusic : MonoBehaviour
{
//    private AudioClip[] clips;
    List<string> audionames;
    List<string> audioPaths;
    public static LoadMusic instance;
    private MusicManager musicManager;
    private PathManager pathManager;
    private Dictionary<string, AudioClip> musicCashe;

        //   [SerializeField] private TextMeshProUGUI text;

    void Awake()
    {
        instance = this;
        musicCashe = new Dictionary<string, AudioClip>();
    }

    private void Start()
    {
        musicManager = MusicManager.instance;
        pathManager = PathManager.instance;
        RefreshList();
    }

    public void RefreshList()
    {
        audionames = new List<string>();
        audioPaths = new List<string>();

        LoadMP3NamesFromPath(Application.persistentDataPath);
        if (pathManager.saveFileWasCreated)
        {
            string[] paths = pathManager.GetPaths();
//            File.WriteAllLines(Application.persistentDataPath, audionames.ToArray());
            foreach (var path in paths)
            {
                if (path.Equals("")) continue;

//                Debug.Log("Additional Path:    " + path);
                LoadMP3NamesFromPath(path);
            }
        }
        else
        {
          //  pathManager.CreateSaveFileWithEveryPath();
        }

        musicManager.SetAudioClipLength(audionames.Count);
        ItemSpawner.instance?.SpawnGuys();
        // MusicInfo.SpawnGuys( audionames.ToArray());

        //  ButtonManager.InvokeStopSpinningAnimation();
    }

    public void LoadMP3NamesFromPath(string path)
    {
        if (Directory.Exists(path))
        {
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (FileInfo item in info.GetFiles("*.mp3"))
            {
                audionames.Add(item.Name);
                audioPaths.Add(path);
//                Debug.Log("[" + (audioPaths.Count - 1) + "]      ADD   " + item.Name + "       AT    " + path);
            }
        }
    }

//            Debug.Log(string.Format("[{0}] {1}",i, audionames[i]));
    public async Task LoadClip(int index)
    {
        var audioname = audionames[index];

        if (musicCashe.ContainsKey(audioname)) //Check if cashed
        {
            musicManager.PlayClip(musicCashe[audioname]);
        }
        else //load out of storage
        {
           var www = new WWW("file:///" + audioPaths[index] + "//" + audioname); //this shouldn't work
           while (!www.isDone)//wait for request to finish
           {
               await Task.Yield();
           }  
           
            var clip = www.GetAudioClip(false, false);
                clip.name = audionames[index].Replace(".mp3", "");
                musicCashe[audioname] = clip; //Add to cashe
                musicManager.PlayClip(clip);
        }
    }
    
    
    public void ReanameFiles()
    {
        for (int i = 0; i < audionames.Count; i++)
        {
           var path = audioPaths[i] + "//" + audionames[i];
        //   var newName =audionames[i].Replace(" ", "").Replace("yt1s.com-", "").Replace("yt1s.io-", "").Replace("Yt1s.com-", "");
           var oldName =audionames[i].Replace(" ", "").Replace("OfficialVideo", "").Replace("Yt1s.com-", "");
           var newName = Regex.Replace(oldName, @" ?\(.*?\)", string.Empty);

         var newPath = audioPaths[i] + "//" + newName;
         try
         {
             File.Move(path,newPath);
         }
         catch (Exception e)
         {
//             Debug.Log(oldName+" == "+newName);
         }
          
        }
        
        RefreshList();
      //  ButtonManager.InvokeStopSpinningAnimation();
    }


    public string[] GetSongList()
    {
        return audionames.ToArray();
    }
}