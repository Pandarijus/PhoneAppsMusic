using UnityEngine;
using UnityEngine.UI;

public abstract class MyButton : MonoBehaviour
{
    protected Button myButton;


    void OnEnable()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        myButton.onClick.RemoveListener(OnClick);
    }

    public void SetButtonColor(Color color)
    {
       // myButton.image.color = color;
       myButton.targetGraphic.color = color;
    }
    protected abstract void OnClick();

}
