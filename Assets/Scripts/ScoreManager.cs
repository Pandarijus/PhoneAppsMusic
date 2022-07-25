using System;
using UnityEngine;
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
    public void MusicBlockWasClicked(float lifeTime,Vector3 blockPos)
    {
     //   Debug.Log(blockPos);
        
        int score = GetCalculatedScore(lifeTime);
        if (score > 0)
        {
            int comboLevel = Combo.instance.GetComboLevel();
            score *= comboLevel;
            AddScore(score);
            Spawner.instance.SpawnAnimatedTextFieled(score,blockPos);
            Spawner.instance.SpawnExplosion(blockPos);
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
        return  PlayerPrefs.GetInt(GetHiscoreName(name),0);
    }
    public static int GetHighScore()
    {
//        Debug.Log( "Got Highsoce:"+PlayerPrefs.GetInt(GetHiscoreName(),0));
        return  PlayerPrefs.GetInt(GetHiscoreName(),0);
    }


    public static string GetHiscoreName()
    {
        return "HighScore"+ Saver.instance.GetAudioClipName();
    }
    public static string GetHiscoreName(string name)
    {
        return "HighScore"+ name;
    }

    private void UpdateHigscore()
    {
        int oldHighscore = GetHighScore();
        if (score > oldHighscore)
        {
          //  Debug.Log("Saved"+GetHiscoreName()+score);
            //trigger applause or fireworks/ reward for new highscore
            PlayerPrefs.SetInt(GetHiscoreName(),score);
        }
      
    }

    private void OnDisable()
    {
        UpdateHigscore();
    }
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
