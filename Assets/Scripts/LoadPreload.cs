using UnityEngine;

public class LoadPreload : MonoBehaviour
{
#if UNITY_EDITOR
    public static LoadPreload instance;
    [SerializeField] bool isPrelaod;

    void Awake()
    {
        if (isPrelaod)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Loader.LoadLevel(Level._PreLoad);
            //Loader;
        }
    }
#endif
}