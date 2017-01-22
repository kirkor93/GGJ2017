using System;
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

    private void Awake()
    {
        FirstLogo.DOFade(0.0f, 2.0f).OnComplete(OnFirstLogoFadeComplete).SetDelay(1.5f);
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
        MenuButtons[0].DOFade(1.0f, 0.6f).OnComplete(() => MenuButtons[1].DOFade(1.0f, 0.6f).OnComplete(() => MenuButtons[2].DOFade(1.0f, 0.6f)));
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void CreditsButton()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
