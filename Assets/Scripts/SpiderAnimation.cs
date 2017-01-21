using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour {
	Renderer rend;

	//Sprite net
	public int columns	= 8;
	public int rows 	= 5;

	private int i;
	public int currentFrame =1;
	private float fps = 40.0f;
	public float animTime = 0.0f;

	//Animation control variables
	private Vector2 frameSize;
	private Vector2 frameOffset;
	private Vector2 framePosition;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		AnimationPlay ();
	}

	void AnimationPlay(){


		animTime -= Time.deltaTime;

		if (animTime <= 0) {
			currentFrame += 1;
			animTime += 1.0f;
		}

		if (currentFrame == 41) {
			currentFrame = 1;
		}

		framePosition.y = 1;
		for(i = currentFrame; i > columns; i -= rows){
			framePosition.y += 1;
		}
		framePosition.x = i - 1;

		frameSize = new Vector2 (1.0f / columns, 1.0f / rows);
		frameOffset = new Vector2 (framePosition.x / columns, 1.0f - (framePosition.y / rows));

		rend.material.SetTextureScale ("_MainTex", frameSize);
		rend.material.SetTextureOffset ("_MainTex", frameOffset);
	}
}
