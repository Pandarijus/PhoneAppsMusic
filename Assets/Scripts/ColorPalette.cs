using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class ColorPalette : ScriptableObject
{
    public Color backgroundColor;
    public Color  mainColor;
    [FormerlySerializedAs("blockColor")] public Color  darkestColor;
    
    

}
