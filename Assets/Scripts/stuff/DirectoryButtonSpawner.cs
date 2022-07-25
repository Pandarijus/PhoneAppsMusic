using System.Collections.Generic;
using UnityEngine;

public class DirectoryButtonSpawner : MonoBehaviour
{
    private List<DirectoryButton> spawnedButtons;
    [SerializeField] private GameObject directoryButtonPrefab;
    private int buttonCount;
    void Awake()
    {
        SingeltonManager.directoryButtonSpawner = this;
        spawnedButtons = new List<DirectoryButton>();
    }
    public void SpawnDirectoryButton(bool isFile,string name,string path)
    {
        DirectoryButton button;
        if (buttonCount < spawnedButtons.Count)
        {
            button = spawnedButtons[buttonCount];
            button.gameObject.SetActive(true);
        }
        else
        {
            button =  Instantiate(directoryButtonPrefab, transform).GetComponent<DirectoryButton>();
            spawnedButtons.Add(button);
        }
        button.Setup(isFile,name,path);

        buttonCount++;
    }
    public void DisableUnusedButtons()
    {
        int spawnedButtonsCount = spawnedButtons.Count;
        if (spawnedButtonsCount > buttonCount)
        {
            int diff = spawnedButtonsCount - buttonCount;
            for (int i = 0; i < diff; i++)
            {
                spawnedButtons[i+buttonCount].gameObject.SetActive(false);//+1 munus 1
            }
        }
    }

    public void StartSpawning()
    {
        buttonCount=0;
    }
}