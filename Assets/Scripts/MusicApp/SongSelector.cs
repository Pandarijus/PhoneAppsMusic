using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongSelector : MonoBehaviour
{
    private TMP_InputField inputField;
    [SerializeField] private Button button;
    
    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        button.onClick.AddListener(() =>
        {
            var text = inputField.text;
            int songNumber;
            if (text.Length > 0)
            {
                if (int.TryParse(text,out songNumber))
                {
                    MusicManager.instance.PlayAndLoadSong(songNumber);
                    inputField.text = "";
                }
               
            }
        });
    }
}
