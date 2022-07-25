using TMPro;
using UnityEngine;
public class DirectoryButton : MyButton
{
    private string path;//,name;
    private TextMeshProUGUI text;
    private bool isFile,isSelected;
    [SerializeField] private Color fileColor, directoryColor,selectedColor;
     
//FFD300  FC7800
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Setup(bool isFile,string name,string path)
    {
        text.text = name;
        this.name = name;
        this.path = path;
        this.isFile = isFile; 
        isSelected = false;
        Color currentColor = (isFile) ? fileColor : directoryColor;
        SetButtonColor(currentColor);
    }
    protected override void OnClick()
    {
        if (isFile)
        {
            Debug.Log(path);
            isSelected = !isSelected;
            if (isSelected)
            {
                SingeltonManager.saveManger.AddToSave(path);
                SetButtonColor(selectedColor);
            }
            else
            {
                SingeltonManager.saveManger.RemoveFromSave(path);
                SetButtonColor(fileColor);
            }
          //  Color currentColor = (isSelected) ? selectedColor : fileColor;
          // SetButtonColor(currentColor);
        }
        else
        {
            SingeltonManager.pathManager.LoadDirectory(path);
        }
    }
}