using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathBoy : MonoBehaviour
{
    //  private List<string> paths = new List<string>();
    public static PathBoy instance;

    //  private List<string> mp3PathList;
    private string[] mp3Paths;

    private Dictionary<string, AudioClip> musicCashe = new Dictionary<string, AudioClip>();
    //private static Dictionary<string, string> nameToPath;

    // private string currentselectedMusicPath;
    private string persistentDataPath;
  //  private bool hasLoadedBefore;
    private int currentMusicIndex;

    public static bool loadedAllPaths;
   // private const string DIC_SAVEFILE_NAME = "dicSave.txt";


    void Awake()
    {
        SingeltonCall();
    }

    private void SingeltonCall()
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
    }

    private string[] savedDicPaths;
    private async void Start()
    {
        persistentDataPath = Application.persistentDataPath;
        savedDicPaths = Bigbrain.GetTextFromFile("dicSave.txt");
         await Task.Run(Setup);
         loadedAllPaths = true;
         Debug.Log("LOADED PATHS SUCCESFULLY");

         //Invoke(nameof(Setup), 0.01f);
         // if (hasLoadedBefore == false)
         // {
         //     Loader.LoadLevel(Level.Menu);
         // }
    }

    private void Setup()
    {
        string path;
#if UNITY_EDITOR
        path = @"C:\Users\krott\Music";

#else
        path = "/storage";
#endif
        try
        {
            FindAllDirectoriesWithMp3s(path);
        }
        catch (Exception e)
        {
        }
      
        foreach (var savedDicPath in savedDicPaths) //error when no file created>
        {
            var d = myDirectories.Keys.Where(k => k.FullName.Equals(savedDicPath)).FirstOrDefault();
            myDirectories[d] = false;
        }
        //Bigbrain.SaveTextToFile("dicSave.txt","");
        ReloadMp3s();
    }

    public void ReloadMp3s()
    {
        List<string> mp3PathsList = new List<string>();
        foreach (var dic in myDirectories)
        {
            if (dic.Value == true) //if dic is activated
            {
                mp3PathsList.AddRange(dic.Key.GetFiles("*mp3").Select(e => e.FullName));
            }
        }

        // mp3Paths = Directory.EnumerateFiles(path, "*.mp3", SearchOption.AllDirectories).ToArray();
        mp3Paths = mp3PathsList.ToArray();
        Array.Sort(mp3Paths);
    }


    //


    public void FindAllDirectoriesWithMp3s(string path)
    {
        foreach (var directory in new DirectoryInfo(path).GetDirectories())
        {
            if (Directory.GetFiles(directory.FullName, "*.mp3").Length > 0)
            {   
//                Debug.Log($"Adding to directory{directory.Name}");
                myDirectories.Add(directory, true);
            }

            FindAllDirectoriesWithMp3s(directory.FullName);
        }
    }

    public async Task LoadAudioClip(string currentselectedMusicPath)
    {
        AudioClip audioClip;
        if (musicCashe.ContainsKey(currentselectedMusicPath)) //Check if cashed
        {
            audioClip = musicCashe[currentselectedMusicPath];
        }
        else //load out of storage
        {
            string oldName = Path.GetFileName(currentselectedMusicPath);
            string path;
            string fileName = oldName;
            if (oldName.Contains(" "))
            {
                string newName = RemoveSpaces(oldName);
                Debug.Log("HAD WHITESPACE: [" + oldName + "]" + " [" + newName + "]");
                string newPath = persistentDataPath + "/" + newName;
                if (!File.Exists(newPath))
                {
                    File.Copy(currentselectedMusicPath, newPath);
                }

                Debug.Log(newPath);
                path = newPath;
                //  fileName = oldName; newName
            }
            else
            {
                path = currentselectedMusicPath;
                //fileName = oldName;
            }


            var www = new WWW("file:///" + path);
            while (!www.isDone) //wait for request to finish
            {
                await Task.Yield();
            }

            audioClip = www.GetAudioClip(false, false);
            audioClip.name = fileName.Replace(".mp3", "");
            musicCashe[currentselectedMusicPath] = audioClip; //Add to cashe
        }

        Saver.instance.clip = audioClip;
    }


    public static string GetPathMusicName(string path)
    {
        return Path.GetFileName(path).Replace(".mp3", "");
    }
    // public static string GetPathMusicNameWithoutSpaces(string path)
    // {
    //     return RemoveSpaces(GetPathMusicName(path));
    // }

    public async void LoadLevel(string path)
    {
      
        //   MyReferences.instance.loadingPanel.SetActive(true);
        string songName = GetPathMusicName(path);
        LoadingPanelManager.instance.SetTexts(songName,ScoreManager.GetHighScore(songName));
        FirebaseManager.SaveMyMusicPathToMyProfile(GetPathMusicName(path), path);
        //Debug.Log($"Give Info to Loading Panel of name:{songName} = Highscore:{ScoreManager.GetHighScore(songName)}");
        
        // if ( MyReferences.instance.loadingPanel_LEVELS != null)
        // {
        //     MyReferences.instance.loadingPanel_LEVELS.SetActive(true);
        // }
        // else
        // {
        //     MyReferences.instance.loadingPanel.SetActive(true);
        // }
        
        await LoadAudioClip(path);
        Loader.LoadPlay();
        LoadingPanelManager.instance.Hide();
    }

    public void LoadRandomLevel()
    {
        if(mp3Paths.Length == 0) return;
        var r = Random.Range(0, mp3Paths.Length);
        LoadLevelByIndex(r);
        // LoadLevel(mp3Paths[r]);
    }

    private void LoadLevelByIndex(int index)
    {
        currentMusicIndex = index;
        Debug.Log($"{mp3Paths.Length} : {index}");
        LoadLevel(mp3Paths[index]);
    }

    public static string RemoveSpaces(string path)
    {
        return path.Replace(" ", "");
    }

    public string[] GetMP3Paths()
    {
        return mp3Paths; //mp3PathList.ToArray();
    }

    public void LoadPrevLevel()
    {
        LoadLevelByIndex(Bigbrain.MakeArraySave(currentMusicIndex - 1, mp3Paths.Length));
    }

    public void LoadNextLevel()
    {
        LoadLevelByIndex(Bigbrain.MakeArraySave(currentMusicIndex + 1, mp3Paths.Length));
    }

    public Dictionary<DirectoryInfo, bool> GetDirectories()
    {
        return myDirectories;
    }

    private Dictionary<DirectoryInfo, bool> myDirectories = new Dictionary<DirectoryInfo, bool>();

    public void ChangeAcitveDirecories(DirectoryInfo key, bool isActive)
    {
        myDirectories[key] = isActive;
    }
}