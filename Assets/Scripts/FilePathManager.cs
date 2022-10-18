using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class FilePathManager : MonoBehaviour
{
    [SerializeField] private Button reloadButton;

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;
    void Awake()
    {
        reloadButton.onClick.AddListener(PathBoy.instance.ReloadMp3s);
        SpawnPathOGs();
    }
    
    

    private void SpawnPathOGs()
    {
        foreach (var dir in PathBoy.instance.GetDirectories())
       {
           var g = Instantiate(prefab, parent);
          g.GetComponent<PathOG>().Setup(dir.Key,dir.Value);
       }
    }

    public void RefreshAndLoadButtons()
    {
        PathBoy.instance.ReloadMp3s();
        LoneDisposer.instance.DisposeAll(LoneDisposableType.Button);
        foreach (var path in PathBoy.instance.GetMP3Paths())
        {
            LoneDisposer.instance.GetDisposableObject(LoneDisposableType.Button).GetComponent<LevelLoader>().Setup(PathBoy.GetPathMusicName(path),path);
        }
    }
}
