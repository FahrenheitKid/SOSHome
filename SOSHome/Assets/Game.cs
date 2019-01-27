using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    [Header("Refs")]
    public Transform spawnPoints;

    private Transform playerPoints;
    private Transform dogPoints;
    private Transform npcPoints;

    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI comboUI;
    public Image comboBarUI;
    public TextMeshProUGUI scoreUI;

    public GameObject fadingText_prefab;
    Timer comboTimer;
    public int pointsPerDog = 10;
    public float initialComboTimer = 10f;
    public int currentCombo;
    public int score;
    public float currentComboTimerInitValue;

    private void Awake()
    {
        if (spawnPoints)
        {
            playerPoints = spawnPoints.GetChild(0);
            dogPoints = spawnPoints.GetChild(1);
            npcPoints = spawnPoints.GetChild(2);
        }
    }

    void Start() {
        //setting initial values
        TimersManager.SetTimer(this, 78f, printTimer);
        score = 0;
        currentCombo = 1;
        currentComboTimerInitValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = (int)TimersManager.RemainingTime(printTimer) / 60;
        int seconds = (int)TimersManager.RemainingTime(printTimer) % 60;
        int hundredth = (int)(((TimersManager.RemainingTime(printTimer) - (int)TimersManager.RemainingTime(printTimer))) * 100);
        timerUI.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + hundredth.ToString("00");
        

        if (Input.GetKeyDown(KeyCode.P))
        {
            scorePoints(10);
           

        }

        if (currentComboTimerInitValue != 0 && TimersManager.RemainingTime(endCombo) > 0)
            comboBarUI.fillAmount = 1 - (currentComboTimerInitValue - TimersManager.RemainingTime(endCombo)) / currentComboTimerInitValue;

        if (currentCombo >= 5)
        {
            comboBarUI.color = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * 0.4f, 1), 1, 1));
        }

    }



    public int scorePoints(int points)
    {
        int aux = points * currentCombo;
        score += points * currentCombo;
        scoreUI.text = score.ToString();
        setCombo(++currentCombo);
        currentComboTimerInitValue = initialComboTimer / currentCombo * 3;
        TimersManager.SetTimer(this, currentComboTimerInitValue, endCombo);

        return aux;
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

        if (val <= 1)
        {
            comboBarUI.fillAmount = 0;
        }
        else
        {
            comboBarUI.fillAmount = 1;
        }

        comboUI.text = currentCombo + "x";

    }




public Vector3 GetRandomPlayerPoint()
    {
        int random = Random.Range(0, playerPoints.childCount);
        return playerPoints.GetChild(random).position;
    }

    public Vector3[] GetRandomDogPoints(int count)
    {
        List<Vector3> selectedPoints = new List<Vector3>();

        do
        {
            int random = Random.Range(0, dogPoints.childCount);
            Vector3 point = dogPoints.GetChild(random).position;

            if (!selectedPoints.Contains(point))
            {
                selectedPoints.Add(point);
            }
        } while (selectedPoints.Count < count);

        return selectedPoints.ToArray();
    }

    public Vector3[] GetRandomNPCPoints(int count)
    {
        List<Vector3> selectedPoints = new List<Vector3>();

        do
        {
            int random = Random.Range(0, npcPoints.childCount);
            Vector3 point = npcPoints.GetChild(random).position;

            if (!selectedPoints.Contains(point))
            {
                selectedPoints.Add(point);
            }
        } while (selectedPoints.Count < count);

        return selectedPoints.ToArray();
    }
}
