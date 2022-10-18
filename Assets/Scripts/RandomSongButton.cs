using UnityEngine;

public class RandomSongButton : MyButton
{
    [SerializeField] private ButtonType type;
    protected override void OnClick()
    {
        if (type == ButtonType.RandomLevel)
        {
            PathBoy.instance.LoadRandomLevel();
        }else if (type == ButtonType.Prev)
        {
            PathBoy.instance.LoadPrevLevel();
        }else if (type == ButtonType.Next)
        {
            PathBoy.instance.LoadNextLevel();
        }
      
    }
    
    private enum ButtonType
    {
        RandomLevel,Prev,Next
    }
}
