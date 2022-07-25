using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public abstract class MySlider : MonoBehaviour
{

    private Slider slider;

    void OnEnable()
    {
        GetSlider().onValueChanged.AddListener(OnSliderChange);
    }

    void OnDisable()
    {
        GetSlider().onValueChanged.RemoveListener(OnSliderChange);
    }

    private Slider GetSlider()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
        return slider;
    }

    public void SetSliderValue(float value)
    {
        GetSlider().value = value;
    }

    protected abstract void OnSliderChange(float value);

}
