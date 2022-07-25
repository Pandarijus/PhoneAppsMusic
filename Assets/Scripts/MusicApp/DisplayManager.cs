
using UnityEngine;

public class DisplayManager : MyDisplay
{
    [SerializeField] private DisplayType _displayType;
    private delegate void Inting(int i);
    private static event Inting onMusicChange; 
   // public static DisplayManager musicIndex;
    void Start()
    {
        if (_displayType == DisplayType.Pitch)
        {
            MusicManager.instance.onValueChanged += OnValueChanged;
        }
        else if (_displayType == DisplayType.MusicIndex)
        {
            onMusicChange += OD;
        }
    }
    private void OnValueChanged(float volume, float pitch)
    {
//        Debug.Log(volume+"display"+pitch );
        Display2DecimalPlaces(pitch);
    }

    public static void InformOfMusicChange(int i)
    {
        onMusicChange?.Invoke(i);
    }
    public void OD(int i)
    {
        Display(i);
    }
    private void OnDestroy()
    {
        if (_displayType == DisplayType.Pitch)
        {
            MusicManager.instance.onValueChanged -= OnValueChanged;
        }
        else
        {
            
        }
       
    }

    private enum DisplayType
    {
        Pitch,
        MusicIndex
    }
}
