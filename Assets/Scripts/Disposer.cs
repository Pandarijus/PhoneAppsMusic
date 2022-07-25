using System;
using System.Collections.Generic;
using UnityEngine;
//Make disposer be able identify if object is of the same object type and sort it into the correct list of stacks
public class Disposer : MonoBehaviour
{
    private Stack<GameObject> availableMusicBlocks = new Stack<GameObject>();
    private Stack<GameObject> availableExplosions = new Stack<GameObject>();
    private Stack<GameObject> availableScoreTexts = new Stack<GameObject>();
    
    [SerializeField] protected GameObject musicBlockPrefab,explosionPrefab,scoreTextPrefab;
    public void DisposeMusicBlock(GameObject obj)
    {
        obj.SetActive(false);
        availableMusicBlocks.Push(obj);
    }
    public void DisposeExplosion(GameObject obj)
    {
        obj.SetActive(false);
        availableExplosions.Push(obj);
    }
    public void DisposeScoreText(GameObject obj)
    {
        obj.SetActive(false);
        availableScoreTexts.Push(obj);
    }

    public GameObject GetMusicBlock()// could cause problems if I rotate the prefab before disposing it
    {
        if (availableMusicBlocks.Count == 0)
        {
            return Instantiate(musicBlockPrefab, transform);
        }
        else
        {
            var g = availableMusicBlocks.Pop();
            g.SetActive(true);
            return g;
        }
    }
    public GameObject GetExplosion()// could cause problems if I rotate the prefab before disposing it
    {
        if (availableExplosions.Count == 0)
        {
            return Instantiate(explosionPrefab, transform);
        }
        else
        {
            var g = availableExplosions.Pop();
            g.SetActive(true);
            return g;
        }
    }
    private Transform canvasTransform;

    protected void Awake()
    {
        canvasTransform = ScoreManager.instance.transform.parent;
    }

    public GameObject GetScoreText()// could cause problems if I rotate the prefab before disposing it
    {
        if (availableScoreTexts.Count == 0)
        {
            return Instantiate(scoreTextPrefab,canvasTransform);
        }
        else
        {
            var g = availableScoreTexts.Pop();
            g.SetActive(true);
            return g;
        }
    }
    
}