using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Image FirstLogo;
    public Image SecondLogo;
    public Transform SecondLogoTarget;
    public Image[] MenuButtons;
    public GameObject[] Shrooms;
    public Image LeftImage;
    public Image Credits;
	// AudioSource theme;

    private void Awake()
    {
        FirstLogo.DOFade(0.0f, 2.0f).OnComplete(OnFirstLogoFadeComplete).SetDelay(1.5f);
		//theme = GetComponent<AudioSource> ();
		//theme.playOnAwake = true;

    }

    private void OnFirstLogoFadeComplete()
    {
        SecondLogo.DOFade(1.0f, 1.5f).OnComplete(OnSecondLogoFadeComplete);
    }

    private void OnSecondLogoFadeComplete()
    {
        SecondLogo.transform.DOScale(Vector3.one * 0.55f, 2.0f)
            .OnStart(() => SecondLogo.transform.DOMove(SecondLogoTarget.position, 2.0f)).OnComplete(ShowButtons);

    }

    private void ShowButtons()
    {
        MenuButtons[0].DOFade(1.0f, 0.6f).OnComplete(() => MenuButtons[1].DOFade(1.0f, 0.6f).OnComplete(() => MenuButtons[2].DOFade(1.0f, 0.6f).OnComplete(ShowShrooms)));
        LeftImage.DOFade(1.0f, 1.5f);
    }

    private void ShowShrooms()
    {
        foreach (GameObject pair in Shrooms)
        {
            Image[] images = pair.GetComponentsInChildren<Image>(true);
            float delay = Random.Range(0.0f, 2.0f);
            foreach (Image image in images)
            {
                float spawnTime = Random.Range(0.2f, 0.8f);
                image.DOFade(1.0f, spawnTime).SetDelay(delay);
            }
        }
    }

    public void StartButton()
    {
        string[] levels = new[] {"Level1", "Level2"};

        
        SceneManager.LoadScene(levels[Random.Range(0, levels.Length)]);
    }

    public void CreditsButton()
    {
        Image faded = Credits;
        Image shown = LeftImage;
        if (LeftImage.material.color.a > 0.5f)
        {
            faded = LeftImage;
            shown = Credits;
        }

        faded.DOFade(0.0f, 1.0f);
        shown.DOFade(1.0f, 1.0f).SetDelay(1.0f);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
