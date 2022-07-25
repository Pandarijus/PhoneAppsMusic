using UnityEngine;

public class Saver : MonoBehaviour
{
    public static Saver instance;
    public ColorPalette colorPalette;
    public AudioClip clip;
    public float muiscVolume=1f,popVolume=1f;
    [SerializeField] private AudioClip[] baseMusicsAudioClips;

    void Awake()
    {
        CheckForOthers();
    }

    private void CheckForOthers()
    {
        if (instance !=null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public string GetAudioClipName()
    {
        return (clip!=null)?clip.name:"";
    }

    public bool IsCustomMusic()
    {
        foreach (var c in baseMusicsAudioClips)
        {
            if (c == clip)
            {
                return false;
            }
        }
        return true;
    }
}
