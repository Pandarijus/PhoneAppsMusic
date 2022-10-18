using UnityEngine;
using UnityEngine.Serialization;

public class MyReferences : MonoBehaviour
{
    public static MyReferences instance;

    [Header("Play")]
    // public Transform perfectLine;
    public GameObject winPanel;
   public GameObject namePanel,leaderboardPanel,misClickPanel,pausePanel;
  // public GameObject loadingPanel;

    [FormerlySerializedAs("loadingPanel_LEVELS")]// [Header("Levles")]
    public GameObject loadingPanel;


    private void Awake()
    {
        instance = this;
    }
}
