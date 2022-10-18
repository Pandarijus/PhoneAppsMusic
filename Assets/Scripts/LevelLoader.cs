using TMPro;
using UnityEngine;

public class LevelLoader : MyButton
{
    private string path;
    private TextMeshProUGUI text;
    private DisplayHighScore highScore;
    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        highScore = GetComponentInChildren<DisplayHighScore>();
    }
    
    public void Setup(string path)
    {
        this.path = path;
        name = PathBoy.GetPathMusicName(path);
        text.text = name;
        highScore.DisplayTheHighScore(PathBoy.GetPathMusicName(path));
    }
    public void Setup(string theName,string path)
    {
  //      Debug.Log(path);
//        Debug.Log(theName);
        this.path = path;
        name = theName;
        text.text = theName;
        highScore.DisplayTheHighScore(theName);
    }
    protected override void OnClick()
    {
        PathBoy.instance.LoadLevel(path);
    }
}
