using UnityEngine;

public class DisplayHighScore : MyDisplay
{
    public void DisplayTheHighScore(string getPathMusicNameWithoutSpaces)
    {
        int highscore = ScoreManager.GetHighScore(getPathMusicNameWithoutSpaces);
//        Debug.Log($"[{getPathMusicNameWithoutSpaces}]:{highscore}");
        if (highscore > 0)
        {
            Display("Highscore: "+highscore);
        }
    }
}
