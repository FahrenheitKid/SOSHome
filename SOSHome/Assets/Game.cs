using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [Header("Refs")]
    public Transform spawnPoints;

    //Lets make a list of prefab instances in the inspector.
    //These lists will contain the basic instances of NPCs and Dogs.
    //Use these to instantiate basic prefabs that can get the game going....In the future, make it more generic.
    public GameObject[] NPCsPrefabs;
    public GameObject[] DogsPrefabs;
    //The number desired of instances of each type on the map, at any given time.
    [Range(1, 11)]
    public int dogAmount = 1;
    [Range(1, 11)]
    public int npcAmount = 1;
    //These hold the coordinates (In Vector3 type) that serves as a reference of a location to spawn each instance.
    private Transform playerPoints;
    private Transform dogPoints;
    private Transform npcPoints;

    [Header("Timer")]
    public float initialTime;

    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI comboUI;
    public Image comboBarUI;
    public TextMeshProUGUI scoreUI;

    Timer comboTimer;
    public int pointsPerDog = 10;
    public float initialComboTimer = 10f;
    public int currentCombo;
    public int score;
    public float currentComboTimerInitValue;

    [Header("High Score")]
    public Transform highScoreCanvas;
    bool gameEnded = false;

    [Header("Audio")]
    public AudioManager audioManager;

    private bool seconds30 = false;
    private bool seconds5 = false;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayGameTheme();
        audioManager.PlayScoreTheme();

        audioManager.wheelSource.Play();
        audioManager.wheelSource.Pause();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Cursor.visible = false;

        if (spawnPoints)
        {
            playerPoints = spawnPoints.GetChild(0);
            dogPoints = spawnPoints.GetChild(1);
            npcPoints = spawnPoints.GetChild(2);
        }
    }

    void Start() {
        //setting initial values
        TimersManager.SetTimer(this, initialTime, printTimer);
        score = 0;
        currentCombo = 1;
        currentComboTimerInitValue = 0;

        SpawnInstances();
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = (int)TimersManager.RemainingTime(printTimer) / 60;
        int seconds = (int)TimersManager.RemainingTime(printTimer) % 60;
        int hundredth = (int)(((TimersManager.RemainingTime(printTimer) - (int)TimersManager.RemainingTime(printTimer))) * 100);

        string min, sec, hun;

        if (minutes - 10 < 0)
            min = "0" + minutes.ToString();
        else
            min = minutes.ToString();

        if (seconds - 10 < 0)
            sec = "0" + seconds.ToString();
        else
            sec = seconds.ToString();

        if (hundredth - 10 < 0)
            hun = "0" + hundredth.ToString();
        else
            hun = hundredth.ToString();

        timerUI.text = min + ":" + sec + ":" + hun;

        if (!seconds30 && TimersManager.RemainingTime(printTimer) <= 30)
        {
            seconds30 = true;
            audioManager.PlayTimer30Seconds();
        }

        if (!seconds5 && TimersManager.RemainingTime(printTimer) <= 5)
        {
            seconds5 = true;
            audioManager.PlayTimer5Seconds();
        }

        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            scorePoints(10);
           

        }*/

        if (currentComboTimerInitValue != 0 && TimersManager.RemainingTime(endCombo) > 0)
            comboBarUI.fillAmount = 1 - (currentComboTimerInitValue - TimersManager.RemainingTime(endCombo)) / currentComboTimerInitValue;

        if (currentCombo >= 3)
        {
            comboBarUI.color = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * 0.4f, 1), 1, 1));
        }

        if (!gameEnded && TimersManager.RemainingTime(printTimer) <= 0)
        {
            audioManager.PlayTimeUp();
            End();
        }
    }

    public void End()
    {
        StartCoroutine("PlayScore");

        audioManager.StopWheel();

        gameEnded = true;
        FindObjectOfType<PlayerScript>().enabled = false;
        highScoreCanvas.gameObject.SetActive(true);

        timerUI.GetComponentInParent<Canvas>().enabled = false;

        StartCoroutine("EndGame");
    }

    public void scorePoints(int points)
    {
        score += points * currentCombo;
        scoreUI.text = score.ToString();
        setCombo(++currentCombo);
        currentComboTimerInitValue = initialComboTimer / currentCombo * 3;
        TimersManager.SetTimer(this, currentComboTimerInitValue, endCombo);


    }

    public void printTimer()
    {
        //print("Timer" + TimersManager.RemainingTime(printTimer));
    }

    void endCombo()
    {
        audioManager.PlayComboBreak();
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


    IEnumerator EndGame()
    {
        bool achievedHighscore = false;

        highScoreCanvas.GetChild(1).GetComponent<TextMeshProUGUI>().text = "YOUR SCORE: " + score;
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            achievedHighscore = true;
            PlayerPrefs.SetInt("Highscore", score);
        }
        else
        {
            highScoreCanvas.GetChild(2).GetComponent<TextMeshProUGUI>().text = "HIGH SCORE: " + PlayerPrefs.GetInt("Highscore", 0);
        }

        while (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Backspace) && !Input.GetKey(KeyCode.Escape))
        {
            if (achievedHighscore)
            {
                highScoreCanvas.GetChild(2).GetComponent<TextMeshProUGUI>().color = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * 0.4f, 1), 1, 1));
            }

            yield return null;
        }

        SceneManager.LoadScene(0);
    }

    IEnumerator PlayScore()
    {
        for (int i = 0; i < 100; i++)
        {
            audioManager.main.volume -= 0.01f;
            audioManager.background.volume += 0.01f;

            yield return null;
        }

        audioManager.main.volume = 0f;
        audioManager.background.volume = 1f;
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

    void SpawnInstances() {
        if (dogPoints) {
            
            //If there is an instance of an array with points...
            int max_children = dogPoints.childCount;
            List<int> alreadyGottenValues = new List<int>();

            for (int i = 0; i < dogAmount; i++) {
                
                int random_index = Random.Range(0, max_children -1);
                if (!alreadyGottenValues.Contains(random_index))
                {
                    //print("Didnt contain!");
                    alreadyGottenValues.Add(random_index);
                    //Spawn this!
                    Instantiate(DogsPrefabs[i], dogPoints.GetChild(random_index).transform.position, Quaternion.identity);

                }
                else {//if that number has already been instantiated...
                    while (alreadyGottenValues.Contains(random_index)) {
                        random_index = Random.Range(0, max_children - 1);
                    }
                    alreadyGottenValues.Add(random_index);

                    Instantiate(DogsPrefabs[i], dogPoints.GetChild(random_index).transform.position, Quaternion.identity);

                }
            } 
        }

        Vector3[] pointsToSpawnNPC = GetRandomNPCPoints(npcAmount);

        //int j = Random.RandomRange(0, 13 - npcAmount);
        for (int i = 0; i < npcAmount; i++) {
            Instantiate(NPCsPrefabs[i], pointsToSpawnNPC[i], Quaternion.identity);
        }
    }
}
