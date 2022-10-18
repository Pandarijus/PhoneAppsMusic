using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MySlider
{
     [SerializeField] private bool isMusicVolume;

     private void Awake()
     {
          if (isMusicVolume)
          {
             SetSliderValue(Saver.instance.musicVolume);
          }
          else
          {
               SetSliderValue(Saver.instance.popVolume);
          }
     }

     protected override void OnSliderChange(float value)
     {
          Saver.InvokeVolumeChange(value,isMusicVolume);
     }
}
