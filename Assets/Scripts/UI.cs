using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : Singleton<UI> {

	public Text timer;
    public Image[] PlayerBars;

    public Image WonScreen;
    public Image LostScreen;
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

	    if (Input.GetKeyDown(KeyCode.R))
	    {
            RestartButton();
	    }
	    else if(Input.GetKeyDown(KeyCode.Alpha1))
	    {
	        MainMenuButton();
	    }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
	    {
            SceneManager.LoadScene("Level1");
        }
	    else if (Input.GetKeyDown(KeyCode.Alpha3))
	    {
            SceneManager.LoadScene("Level2");
        }

    }

    public void ShowEndgameScreen(bool won)
    {
        Image selectedScreen = won ? WonScreen : LostScreen;
        selectedScreen.DOFade(1.0f, 2.0f).SetDelay(1.0f);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
