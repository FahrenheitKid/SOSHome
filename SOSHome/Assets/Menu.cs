using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum MenuState
{
    Home,
    Tutorial,
    Credits
}

public class Menu : MonoBehaviour
{
    public MenuState state;
    public Animator cameraAnimator;

    [Header("Home")]
    public int menuIndex;
    public TextMeshProUGUI playOption;
    public TextMeshProUGUI tutorialOption;
    public TextMeshProUGUI creditsOption;

    [Header("Fade")]
    public Image fade;
    public float fadeDuration;

    private void Update()
    {
        switch (state)
        {
            case MenuState.Home:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    menuIndex--;

                    if (menuIndex < 0)
                    {
                        menuIndex = 2;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    menuIndex++;

                    if (menuIndex > 2)
                    {
                        menuIndex = 0;
                    }
                }

                switch (menuIndex)
                {
                    case 0:
                        playOption.color = Color.yellow;
                        tutorialOption.color = Color.white;
                        creditsOption.color = Color.white;
                        break;
                    case 1:
                        playOption.color = Color.white;
                        tutorialOption.color = Color.yellow;
                        creditsOption.color = Color.white;
                        break;
                    case 2:
                        playOption.color = Color.white;
                        tutorialOption.color = Color.white;
                        creditsOption.color = Color.yellow;
                        break;
                }

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    switch (menuIndex)
                    {
                        case 0:
                            IEnumerator fade = Fade(fadeDuration);
                            StartCoroutine(fade);
                            break;
                        case 1:
                            state = MenuState.Tutorial;
                            cameraAnimator.SetTrigger("Tutorial");
                            break;
                        case 2:
                            state = MenuState.Credits;
                            cameraAnimator.SetTrigger("Credits");
                            break;
                    }
                }

                break;
            case MenuState.Tutorial:
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
                {
                    state = MenuState.Home;
                    cameraAnimator.SetTrigger("Home");
                }
                break;
            case MenuState.Credits:
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
                {
                    state = MenuState.Home;
                    cameraAnimator.SetTrigger("Home");
                }
                break;
        }
    }

    IEnumerator Fade(float time)
    {
        while (fade.color.a < 1)
        {
            fade.color = new Color(0, 0, 0, fade.color.a + Time.deltaTime / time);
            yield return null;
        }

        SceneManager.LoadScene(1);
    }
}
