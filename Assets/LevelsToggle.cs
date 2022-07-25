using System;
using UnityEngine;

public class LevelsToggle : MyToggle
{
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private bool[] inverted;

    private void Start()
    {
        bool isOn = PlayerPrefs.GetInt("ToggleIsOn",0) == 1;
        SetToggle(isOn);
        OnToggleChange(isOn);
    }

    protected override void OnToggleChange(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("ToggleIsOn", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ToggleIsOn", 0);
        }

        for (int i = 0; i < gameObjects.Length; i++)
        {
            bool isActive = (inverted[i]) ? !isOn : isOn;
            gameObjects[i].SetActive(isActive);
        }
    }
}
