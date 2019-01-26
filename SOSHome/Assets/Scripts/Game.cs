using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Timers;
using TMPro;

public class Game : MonoBehaviour
{

    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI comboUI;
    public Image comboBarUI;
    public TextMeshProUGUI scoreUI;

    Timer comboTimer;
    public float initialComboTimer = 10f;
    public int currentCombo;
    public int score;
    public float currentComboTimerInitValue;

    // Start is called before the first frame update
    void Start()
    {
        TimersManager.SetTimer(this, 78f, printTimer);
        score = 0;
        currentCombo = 1;
        currentComboTimerInitValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = (int) TimersManager.RemainingTime(printTimer) / 60;
        int seconds = (int)TimersManager.RemainingTime(printTimer) % 60;
        int hundredth = (int)(((TimersManager.RemainingTime(printTimer) - (int)TimersManager.RemainingTime(printTimer))) * 100);
        timerUI.text =   minutes.ToString() + ":" +  seconds.ToString() + ":" + hundredth.ToString();


        if(Input.GetKeyDown(KeyCode.P))
        {
            scorePoints(10);
            currentComboTimerInitValue = initialComboTimer / currentCombo * 3;
            TimersManager.SetTimer(this, currentComboTimerInitValue, endCombo);

        }

        if(currentComboTimerInitValue != 0 && TimersManager.RemainingTime(endCombo) > 0)
        comboBarUI.fillAmount = 1 - (currentComboTimerInitValue - TimersManager.RemainingTime(endCombo)) / currentComboTimerInitValue;

        if (currentCombo >= 5)
        {
            comboBarUI.color = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * 0.4f , 1), 1, 1));
        }

    }

    void scorePoints(int points)
    {
        score += points * currentCombo;
        scoreUI.text = score.ToString();
        setCombo(++currentCombo);

      
    }

    void printTimer()
    {
        //print("Timer" + TimersManager.RemainingTime(printTimer));
    }

    void endCombo()
    {
        setCombo(1);

    }

    void setCombo(int val)
    {
        currentCombo = val;

        if(val <= 1)
        {
            comboBarUI.fillAmount = 0;
        }
        else
        {
            comboBarUI.fillAmount = 1;
        }

        comboUI.text = currentCombo + "x";
        
    }


    }

