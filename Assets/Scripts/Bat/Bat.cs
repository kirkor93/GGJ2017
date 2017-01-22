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
    private bool _steeringInverted = false;

    public bool SteeringInverted
    {
        get { return _steeringInverted; }
        set { _steeringInverted = value; }
    }

    // Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		BatsControll ();	
	}

	void BatsControll(){

		Vector3 changePos = new Vector3(0, 0, 0);

		//Female controll
		if (Input.GetKey (up))
		{
		    changePos.y += Time.deltaTime * speed;

		} else if (Input.GetKey (down))
        {
            changePos.y -= Time.deltaTime * speed;
        }

		if (Input.GetKey (right))
        {
            changePos.x += Time.deltaTime * speed;
        } else if (Input.GetKey (left))
        {
            changePos.x -= Time.deltaTime * speed;
        }

	    if (SteeringInverted)
	    {
	        changePos *= -1.0f;
	    }

	    Vector3 oldPos = transform.position;
        transform.position += changePos;

	    if (changePos.sqrMagnitude > float.Epsilon)
	    {
	        Vector3 vectorToTarget = transform.position - oldPos;
	        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
	        Quaternion q = Quaternion.Euler(0, 0, angle - 90);
	        transform.rotation = Quaternion.Lerp(transform.rotation, q, 0.1f);
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

            GameManager.Instance.ProcessFlies();
        }
    }
}
