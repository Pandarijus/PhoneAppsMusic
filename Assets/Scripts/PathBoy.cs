using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PathBoy : MonoBehaviour
{
    //  private List<string> paths = new List<string>();
    public static PathBoy instance;
    private List<string> mp3PathList = new List<string>();
    private string[] mp3Paths;
    private Dictionary<string, AudioClip> musicCashe = new Dictionary<string, AudioClip>();
   // private string currentselectedMusicPath;
    private string saveFilePath,persistentDataPath;
    private bool hasLoadedBefore;
    

    void Awake()
    {
        CheckForOthers();
    }

    private void CheckForOthers()
    {
        if (FindObjectsOfType<PathBoy>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        persistentDataPath = Application.persistentDataPath;
        saveFilePath = persistentDataPath + "/savedPaths.txt";
      //  Debug.Log(saveFilePath);
       // bool saveFileWasCreated = File.Exists(saveFilePath);
        // if (saveFileWasCreated && false)
        // {
            // Debug.Log("SaveFileWasAlreadyCreated");
            // LoadMP3PathsFromFile();
        // }
        // else
        // {
            // SetupAndSaveMP3Paths();
        // }
        Invoke(nameof(Setup),0.01f);
        if (hasLoadedBefore == false)
        {
            Loader.LoadLevel(Level.Menu);
        }
    }
    
    private void Setup()
    {
        SetupMP3Paths();
       // SetupAndSaveMP3Paths();
#if UNITY_EDITOR
        Debug.Log(GetMP3Paths().Length);
#endif
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Loader.LoadPrevLevel();
        }
    }


    // public void SetupAndSaveMP3Paths()
    // {
    //     SetupMP3Paths();
    //    // SaveMP3Paths();
    // }

    private static void DebugStringArray(string[] strings)
    {
        //Debug.Log("Debuging");
        foreach (var str in strings)
        {
            Debug.Log("[" + str + "]");
        }
    }

    private void SetupMP3Paths()//async
    {
#if UNITY_EDITOR
        FindMP3Directories(@"C:\Users\krott\Music");
       
        //FindMP3Directories(@"C:\");
#else
         FindMP3Directories("/storage");
#endif
        //mp3PathList.Sort();
        mp3Paths =  mp3PathList.ToArray();
        Array.Sort(mp3Paths);
    }

//     private void SaveMP3Paths()
//     {
//         foreach (var mp3Path in mp3PathList)
//         {
//             File.WriteAllText(saveFilePath, mp3Path + Environment.NewLine);
//         }
//         
// #if UNITY_EDITOR
//         Debug.Log("Saved File");
// #endif
//         
//      
//     }

    public void FindMP3Directories(string path)
    {
        foreach (var directory in new DirectoryInfo(path).GetDirectories())
        {
            FileInfo[] mp3Files;
            try
            {
                mp3Files = directory.GetFiles("*.mp3");
            }
            catch (Exception e)
            {
                continue; //Skip Serching Dictonary if Access is Denied
            }

            foreach (var mp3File in mp3Files)
            {
                mp3PathList.Add(mp3File.FullName); //add mp3 path to list
            }

            FindMP3Directories(directory.FullName);
        }
    }

    // private void LoadMP3PathsFromFile()
    // {
    //     var lines = File.ReadAllLines(saveFilePath);
    //     foreach (var line in lines)
    //     {
    //         Debug.Log("[" + line + "]"); //still have to remove /m
    //         var l = line.Remove(line.Length - 2, 2);
    //         Debug.Log("[" + l + "]"); //still have to remove /m
    //     }
    // }


    // // // // // // // // //
    // public void SaveAllDirectories(string path)
    // {
    //    SaveAllDirectories("/storage");
    // }
    // private void SaveDirecotries(string path)
    // {
    //     foreach (var directory in new DirectoryInfo(path).GetDirectories())
    //     {
    //         paths.Add(directory.FullName);
    //         SaveDirecotries(directory.FullName);
    //     }
    // }
    // private void SaveFilesToPath()
    // {
    //     foreach (var path in paths)
    //     {
    //         File.AppendAllText(saveFilePath, path + Environment.NewLine);
    //     }
    // }
    // // // // // // // //

  

    public async Task LoadAudioClip(string currentselectedMusicPath)
    { 
        AudioClip audioClip;
        if (musicCashe.ContainsKey(currentselectedMusicPath)) //Check if cashed
        {
            audioClip = musicCashe[currentselectedMusicPath];
        }
        else //load out of storage
        {

            string oldName =  Path.GetFileName(currentselectedMusicPath);
            string path;
            string fileName;
            if (oldName.Contains(" "))
            {
                string newName =  RemoveSpaces(oldName);
                Debug.Log("HAD WHITESPACE: ["+oldName+"]" +" ["+newName+"]");
                string newPath = persistentDataPath+"/"+newName;
                if (!File.Exists(newPath))
                {
                    File.Copy(currentselectedMusicPath,newPath);
                }
                Debug.Log(newPath);
                path = newPath;
                fileName = newName;
            }
            else
            {
                path = currentselectedMusicPath;
                fileName = oldName;
            }


            var www = new WWW("file:///" + path);
            while (!www.isDone) //wait for request to finish
            {
                await Task.Yield();
            }
            
            audioClip = www.GetAudioClip(false, false);
            audioClip.name = fileName.Replace(".mp3","");
            musicCashe[currentselectedMusicPath] = audioClip; //Add to cashe

            Saver.instance.clip = audioClip;

            // var www = new WWW("file:///" + currentselectedMusicPath); //this shouldn't work
            // while (!www.isDone) //wait for request to finish
            // {
            //     await Task.Yield();
            // }
            //
            // audioClip = www.GetAudioClip(false, false);
            // audioClip.name = GetPathMusicName(currentselectedMusicPath); // audionames[index].Replace(".mp3", ""));
            // musicCashe[currentselectedMusicPath] = audioClip; //Add to cashe
        }

    }

    // public void SetAudioClip(AudioClip clip)
    // {
    //     audioClip = clip;
    // }

    // public AudioClip GetAudioClip()
    // {
    //     return audioClip;
    // }
    // public string GetAudioClipName()
    // {
    //     return (audioClip!=null)?audioClip.name:"";
    // }

    public static string GetPathMusicName(string path)
    {
        return Path.GetFileName(path).Replace(".mp3", "");
    }
    public static string GetPathMusicNameWithoutSpaces(string path)
    {
        return RemoveSpaces(GetPathMusicName(path));
    }

    public async void LoadLevel(string path)
    {
        MyReferences.instance.loadingPanel_LEVELS.SetActive(true);
        await LoadAudioClip(path);
        Loader.LoadPlay();
    }

    // public void RemoveAllSpacesInMP3Paths()
    // {
    //     for (int i = 0; i < mp3PathList.Count; i++)
    //     {
    //         var oldPath = mp3PathList[i];
    //         var newPath = oldPath.Replace(" ", "");
    //         File.Move(oldPath, newPath);
    //         mp3PathList[i] = newPath;
    //     }
    // }
    private static string RemoveSpaces(string path)
    {
       return path.Replace(" ", "");
    }

    public string[] GetMP3Paths()
    {
        return mp3Paths; //mp3PathList.ToArray();
    }
}