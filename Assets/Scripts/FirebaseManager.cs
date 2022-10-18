using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Database;

public static class FirebaseManager
{
    private static DatabaseReference db;
    private static string userId;
    private const string SCORES = "Scores", NAME = "Name";
    private static string myName; // could cause issues

    private static DatabaseReference GetDatabase()
    {
        if (db == null)
        {
            userId = SystemInfo.deviceUniqueIdentifier;
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
            db = FirebaseDatabase.DefaultInstance.RootReference;
        }
        return db;
    }

    public static void SaveMyNameOnMyProfile(string name)
    {
        myName = name;
        Debug.Log("My name is:" + myName);
        GetUser().Child(NAME).SetValueAsync(name);
    }

    public static Task<DataSnapshot> GetMyNameTaksFromMyProfile()
    {
        return GetUser().Child(NAME).GetValueAsync();
    }

    public static void SaveMyScoreOnThisLevelToMyProfile(string levelName, int score)
    {
        GetUser().Child(SCORES).Child(levelName).SetValueAsync(score);
    }

    public static void SaveMyMusicPathToMyProfile(string levelName, string path)
    {
        GetUser().Child("Paths").Child(levelName).SetValueAsync(path);
    }

    public static Task SaveMyScoreOnThisLevelToLevels(string levelName, int score)
    {
        return GetCurrentLevelLeaderboard(levelName).Child(myName).SetValueAsync(score);
    }

    public static async Task<string> GetMyScoreOnThisLevel(string levelName)
    {
        //Debug.Log("This is a Perma Lock");
        var snapshot = await GetUser().Child(SCORES).Child(levelName).GetValueAsync();
        return snapshot.Value.ToString();
    }
    private static DatabaseReference GetUser()
    {
        return GetDatabase().Child("UserProfiles").Child(userId);
    }
    private static DatabaseReference GetCurrentLevelLeaderboard(string levelName)
    {
        return GetDatabase().Child("Levels").Child(levelName);
    }

    public async static Task<string[]> GetLeaderboard(string levelName)
    {
        var leaderboard = await GetCurrentLevelLeaderboard(levelName).GetValueAsync();
//        Debug.Log("Show children");
        // foreach (var c in leaderboard.Children)
        // {
        //     Debug.Log(c); 
        // }
        return leaderboard.Children.Select(e => $"{e.Key}:{e.Value}").ToArray();
    }


    public static async void DecideWhatPanelToActivate()
    {

        if (Bigbrain.HasNoInternet())
        {
            MyReferences.instance.pausePanel.SetActive(true);// 
        }
        else
        {
            var savedNameSnap = await GetMyNameTaksFromMyProfile(); //myName
            var savedName = savedNameSnap.Value;
            if (savedName == null) //Has not saved name before
            {
                MyReferences.instance.namePanel.SetActive(true);
            }
            else
            {
                myName = savedName.ToString();
                Debug.Log("Load Leaderboardpanel");
                MyReferences.instance.leaderboardPanel.SetActive(true);
            }
        }
    }

    public static void DeleteUserInfo()
    {
        GetUser().SetValueAsync(null);
    }
}