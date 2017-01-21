using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Text timer;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeToString ();
	}

	void timeToString(){
		float timerFloat;
		string timerString;

		timerFloat = Mathf.Ceil( GameManager.Instance.GameTimer);
		timerString = timerFloat.ToString ();
		timer.text = timerString;
	}
}
