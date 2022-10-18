using TMPro;
using UnityEngine;

public class LoadingPanelManager : MonoBehaviour
{
      public static LoadingPanelManager instance;
      [SerializeField] private TextMeshProUGUI levelNameText, highscoreText;
      [SerializeField] private GameObject loadingPanel;
    
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetTexts(string levelName, int highscore)
    {
        if (highscore > 0)
        {
            highscoreText.text = "highscore: " + highscore;
        }
        levelNameText.text = levelName;
        loadingPanel.SetActive(true);
    }

    public void Hide()
    {
        Invoke("H",0.05f);
    }

    private void H()
    {
        loadingPanel.SetActive(false);
    }
}
