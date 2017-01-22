using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    public Texture[] Frames;
    public float FramesPerSecond;

    private MeshRenderer _renderer;
    private float _timer;
    private float _changeTime;
    private int _currentFrame = 0;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.sharedMaterial.mainTexture = Frames[0];
        _changeTime = 1.0f / FramesPerSecond;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
        {
            return;
        }

        _timer += Time.deltaTime;

        if (_timer >= _changeTime)
        {
            _currentFrame++;
            _currentFrame %= Frames.Length;
            _renderer.sharedMaterial.mainTexture = Frames[_currentFrame];
            _timer = 0;
        }

//        _renderer.sharedMaterial.mainTexture 
    }
}
