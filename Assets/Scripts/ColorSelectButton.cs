using UnityEngine;

public class ColorSelectButton : MyButton
{
    [SerializeField] private ColorPalette _colorPalette;
    void Awake()
    {
        ShowColorPaletteColors();
    }

    private void ShowColorPaletteColors()
    {
       // _colorPalett.
    }

    protected override void OnClick()
    {
        SoundManager.instance.PlaySound(Sound.click);
        Saver.instance.colorPalette = _colorPalette;
    }
}
