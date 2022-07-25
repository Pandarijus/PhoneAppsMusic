using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private List<MusicInfo> spawnedSongsInfos;
    public static ItemSpawner instance;

    private void Awake()
    {
        instance = this;
        spawnedSongsInfos = new List<MusicInfo>();
    }

    [SerializeField] private GameObject songInfoPrefab;
     private void OnEnable()
     {
         SpawnGuys();
     }
     public void SpawnGuys()
    {
        string[] songNames = LoadMusic.instance.GetSongList();
        for (int i = 0; i < songNames.Length; i++)
        {
            string songName = songNames[i].Replace(".mp3","");
            if (i < spawnedSongsInfos.Count )
            {
                spawnedSongsInfos[i].Setup(i,songName);  
            }
            else
            {
                MusicInfo m = Instantiate(songInfoPrefab, transform).GetComponent<MusicInfo>();
                m.Setup(i,songName);
                spawnedSongsInfos.Add(m);
            }
          
        }
    }
}
