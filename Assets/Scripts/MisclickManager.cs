using System;
using UnityEngine;

public class MisclickManager : MonoBehaviour
{
     public static bool isPaused;
     private void OnMouseDown()
     {
//          Debug.Log("Misclick");
          if (isPaused == false)
          {
               Combo.instance.StopCombo();
          }
     }

     private void OnDisable()
     {
          isPaused = false;
     }
}
