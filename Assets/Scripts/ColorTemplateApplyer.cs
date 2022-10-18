using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorTemplateApplyer : MonoBehaviour
{
    [SerializeField] private ColorType colorType;
    [Header("Automatic")]
    [SerializeField] private ComponentType componentType;
    private static ColorPalette cp;

    void Start()
    {
        cp = Saver.instance.colorPalette;
        SetColorFromTemplate();
    }

    public void SetColorFromTemplate()
    {
        if (colorType == ColorType.mainColor)
        {
          SetColor(cp.mainColor);
        }else if (colorType == ColorType.darkestColor)
        {
            SetColor(cp.darkestColor);
        }else if (colorType == ColorType.backgroundColor)
        {
            SetColor(cp.backgroundColor);
        }else if (colorType == ColorType.mainColor2)
        {
            SetColor(cp.mainColor);
           // SetColor(cp.timingColor);
        }
    }

    private void SetColor(Color color)
    {
        if (componentType == ComponentType.sprite)
        {
            GetComponent<SpriteRenderer>().color = color;
        }
        else  if (componentType == ComponentType.image)
        {
            GetComponent<Image>().color = color;
        } else  if (componentType == ComponentType.text)
        {
            GetComponent<TextMeshProUGUI>().color = color;
        }else  if (componentType == ComponentType.camera)
        {
            GetComponent<Camera>().backgroundColor = color;
        }
        
       
        
    }

    private void OnAdd()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            componentType = ComponentType.sprite;
        }

        else
        {
            componentType = ComponentType.image;
        }
    }

    private void UpdateColorTemplate(ColorPalette colorPalette)
    {
        cp = colorPalette;
        SetColorFromTemplate();
    }
    private void OnEnable()
    {
        Saver.OnColorChange += UpdateColorTemplate;
    }

    private void OnDisable()
    {
        Saver.OnColorChange -= UpdateColorTemplate;
    }

    private enum ColorType
    { 
        mainColor,mainColor2,darkestColor,backgroundColor
    }
    private enum ComponentType
    { 
        sprite,image,text,camera//,blockColor,backgroundColor,timingColor
    }
}