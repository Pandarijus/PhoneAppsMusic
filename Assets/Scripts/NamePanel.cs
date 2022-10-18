using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NamePanel : MonoBehaviour
{
     //  public static NamePanel instance;
     [SerializeField] Button button;
     [SerializeField] TMP_InputField inputField;
    
    void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        string processedName = inputField.text;//.Replace(" ",);

        if (processedName.Length > 0)
        {
            FirebaseManager.SaveMyNameOnMyProfile(processedName);
            MyReferences.instance?.leaderboardPanel.SetActive(true);
            gameObject.SetActive(false);
            Loader.LoadLevel(Level.Menu);
            //give feedback that its false
        }
       
    }
}
