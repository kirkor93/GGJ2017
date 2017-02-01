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
    public Image ThirdLogo;
    public Transform SecondLogoTarget;
    public Image[] MenuButtons;
    public GameObject[] Shrooms;
    public Image LeftImage;
    public Image Credits;
    public Image OverrideBackground;

    public Image HintScreen1;
    public Image HintScreen2;
    public Image HintBg;
	// AudioSource theme;

    private void Awake()
    {
        FirstLogo.DOFade(0.0f, 2.0f).OnComplete(OnFirstLogoFadeComplete).SetDelay(1.5f);
		//theme = GetComponent<AudioSource> ();
		//theme.playOnAwake = true;

    }

    private void OnFirstLogoFadeComplete()
    {
        Sequence s = DOTween.Sequence();
        s.Append(SecondLogo.DOFade(1.0f, 1.5f));
        s.Append(OverrideBackground.DOFade(1.0f, 0.1f).SetDelay(2.0f));
        s.Append(SecondLogo.DOFade(0.0f, 1.5f).SetDelay(1.0f));

        s.OnComplete(OnSecondLogoFadeComplete);
    }

    
    private void OnSecondLogoFadeComplete()
    {
        ThirdLogo.DOFade(1.0f, 1.5f).OnComplete(OnThirdLogoFadeComplete);
    }

    private void OnThirdLogoFadeComplete()
    {
        ThirdLogo.transform.DOScale(Vector3.one * 0.8f, 2.0f)
            .OnStart(() => ThirdLogo.transform.DOMove(SecondLogoTarget.position, 2.0f)).OnComplete(ShowButtons);
    }

    private void ShowButtons()
    {
        MenuButtons[0].DOFade(1.0f, 0.6f).OnComplete(() => MenuButtons[1].DOFade(1.0f, 0.6f).OnComplete(() => MenuButtons[2].DOFade(1.0f, 0.6f).OnComplete(ShowShrooms)));
        LeftImage.DOFade(1.0f, 1.5f);
    }

    private void ShowShrooms()
    {
        ShowShrooms(true);
    }

    private void ShowShrooms(bool show)
    {
        float value = show ? 1.0f : 0.0f;
        foreach (GameObject pair in Shrooms)
        {
            Image[] images = pair.GetComponentsInChildren<Image>(true);
            float delay = Random.Range(0.0f, 2.0f);
            foreach (Image image in images)
            {
                float spawnTime = Random.Range(0.2f, 0.8f);
                image.DOFade(value, spawnTime).SetDelay(delay);
            }
        }
    }

    public void StartButton()
    {
        ShowShrooms(false);
        foreach (Image button in MenuButtons)
        {
            button.DOFade(0.0f, 1.0f);
        }
        LeftImage.DOFade(0.0f, 1.0f);
        Credits.DOFade(0.0f, 1.0f);
        ThirdLogo.DOFade(0.0f, 1.0f);

        HintBg.gameObject.SetActive(true);
        HintBg.DOFade(1.0f, 1.0f);
        HintScreen1.gameObject.SetActive(true);
        HintScreen1.DOFade(1.0f, 1.0f).SetDelay(1.0f);
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

    public void HintButton(int hintNum)
    {
        switch (hintNum)
        {
            case 1:
                HintScreen1.DOFade(0.0f, 1.0f).OnComplete(() =>
                {
                    HintScreen1.gameObject.SetActive(false);
                HintScreen2.gameObject.SetActive(true);
        });
        HintScreen2.DOFade(1.0f, 1.0f).SetDelay(1.0f);
                break;
            case 2:
            default:
                string[] levels = new[] { "Level1", "Level2" };
                SceneManager.LoadScene(levels[Random.Range(0, levels.Length)]);
                break;
        }
    }
}
