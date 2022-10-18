using TMPro;
using UnityEngine;

public class LoaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject contentPane;
     [SerializeField] private GameObject textPrefab;
     [SerializeField] private TextMeshProUGUI userText;
     private int count;
    //TODO
    async void Start()
    {

        var levelName = Saver.instance.clip.name;
        int score = ScoreManager.GetHighScore();//FirebaseManager.GetScore(levelName).Result;
       userText.text = "You: "+score;
      // FirebaseManager.SaveMyScoreOnThisLevelToMyProfile(levelName,score);
      await Saver.instance.SaveScore();
       //await FirebaseManager.SaveMyScoreOnThisLevelToLevels(levelName,score);

       string[] scores = await FirebaseManager.GetLeaderboard(levelName);
       foreach (var sc in scores)
       {
           SpawnHighscoreText(sc);
       }
    }
    public void SpawnHighscoreText(string jup)
    {
        count++;
        TextMeshProUGUI spawned = Instantiate(textPrefab, contentPane.transform).GetComponent<TextMeshProUGUI>();
       spawned.text =  count+ "."+jup;
        
    }
}
