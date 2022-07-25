using UnityEngine;

public class Singelton : MonoBehaviour
{
    public static Singelton instance;
    void Awake()
    {
        CheckForOthers();
    }

    private void CheckForOthers()
    {
        if (FindObjectsOfType<Singelton>().Length > 1)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    
}
