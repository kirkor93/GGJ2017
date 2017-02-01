using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

	private AudioSource _audioSource;

    public AudioClip MenuTheme;
    public AudioClip LevelThemeIntro;
    public AudioClip LevelThemeLoop;
    
	private void Start ()
    {
        DontDestroyOnLoad(gameObject);

		_audioSource = GetComponent<AudioSource> ();
        _audioSource.loop = false;
        _audioSource.clip = MenuTheme;
        _audioSource.Play();

        SceneManager.sceneLoaded += OnSceneLoad;
	}

    private void Update ()
    {
        if (!_audioSource.isPlaying)
        {
            if (_audioSource.clip == LevelThemeIntro)
            {
                _audioSource.clip = LevelThemeLoop;
            }

            _audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "MainMenu")
        {
            StartClip(MenuTheme);
        }
        else if(_audioSource.clip == MenuTheme)
        {
            StartClip(LevelThemeIntro);
        }
    }

    private void StartClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
