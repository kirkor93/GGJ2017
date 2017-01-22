using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	private AudioSource audiosource;
	public AudioClip intro;
	public AudioClip loop;

	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
		audiosource.clip = intro;
		audiosource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		PlayMusic ();
	}

	void PlayMusic(){
		if (!audiosource.isPlaying) {
			audiosource.clip = loop;
			audiosource.Play ();
			audiosource.loop = true;
		}
	}
}
