using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocation : MonoBehaviour {

	public Light echoLightPrefab;
	private GameObject plain;

	public float echoBar = 100.0f;
	private float resetEchoBar;

	private bool canSuper;

	// Use this for initialization
	void Start () {
		resetEchoBar = echoBar;
		plain = GameObject.FindGameObjectWithTag("Plain");
	}
	
	// Update is called once per frame
	void Update () {
		EchoLoads ();
	}

	void EchoLoads(){
		echoBar += Time.deltaTime;
		if (echoBar == resetEchoBar) {
			canSuper = true;
		} else {
			canSuper = false;
		}

		//Miejsce na muchy
	}

	void SuperEcho(){
		if (canSuper) {
			//GameObject echoLightClone = Instantiate(echoLightPrefab, new Vector3 (0,plain.gameObject.transform.position.z,0 + 0.1f,0), Quaternion.
		}
	}
}
