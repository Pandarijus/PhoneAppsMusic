using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
     //  public static ShopSpawner instance;
     [SerializeField] private GameObject shopPrefab;
    
    void Start()
    {
        var colorPaletts = Saver.instance.colorPalettes;
    for (int i = 0; i < colorPaletts.Length; i++)
    {
     var g =   Instantiate(shopPrefab,transform);
     g.GetComponent<ColorSelectButton>().Setup(colorPaletts[i],i);
    }
    }
}
