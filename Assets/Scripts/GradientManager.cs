using UnityEngine;

public class GradientManager
{
    public static Gradient colorGradient;

    public static void UpdateGradient(Color color1,Color color2)
    {
        colorGradient = new Gradient();
        
        var colorKey = new GradientColorKey[2];
        colorKey[0].color = color1;
        colorKey[0].time = 0.0f;
        colorKey[1].color = color2;
        colorKey[1].time = 1.0f;


        var alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        colorGradient.SetKeys(colorKey, alphaKey);
        // colorGradient.colorKeys[0].color = color1;
        // colorGradient.colorKeys[0].time = 0;
        // colorGradient.colorKeys[1].color = color2;
        // colorGradient.colorKeys[1].time = 1;
    }
    public static Color Evaluate(float getTimingValue)
    {
       return colorGradient.Evaluate(getTimingValue);
    }
}
