using System;
using UnityEngine;

public class LevelButtonSpawner : MonoBehaviour
{
    [SerializeField] GameObject levelButtonPrefab;

    private void Start()
    {
        SpawnLevelButtons();
    }


    public void SpawnLevelButtons()
{
    string[] levelPaths = PathBoy.instance.GetMP3Paths();

    foreach (var levelPath in levelPaths)
    {
      LevelLoader levelButton =  Instantiate(levelButtonPrefab,transform).GetComponent<LevelLoader>();
      levelButton.Setup(levelPath);
    }
    
    }
}
