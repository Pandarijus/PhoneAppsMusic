using UnityEngine;
using UnityEngine.UI;

public class ColorSelectButton : MyButton
{
    private int index;
   // [SerializeField] private ColorPalette _colorPalette;

    [SerializeField] private Image image1, image2, image3, backgroundImage;

    protected override void OnClick()
    {
//        SoundManager.instance.PlaySound(Sound.click);
        //Saver.instance.colorPalette = _colorPalette;
        Saver.instance.SetColorPalette(index);
    }

    public void Setup(ColorPalette colorPalette,int i)
    {
        index = i;
     //   image1.color = colorPalette.mainColor;//.timingColor;
        image2.color = colorPalette.mainColor;
        image3.color = colorPalette.darkestColor;
        backgroundImage.color = colorPalette.backgroundColor;
    }
}
