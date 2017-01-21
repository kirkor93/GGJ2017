using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Text timer;
    public Image[] PlayerBars;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    timer.text = Mathf.Ceil(GameManager.Instance.GameTimer).ToString();
	    for (int i = 0; i < PlayerBars.Length; i++)
	    {
	        Bat bat = GameManager.Instance.Players[i];
	        PlayerBars[i].fillAmount = Mathf.Clamp01(bat.EcholocatorFuel / bat.EcholocatorFuelRequired);
	    }
	}
}
