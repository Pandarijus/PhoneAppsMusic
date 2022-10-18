using System.Collections.Generic;
using UnityEngine;
public class NewDisposer : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs; //need to be the same as the enum 
    [SerializeField] private Transform[] parentTransfroms; //need to be the same as the enum 

    private Dictionary<DisposableType, Stack<GameObject>> availableObjects = new Dictionary<DisposableType, Stack<GameObject>>();
    private Dictionary<DisposableType, Stack<GameObject>> spawnedObjects = new Dictionary<DisposableType, Stack<GameObject>>();
    
    
    public void DisposeAll(DisposableType dType)
    {
        var spawnedGameObjects = GetSpawnedObjectsStack(dType);
       // Debug.Log("Dispose Amount:"+spawnedGameObjects.Count);
        var c = spawnedGameObjects.Count;
        for (int i = 0; i < c; i++)
        {
            var g = spawnedGameObjects.Pop();
         //   Debug.Log($"[{i}]pop: \"{g.name}\"");
            Dispose(dType,g);
        }
    }
    
    public void Dispose(DisposableType dType, GameObject obj)
    {
        obj.SetActive(false);
        GetAvailableObjectsStack(dType).Push(obj);
    }
    private Stack<GameObject> GetAvailableObjectsStack(DisposableType dType)
    {
        if (availableObjects.ContainsKey(dType) == false)
        {
            availableObjects.Add(dType, new Stack<GameObject>());
        }

        return availableObjects[dType];
    }
    
    public GameObject GetDisposableObject(DisposableType dType)
    {
        var availableObjectsStack = GetAvailableObjectsStack(dType);
        GameObject theGameObject;
        if (availableObjectsStack.Count == 0)
        {
            Transform trans = parentTransfroms[(int)dType];
            // if (trans == null)
            // {
            //     trans = transform;
            // }
            theGameObject = Instantiate(prefabs[(int)dType],trans);
        }
        else
        {
            theGameObject = availableObjectsStack.Pop();
            theGameObject.SetActive(true);
        }

        GetSpawnedObjectsStack(dType).Push(theGameObject);
        return theGameObject;
    }
   
    
    private Stack<GameObject> GetSpawnedObjectsStack(DisposableType dType)
    {
        if (spawnedObjects.ContainsKey(dType) == false)
        {
            spawnedObjects.Add(dType,new Stack<GameObject>());
        }
        return spawnedObjects[dType];
    }  
   

}

public enum DisposableType
{
    Button
}