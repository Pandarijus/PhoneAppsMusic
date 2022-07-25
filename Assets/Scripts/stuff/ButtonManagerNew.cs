using UnityEngine;

public class ButtonManagerNew : MyButton
{
    [SerializeField] private ButtonAction buttonAction;
    protected override void OnClick()
    {
        if (buttonAction == ButtonAction.GoBack)
        {
            SingeltonManager.pathManager.LoadPrevDirectory();
        }
        else if (buttonAction == ButtonAction.Save)
        {
               SingeltonManager.saveManger.Save();
       //     SingeltonManager.pathManager.ToggleShowFiles();
        }
        // else if (buttonAction == ButtonAction.GoBack)
        // {
        // }
    }
    
    private enum ButtonAction
    {
        GoBack,
        ShowFiles,
        Save
    }
}