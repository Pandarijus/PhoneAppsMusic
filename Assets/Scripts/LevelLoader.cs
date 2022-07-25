using TMPro;

public class LevelLoader : MyButton
{
    private string path;
    private TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void Setup(string path)
    {
        this.path = path;
        name = PathBoy.GetPathMusicName(path);
        text.text = name;
        GetComponentInChildren<DisplayHighScore>().DisplayTheHighScore(PathBoy.GetPathMusicNameWithoutSpaces(path));
    }
    protected override void OnClick()
    {
        PathBoy.instance.LoadLevel(path);
    }
}
