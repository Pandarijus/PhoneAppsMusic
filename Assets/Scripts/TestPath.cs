using System.IO;
using System.Linq;
using UnityEngine;

public class TestPath : MonoBehaviour
{
    //[SerializeField]
    void Awake()
    {
//        FindMP3Directories("/storage");
    string path = @"C:\Users\krott\Music";
  Directory.EnumerateFiles(path, "*.mp3", SearchOption.AllDirectories).ToList().ForEach(e=> Debug.Log(e));
    }
}
