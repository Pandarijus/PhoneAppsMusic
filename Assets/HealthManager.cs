using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
       public static HealthManager instance;
       [SerializeField] private GameObject[] healths;
       private int health;
       private float imunityTime = 2f;
       private float timer;
       private bool isInvincable = true;
    void Awake()
    {
       instance = this;
       health = healths.Length;
    }

    private void Update()
    {
        if (timer < imunityTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            
        }
    }

    public void TakeDamage()
    {
        if (timer < imunityTime ||isInvincable) // imune time
        {
            return;
        }
        
        if (health == 1)
        {
            // FirebaseManager.SaveMyScoreOnThisLevelToLevels()
            AudioVisualizer.instance.Pause();
            healths[0].SetActive(false);
            AudioVisualizer.instance.MuffleSoundForSecounds(imunityTime);
        }
        else
        {
            health--;
            timer = 0;
            healths[health].SetActive(false);
        }
       
        
    }
    public void RestoreHealth()
    {
        health++;
        if (health > 3)
        {
            health = 3;
        }
        healths[health-1].SetActive(true);
        
    }
}
