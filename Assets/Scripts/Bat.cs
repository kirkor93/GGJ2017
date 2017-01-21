using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

	public float speed;

	//Movement direction variables
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		BatsControll ();	
	}

	void BatsControll(){

		Vector3 changePos;

		//Female controll
		if (Input.GetKey (up)) {
			changePos = new Vector3 (0, Time.deltaTime*speed,0);
			transform.position +=changePos;
		} else if (Input.GetKey (down)) {
			changePos = new Vector3 (0, -Time.deltaTime*speed,0);
			transform.position +=changePos;
		}

		if (Input.GetKey (right)) {
			changePos = new Vector3 (Time.deltaTime * speed, 0, 0);
			transform.position += changePos;
		} else if (Input.GetKey (left)) {
			changePos = new Vector3 (-Time.deltaTime * speed, 0, 0);
			transform.position += changePos;
		}
	}
}
