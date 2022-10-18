using UnityEngine;
using UnityEngine.Networking;

public class NamePanelActivator : MonoBehaviour
{
     //  public static NamePanelActivator instance;
     [SerializeField] private GameObject namePanel;
     private static bool isLoadingForName;
     
    public async void Awake()
    {
        // CancellationToken cancel = new CancellationToken();
        // Debug.Log(UnityWebRequest.Get("www.google.com").result);
        // Debug.Log(UnityWebRequest.Get("www.google.com").timeout);
        // Debug.Log(UnityWebRequest.Get("www.google.com").isDone);
        // Debug.Log(UnityWebRequest.Get("www.google.com"));
        if (Bigbrain.HasNoInternet())
        {
            Loader.LoadLevel(Level.Menu);
        }
        else
        {
            var savedNameSnap = await FirebaseManager.GetMyNameTaksFromMyProfile(); //myName
            var savedName = savedNameSnap.Value;
            if (savedName == null) //Has not played before
            {
                namePanel.SetActive(true);
            }
            else
            {
                Debug.Log("Hello "+savedName);
                Loader.LoadLevel(Level.Menu);
                //Debug.Log("Hello "+savedName);
            }
        }
        
        
        

    }
}
