using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayImages : MonoBehaviour
{
    public Texture2D[] textures;
    private RawImage _rawImage;
    private DateTime _lastUpdateTime;
    private int _currentIdx;
    
    private void Awake()
    {
        _rawImage = gameObject.GetOrAddComponent<RawImage>();
        _lastUpdateTime = DateTime.Now;
        _currentIdx = 0;
    }
    
    private void Update()
    {
        if (DateTime.Now.Subtract(_lastUpdateTime).Seconds <= 2) return;
        _currentIdx = (_currentIdx + 1) % textures.Length;
        _rawImage.texture = textures[_currentIdx];
        _lastUpdateTime = DateTime.Now;
    }
}
