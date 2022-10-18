using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
//using UnityEngine.UI;

public class SearchManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
  //   [SerializeField] private TextMeshProUGUI output;
         //[SerializeField] private Transform scrollParent;
  //   const string path = @"C:\Users\krott\AppData\LocalLow\PandarijusGames\MusicPop\savedPaths.txt";
     private string[] mp3s;
     private string[] names;
     private Dictionary<string, string> dic;
     private List<string> searchResults;

    void Awake()
    {
        mp3s = PathBoy.instance.GetMP3Paths();//File.ReadAllLines(path);
       names = mp3s.Select(s => PathBoy.GetPathMusicName(s)).ToArray();
       inputField.onValueChanged.AddListener(OnChange);
       SetupDictionary();
    }

    private void Start()
    {
        OnChange("");
    }

    private void SetupDictionary()
    {
        //Debug.Log("Starting setup");
        dic = new Dictionary<string, string>();
        for (int i = 0; i < mp3s.Length; i++)
        {
            if (!dic.ContainsKey(names[i]))
            {
                dic.Add(names[i],mp3s[i]);
            //    Debug.Log($"Key:[{names[i]}]  Value:{mp3s[i]}");
//                Debug.Log($"[{names[i]}]");
            }
          
        }

      //  Debug.Log("DIc count"+dic.Count);
    }

    private void OnChange(string newText)
    {
  //      Debug.Log("CHANGING:"+newText);
//  Debug.Log("hi");
        PerformSearch(newText);
        OrderSearchResults();
        DisplayLeftoverOptions();
    }
    private void OrderSearchResults()
    {
        searchResults.Sort();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         DisposeAll(DisposableType.Button);
    //     }
    // }

    private void DisplayLeftoverOptions()
    {
       // output.text = "";
       
      LoneDisposer.instance.DisposeAll(LoneDisposableType.Button);
     //  Debug.Log("//////////////////////////");
        foreach (var opt in searchResults)
        { 
//            Debug.Log( GetDisposableObject(DisposableType.Button));
  //          Debug.Log( GetDisposableObject(DisposableType.Button).GetComponent<LevelLoader>());
 //  Debug.Log($"[{opt}]");
  // Debug.Log(dic.Count);
  // Debug.Log(dic.ContainsKey(opt));
  // Debug.Log(dic[opt]);
  LoneDisposer.instance.GetDisposableObject(LoneDisposableType.Button).GetComponent<LevelLoader>().Setup(opt,dic[opt]);;
  //Debug.Log(l);
  // var g = GetDisposableObject(DisposableType.Button);
  // //Debug.Log(g);
  // var l = g.GetComponent<LevelLoader>();
  // //Debug.Log(l);
  // l.Setup(opt,dic[opt]);
  //output.text += opt + Environment.NewLine;

  //  Instantiate()
        }
    }
    private void PerformSearch(string input)
    {
//        Debug.Log($"[{input}]");
        var searchResultsTemp = new List<string>();
        
        
        foreach (var option in names)
        {
           // Debug.Log($"@@{option}@@]");
            if (option.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)//option.Contains(input)
            {
                searchResultsTemp.Add(option);
            }
        }
        searchResults = searchResultsTemp;
    }


}
