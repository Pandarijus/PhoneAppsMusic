using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private List<string> saveText;
    void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        saveText = new List<string>();
        SingeltonManager.saveManger = this;
    }
    public void AddToSave(string text)
    {
       saveText.Add(text);
    }
    public void Save()
    {
        foreach (string text in saveText)
        {
            File.AppendAllText(Application.persistentDataPath+@"/savedPath.txt", text + Environment.NewLine);
        }
        saveText.Clear();
    }
    // public void Save(string text)
    // {
    //     File.AppendAllText(Application.persistentDataPath, text + "/n");
    // }
    public void RemoveFromSave(string path)
    {
        saveText.Remove(path);
    }
}