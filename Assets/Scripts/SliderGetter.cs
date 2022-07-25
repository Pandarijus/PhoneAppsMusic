using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderGetter : MyDisplay
{
    private Slider slider;

    //   [SerializeField] private float minValue, maxValue;
    [NonSerialized] public float value;

    [SerializeField] private bool startDelay;

    [SerializeField] private AudioSource _audioSource;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(e => OnSlide(e));
        OnSlide(slider.value);
    }

    private void OnSlide(float rawValue)
    {
        // value = Mathf.Lerp(minValue,maxValue, rawValue);
        value = rawValue;
        if (startDelay)
        {
            AudioVisualizer.staticStartDelay = value;
        }
        else if(_audioSource != null)
        {
            _audioSource.volume = value;
        }
        Display(value);
    }

    public float GetValue()
    {
        return value;
    }

}
