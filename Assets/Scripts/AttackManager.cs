using Firebase.Database;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private static DatabaseReference db;
 //   private static BackgroundManager backgroundManager;
    private static string userId;

    private const string SCORE = "score", NAME = "name";

    private void Setup()
    {
        if (userId == null)
        {
   //         backgroundManager = FindObjectOfType<BackgroundManager>();
            userId = SystemInfo.deviceUniqueIdentifier;
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
            db = FirebaseDatabase.DefaultInstance.RootReference;
        }
    }

    void Start()
    {
        Setup();
        db.ChildAdded += OnChild;
    }

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

    private void OnDestroy()
    {
        db.ChildAdded -= OnChild;
    }
}