using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoScaler : MonoBehaviour {
	Camera mainCamera;

	public Transform biggerRingTrans;
	public Transform cylinderTrans;
	public Light echoLight;

	public float scaler;
	public float echoSpeed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		ScaleAll ();
	}

	void ScaleAll(){		
		biggerRingTrans.localScale 	+=  new Vector3(biggerRingTrans.localScale.x, biggerRingTrans.localScale.y, 0.1f)*echoSpeed*Time.deltaTime;
		cylinderTrans.localScale 	+=  new Vector3(biggerRingTrans.localScale.x, 0.1f, biggerRingTrans.localScale.z)*echoSpeed*Time.deltaTime;
		echoLight.range *= scaler;
	}
}
