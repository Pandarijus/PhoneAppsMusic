using UnityEngine;
using UnityEngine.UI;
public class SliderManager : MonoBehaviour
{
    private MusicManager musicManager;

    [SerializeField] private bool isVolume;
    private Slider slider;
    
    void Start()
    {
        musicManager = MusicManager.instance;
        musicManager.onValueChanged += OnValueChanged;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(e => OnSlide(e));

    }

    private void OnSlide(float value)
    {
//        Debug.Log("endless slide?");
        if (isVolume)
        {
            musicManager.SetVolume(value);
        }
        else
        {
            musicManager.SetPitch(value*3);
        }

    }
    private void OnValueChanged(float volume,float pitch)
    {
  //      Debug.Log("endless loop?");
        slider.value = (isVolume) ? volume : pitch/3;
    }
}
