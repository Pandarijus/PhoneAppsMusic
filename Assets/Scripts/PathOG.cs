using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PathOG : MonoBehaviour
{
    [SerializeField] private Button button;
   // private Image buttonImage;
     [SerializeField] private TextMeshProUGUI text;
     private bool activated = true;
     private DirectoryInfo directoryInfo; 

     public void Setup(DirectoryInfo dir,bool isActivated)
     {
//         Debug.Log($"{dir.Name} : {isActivated}");
         activated = isActivated;
         button.image.color = (activated)?Saver.instance.colorPalette.mainColor:Saver.instance.colorPalette.darkestColor;
         directoryInfo = dir;
         text.text = dir.Name;
     }
    void Awake()
    {
        button.onClick.AddListener(OnClicked);
        //   instance = this;
    }

    private void OnClicked()
    {
        activated = !activated;
        button.image.color = (activated)?Saver.instance.colorPalette.mainColor:Saver.instance.colorPalette.darkestColor;
        PathBoy.instance.ChangeAcitveDirecories(directoryInfo,activated);
        if (activated)
        {
            Bigbrain.RemoveLineFromFile("dicSave.txt", directoryInfo.FullName);
        }
        else
        {
            Bigbrain.SaveTextToFile("dicSave.txt", directoryInfo.FullName, true);
        }
    }
}
