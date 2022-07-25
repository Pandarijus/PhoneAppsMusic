using System.IO;
using TMPro;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private const char SEPERATOR = '|';
    public static PathManager instance;

    [SerializeField] private TMP_InputField text;
    private string textPath;
    public bool saveFileWasCreated;

    private void Awake()
    {
        textPath = Application.persistentDataPath + "//paths.txt";
        saveFileWasCreated = File.Exists(textPath);
        instance = this;
    }

    public string[] GetPaths() //var path = @"/storage/22C3-E524/FromOldPhone/musicc";
    {
        return File.ReadAllText(textPath).Split(SEPERATOR);
    }

    public void SavePath()
    {
        if (text.text.Equals("")) return;
        SavePath(text.text);
        ResetTextField();
    }

    public void SavePath(string path)
    {
        File.AppendAllText(textPath, path + SEPERATOR);
        Debug.Log(path);
        LoadMusic.instance.LoadMP3NamesFromPath(path);
        foreach (var directory in new DirectoryInfo(path).GetDirectories())
        {
            SavePath(directory.FullName);
        }
        LoadMusic.instance.RefreshList();
    }
    private void ResetTextField()
    {
        text.text = ""; //   /storage/22C3-E524/FromOldPhone/musicc
    }

    public void CreateSaveFileWithEveryPath()
    {
        SavePath("/storage");//Takes too long. Make Async
    }
}