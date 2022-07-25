using System;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Database;
public class FirebaseManager : MonoBehaviour
{
       public static FirebaseManager instance;
       private static DatabaseReference db;
    //   private static BackgroundManager backgroundManager;
    private static string userId;

    private const string SCORE = "score", NAME = "name";

    public static bool isCoustomMusic;
    
    void Awake()
    {
        if (Saver.instance.IsCustomMusic())
        {
            isCoustomMusic = true;
            Debug.Log("IS CUSTOM SCRIPT");
            enabled = false;
        }
        else
        {
            instance = this;
            if (userId == null)
            {
                userId = SystemInfo.deviceUniqueIdentifier;
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                db = FirebaseDatabase.DefaultInstance.RootReference;
            }

        }
        
       
        // SaveName("Petra");
        // SaveScore(454);
    }
    
    private void OnEnable()
    {
        db.ChildAdded += OnChild;
    }
    private void OnDestroy()
    {
        db.ChildAdded -= OnChild;
    }

    //
    
    private void OnChild(object sender, ChildChangedEventArgs childChangedEventArgs)
    {
        DataSnapshot snap = childChangedEventArgs.Snapshot;
        //var 
        var snapsUserId = childChangedEventArgs.Snapshot.Key;
        var name = snap.Child(NAME).Value.ToString();
        int score = int.Parse(snap.Child(SCORE).Value.ToString());
        Debug.Log("[Player added]" + name + ":" + score);

        if (snapsUserId != userId) //only add other players 
        {
            // Debug.Log();
            //     backgroundManager.AddBackgroundPlayer(name, score, db.Child(snapsUserId).Child(SCORE));
        }
    }
    public static void SaveName(string name)
    {
        GetUser().Child(NAME).SetValueAsync(name);
    }
    public static void SaveScore(int score)
    {
        GetUser().Child(SCORE).SetValueAsync(score);
    }

    // private async int GetScore()
    // {
    //     var task = GetUser().GetValueAsync();
    //     await task;
    //     var data = task.Result;
    //     if (data.Value != null)
    //     {
    //         int score;
    //
    //         if (int.TryParse(data.Child(SCORE).Value.ToString(),out score))
    //         {
    //             return score;
    //         }
    //         return -1;
    //     }
    //     else
    //     {
    //         return -1;
    //     }
    // }
    // private async string GetName()
    // {
    //     var task = GetUser().GetValueAsync();
    //     await task;
    //     var data = task.Result;
    //     if (data.Value != null)
    //     {
    //         return data.Child(NAME).Value.ToString();
    //     }
    //     else
    //     {
    //         return null;
    //     }
    // }

    private static DatabaseReference GetUser()
    {
        return db.Child(userId);
    }

    public bool HasNoName()
    {
        return true;
        //GetName()== null  //TODO
    }
}
