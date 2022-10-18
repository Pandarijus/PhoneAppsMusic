using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class ScoreManager : MyDisplay
{
    
    public static ScoreManager instance;
    private int score;
    void Awake()
    {
        //  base.Awake();
        instance = this;

    }
    public void MusicBlockWasClicked(float lifeTime,Vector3 blockPos,Color blockColor)
    {
     //   Debug.Log(blockPos);
        
        int score = GetCalculatedScore(lifeTime);
        if (score > 0)
        {
            int comboLevel = Combo.instance.GetComboLevel();
            if (comboLevel == 0)//sliding
            {
                score = Mathf.RoundToInt(score*0.1f);
            }
            else
            {
                score *= comboLevel;
            }
            
            AddScore(score);
            Spawner.instance.SpawnAnimatedTextFieled(score,blockPos,blockColor);
            Spawner.instance.SpawnExplosion(blockPos,blockColor);
            SoundManager.instance.PlayBlockCrunchySounds();
           
          
        }
        else
        {
            Combo.instance.StopCombo();
        }
    }
    public void AddScore( int addScore)
    {
        score += addScore;
        Display(score);
    }
    
    public int GetCalculatedScore(float lifeTime)
    {
        float timingValue = MusicBlock.GetTimingValue(lifeTime);
        int score = 0; 
        if (timingValue > 0)
        {
            score = Mathf.RoundToInt(100 * timingValue);
            score += Random.Range(-10, 10);
        }
        return score;
    }

    public static int GetHighScore(string name)
    {
    //    Debug.Log( "Got Highsoce:   "+name+"    "+PlayerPrefs.GetInt(GetHiscoreName(name),0));
        return  PlayerPrefs.GetInt(GetHighscoreName(name),0);
    }
    public static int GetHighScore()
    {
//        Debug.Log( "Got Highsoce:"+PlayerPrefs.GetInt(GetHiscoreName(),0));
        return  PlayerPrefs.GetInt(GetHighscoreName(),0);
    }


    private static string GetHighscoreName()
    {
        return "HighScore"+ Saver.instance.GetAudioClipName();
    }
    private static string GetHighscoreName(string name)
    {
        return "HighScore"+ name;
    }

    public void SaveHigscore()
    {
        int oldHighscore = GetHighScore();
//        Debug.Log($"name:[{GetHiscoreName()}] old higscore: {oldHighscore}  new score:{score}");
        if (score > oldHighscore)
        {
//            Debug.Log("Saved"+GetHiscoreName()+score);
            //trigger applause or fireworks/ reward for new highscore
            PlayerPrefs.SetInt(GetHighscoreName(),score);
        }
      
    }

    // private void OnDisable()
    // {
    //     SaveHigscore();
    // }
}
   
    
    

// private int GetCalculatedScore(Vector3 blockPosition)
// {
//   //  var distVector = MyReferences.instance.perfectLine.position - blockPosition;// Player.instance.transform.position;
//   var distFromCenter = Mathf.Abs(MyReferences.instance.perfectLine.position.y - blockPosition.y); //distVector.magnitude;
//     int score;
//     if (distFromCenter <= 1)
//     {
//         score = 100 ;
//     }
//     else
//     {
//         score = Mathf.RoundToInt(100/distFromCenter);
//     }
//     score += Random.Range(-10, 10);
//     return score;
// }
