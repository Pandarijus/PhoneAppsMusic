using UnityEngine;

public class DisplayHighScore : MyDisplay
{
    public void DisplayTheHighScore(string getPathMusicNameWithoutSpaces)
    {
        int highscore = ScoreManager.GetHighScore(getPathMusicNameWithoutSpaces);
        if (highscore > 0)
        {
            Display("Highscore: "+highscore);
        }
    }
}
