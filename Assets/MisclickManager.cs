using UnityEngine;

public class MisclickManager : MonoBehaviour
{
     private void OnMouseDown()
     {
          Debug.Log("Misclick");
          Combo.instance.StopCombo();
     }
}
