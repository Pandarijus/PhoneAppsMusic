using TMPro;
using UnityEngine;

public class LevelLoader2 : MyButton
{
    [SerializeField] AudioClip clip;
    // private TextMeshProUGUI text;
    void Awake()
    {
        //Debug.Log(clip);
        GetComponentInChildren<DisplayHighScore>().DisplayTheHighScore(PathBoy.GetPathMusicNameWithoutSpaces(clip.name));
       // text = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    // public void Setup(string path)
    // {
    //     this.path = path;
    //     name = PathBoy.GetPathMusicName(path);
    //     text.text = name;
    // }
    protected override void OnClick()
    {
        MyReferences.instance.loadingPanel_LEVELS.SetActive(true);
        Saver.instance.clip = clip;
        Loader.LoadPlay();
        
    }
}