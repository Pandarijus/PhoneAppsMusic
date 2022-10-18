using System;
using Firebase;
using Firebase.Database;
using System.Collections;
using UnityEngine;

public class Brain : MonoBehaviour
{
    //Checked 7.04.2022
    private static DatabaseReference db;
    private static string userId, savedName;
    private static int savedScore;

    private const string SCORE = "score", NAME = "name";

    //[NonSerialized] public 
    private static Brain instance; 

    private void Awake()
    {
        instance = this;
    }

    public static void PreLoad()
    {
        Setup();
        instance.StartCoroutine(CheckAndSetSavedUser());
    }
    
    private static void Setup() //Checked 7.04.2022
    {
        userId = SystemInfo.deviceUniqueIdentifier;
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        db = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public static IEnumerator CheckAndSetSavedUser() //Checked 7.04.2022
    {
        var task = GetUser().GetValueAsync();
        yield return new WaitUntil(predicate: () => task.IsCompleted);

        var data = task.Result;
        if (data.Value != null)
        {
            savedName = data.Child(NAME).Value.ToString();
            string score = data.Child(SCORE).Value.ToString();
            savedScore = int.Parse(score);
            Debug.Log(savedName + ":" + score);

            Loader.LoadPlay();
        }
        else
        {
         //   Loader.LoadLevel(Level.Name);
            Debug.Log("USER PLAYS FOR THE FIRST TIME");
        }
    }

    public static void SaveScore(int score)
    {
        GetUser().Child(SCORE).SetValueAsync("" + score);
    }

    public static void SaveUsername(string name)
    {
        GetUser().Child(SCORE).SetValueAsync(0);
        GetUser().Child(NAME).SetValueAsync(name);
    }
    public static (string name, int score) GetSavedUser()
    {
        return (savedName, savedScore);
    }

    private static DatabaseReference GetUser()
    {
        return db.Child(userId);
    }
}

#if UNITY_EDITOR
#endif