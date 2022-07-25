using UnityEngine;

public class MyReferences : MonoBehaviour
{
    public static MyReferences instance;

    [Header("Play")]
   // public Transform perfectLine;
    public GameObject winPanel,namePanel,leaderboardPanel;
    [Header("Levles")]
    public GameObject loadingPanel_LEVELS;
    
    private void Awake()
    {
        instance = this;
    }
}
