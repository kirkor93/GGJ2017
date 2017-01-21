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
    public KeyCode Echolocate;

    public GameObject EcholocatorPrefab;
    public float EcholocatorFuel;
    public float EcholocatorFuelRequired;
    public float EcholocatorTimeout;

    private float _echolocatorTimer;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
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

	    if (Input.GetKeyDown(Echolocate))
	    {
            TryEcholocate();
	    }

	    if (_echolocatorTimer > 0.0)
	    {
	        _echolocatorTimer -= Time.deltaTime;
	    }
	}

    private void TryEcholocate()
    {
        if (EcholocatorFuel >= EcholocatorFuelRequired && _echolocatorTimer <= 0)
        {
            GameObject echo = Instantiate(EcholocatorPrefab,
                new Vector3(transform.position.x, transform.position.y, 0.0f), EcholocatorPrefab.transform.rotation);

            EcholocatorFuel -= EcholocatorFuelRequired;
            _echolocatorTimer = EcholocatorTimeout;

            GameManager.Instance.MoveSpider();
            GameManager.Instance.ProcessFlies();
        }
    }
}
