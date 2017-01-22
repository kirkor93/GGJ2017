using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatsMeeting : MonoBehaviour {
	public GameObject radevousParticle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Bat") {
			if (radevousParticle != null) {
				GameObject randevousPartClone = (GameObject)Instantiate (radevousParticle.gameObject, transform.position + Vector3.back, Quaternion.identity) as GameObject;
			}
			GameManager.Instance.StopGame(true);
		}

	}
}