using UnityEngine;

public class DeletaData : MyButton
{
    protected override void OnClick()
    {
       FirebaseManager.DeleteUserInfo();
    }
}
