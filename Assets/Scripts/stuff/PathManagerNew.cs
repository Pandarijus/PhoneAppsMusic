using System.IO;
using UnityEngine;

public class PathManagerNew : MonoBehaviour
{
    private DirectoryInfo currentDir;
    private bool showFiles;
    void Awake()
    {
        SingeltonManager.pathManager = this;
    }
    void Start()
    {
        LoadFirstDirectory();
    }
    private void LoadFirstDirectory()
    {
        #if UNITY_EDITOR
        LoadDirectory(@"C:\Users\krott\");
        #elif UNITY_ANDROID
        LoadDirectory("/storage");
        #endif
  
    }
    public void LoadPrevDirectory()
    {
        if (currentDir.Parent == null)return;
        LoadDirectory(currentDir.Parent.FullName);
    }
    public void LoadDirectory(string path)
    {
        currentDir = new DirectoryInfo(path);
        SingeltonManager.directoryButtonSpawner.StartSpawning();
        LoadDirectoryesFromDirectory(path);
        LoadFilesFromDirectory(path);
        SingeltonManager.directoryButtonSpawner.DisableUnusedButtons();
      
    }

    private void LoadFilesFromDirectory(string path)
    {
        var childrenDirs = currentDir.GetFiles();
        for (int i = 0; i < childrenDirs.Length; i++)
        {
            var childDir = childrenDirs[i];
            SingeltonManager.directoryButtonSpawner.SpawnDirectoryButton( true,childDir.Name,childDir.FullName);
        }
    }

    private void LoadDirectoryesFromDirectory(string path)
    {
        var childrenDirs = currentDir.GetDirectories();
        for (int i = 0; i < childrenDirs.Length; i++)
        {
            var childDir = childrenDirs[i];
            SingeltonManager.directoryButtonSpawner.SpawnDirectoryButton(false,childDir.Name,childDir.FullName);
        }
    }

    // public void ToggleShowFiles()
    // {
    //     showFiles = !showFiles;
    //     LoadDirectory(currentDir.FullName);
    // }
}