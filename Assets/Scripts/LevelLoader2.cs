using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelLoader2 : MyButton
{
    [SerializeField] AudioClip clip;
    // private TextMeshProUGUI text;
    void Awake()
    {
        //Debug.Log(clip);
        GetComponentInChildren<DisplayHighScore>().DisplayTheHighScore(PathBoy.GetPathMusicName(clip.name));
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
        FirebaseManager.SaveMyMusicPathToMyProfile(clip.name, "Default Song");
        MyReferences.instance.loadingPanel.SetActive(true);
        Saver.instance.clip = clip;
        Loader.LoadPlay();
        
    }
}