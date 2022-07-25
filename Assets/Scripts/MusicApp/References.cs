using System;
using UnityEngine;
public class References : MonoBehaviour
{
    public static References _instance;//72037D    37003D
    public Sprite canPlay, canPause;//isMuted, isNotMuted,
    public Color on, off;
    private void Awake()
    {
        _instance = this;
    }
}
