﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatsMeeting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collision2D other){
		if (other.gameObject.tag == "Bat") {
			print ("randevous");
		}
	}
}