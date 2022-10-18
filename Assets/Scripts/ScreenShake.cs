using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;
    private bool isShaking;
    private float end;

    private void Awake()
    {
        instance = this;
    }
    public async Task Shake(float duration, float magnitude)
    {
        if (!isShaking)
        {
            Vector3 originalPos = transform.localPosition;
            end = Time.time+duration;
            isShaking = true;
            while (end > Time.time)
            {
                Shake(magnitude);
                await Task.Yield();
            }
            transform.localPosition = originalPos;
            isShaking = false;
        }
        else
        {
            end += duration;
        }
        
     
    }

    private void Shake(float magnitude)
    {
        transform.localPosition += new Vector3(Random.Range(-magnitude, magnitude),Random.Range(-magnitude, magnitude),0 );
    }

    public void StopShaking()
    {
        isShaking = false;
        end = 0;
    }
}
