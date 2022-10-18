using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Bigbrain
{

    public static float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
        public static bool HasNoInternet()
    {
        // if (Application.internetReachability == NetworkReachability.NotReachable)
        // {
        //     Debug.Log("NO WIFI");
        // }
        //
        // else
        // {
        //     Debug.Log("HAS WIFI");
        // }
        return Application.internetReachability == NetworkReachability.NotReachable;
    }
     public static Vector3 RandomNormalizedVector3()
    {
        return new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), Random.Range(-1f,1f));
    }
    public static void DebugStringArray(string[] strings)
    {
        Debug.Log("Debuging");
        foreach (var str in strings)
        {
            Debug.Log("[" + str + "]");
        }
    }
    
    public static void SaveTextToFile(string fileName,string textToSave,bool append = false,bool saveToDesktop = false)
    {
        string path;
        if (saveToDesktop)
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.PathSeparator+ fileName;
        }
        else
        {
            path = GetPersistentPath(fileName);
        }

        Debug.Log(path);
        if (append)
        {
            foreach (var text in  GetTextFromFile(path))
            {
                if (text.Equals(textToSave))// no duplicate lines
                {
                    return;
                }
            }

            File.AppendAllText(path,textToSave+Environment.NewLine);
        }
        else
        {
            File.WriteAllText(path,textToSave);   
        }
    }

    public static void RemoveLineFromFile(string fileName,string lineTextYouWantToRemove)
    {
        var path = GetPersistentPath(fileName);
        File.WriteAllLines(path, File.ReadLines(path).Where(l => l.Equals(lineTextYouWantToRemove) == false ).ToList());
    }

    private static string GetPersistentPath(string fileName)
    {
        return    Application.persistentDataPath +Path.DirectorySeparatorChar+ fileName;
    }
    
    public static string[] GetTextFromFile(string fileName)
    {
        var path = GetPersistentPath(fileName);
//        Debug.Log(path);
        if (File.Exists(path))
        {
            return File.ReadAllLines(path);
        }
        else
        {
            return new string[0];
        }
      
    }
    
    
    

    public static string[] FindAllMP3Paths()
    {
        string path;
#if UNITY_EDITOR
        path = @"C:\Users\krott\Music";
#else 
        path ="/storage";
#endif
        return Directory.EnumerateFiles(path, "*.mp3", SearchOption.AllDirectories).ToArray();
    }

    public static int MakeArraySave(int value, int arrayLength)
    {
        if (value >= arrayLength)
        {
            value = 0;
        }
        else if (value < 0)
        {
            value = arrayLength-1;
        }

        return value;
    }


    // public static int DebugArray(string[] array)
    // {
    //     array
    // }
    //

    
    // public static string[] FindMP3Directories(string path)
    // {
    //     List<string> pathList = new List<string>();
    //     SearchForMoreDirectoriesInPath(path, out pathList);
    //        return pathList.ToArray();
    // }
    //
    // private static void SearchForMoreDirectoriesInPath(string path,out List<string> pathList)
    // {
    //     foreach (var directory in new DirectoryInfo(path).GetDirectories())
    //     {
    //         FileInfo[] mp3Files;
    //         try
    //         {
    //             mp3Files = directory.GetFiles("*.mp3");
    //         }
    //         catch (Exception e)
    //         {
    //             continue; //Skip Serching Dictonary if Access is Denied
    //         }
    //
    //         foreach (var mp3File in mp3Files)
    //         {
    //             pathList.Add(mp3File.FullName); //add mp3 path to list
    //         }
    //
    //         FindMP3Directories(directory.FullName);
    //     }
    //
    // }
    
    
    
    
    
    
    
    
    
    
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

    
    
    
    
    
    ////////////////////////////////////////////////////// MUSEUM OF GODS //////////////////////////////////////////////////////
    
    // public void FindMP3Directories(string path)
    // {
    //     foreach (var directory in new DirectoryInfo(path).GetDirectories())
    //     {
    //         FileInfo[] mp3Files;
    //         try
    //         {
    //             mp3Files = directory.GetFiles("*.mp3");
    //         }
    //         catch (Exception e)
    //         {
    //             continue; //Skip Serching Dictonary if Access is Denied
    //         }
    //
    //         foreach (var mp3File in mp3Files)
    //         {
    //             mp3PathList.Add(mp3File.FullName); //add mp3 path to list
    //         }
    //
    //         FindMP3Directories(directory.FullName);
    //     }
    // }

}
