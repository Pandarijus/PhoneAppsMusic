using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI indexText, songNameText;
    [SerializeField] private Button button;
    private int index;

    private void Awake()
    {
        button.onClick.AddListener(()=> MusicManager.instance.PlayAndLoadSong(index));
    }

    public void Setup(int i, string songName)
    {
        index = i;
        indexText.text = "[" + i + "]";
        songNameText.text = songName;
    }
}
