using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float interval = 2f;
    [SerializeField] private List<GameObject> alarmObjects; // can't figure out how to serialize IAlarm
    [SerializeField] private float timeToWin = 6f;
    [SerializeField] private float endDelay = 2f;

    private List<IAlarm> alarms = new List<IAlarm>();
    private int currentIndex = 0;
    private float spawnTimer = 0f;
    private float winTimer = 0f;
    private bool gameEnded = false;

    [SerializeField] private TextMeshProUGUI endingText;

    private bool textShown = false;


    void Start()
    {
        foreach (GameObject obj in alarmObjects)
        {
            IAlarm alarm = obj.GetComponent<IAlarm>();
            if (alarm != null)
            {
                alarms.Add(alarm);
            }
        }

        GameObject endingObj = GameObject.FindGameObjectWithTag("Ending");
        if (endingObj != null)
            endingText = endingObj.GetComponent<TextMeshProUGUI>();

        if (endingText != null)
            endingText.gameObject.SetActive(false); // Hide at start
    }

    void Update()
    {
        if (gameEnded)
            return;

        if (!textShown)
            winTimer += Time.deltaTime;

        if (AllAlarmsDeactivated())
            Win();

        if (currentIndex >= alarms.Count)
            return;

        if (AnyAlarmIsActive())
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= interval)
            {
                alarms[currentIndex].Activate();
                currentIndex++;
                spawnTimer = 0f;
            }
        }
        else
        {
            alarms[currentIndex].Activate();
            currentIndex++;
            spawnTimer = 0f;
        }
    }

    private bool AnyAlarmIsActive()
    {
        for (int i = 0; i < alarms.Count; i++)
        {
            if (alarms[i].IsActive())
                return true;
        }
        return false;
    }

    private bool AllAlarmsDeactivated()
    {
        for (int i = 0; i < alarms.Count; i++)
        {
            if (!alarms[i].IsDeactivated())
                return false;
        }
        return true;
    }

    private void Win()
    {
        if (endingText != null)
        {
            endingText.gameObject.SetActive(true);
            if (timeToWin >= winTimer)
            {
                endingText.text = "You Woke Up In Time!";
                Debug.Log("Win!");
            }
            else
            {
                endingText.text = "You Overslept!"; 
                Debug.Log("Lose!");
            }
        }
        textShown = true;
        Invoke(nameof(ReloadScene), endDelay); // Wait before reloading
    }

    private void ReloadScene()
    {
        textShown = false;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (endingText.text == "You Overslept!")
        {
            SceneManager.LoadScene(currentIndex);
        }
        else if (currentIndex < 3)
        {
            SceneManager.LoadScene(currentIndex + 1); // Load next scene
        }
        else
        {
            Debug.Log("Last scene reached. Staying on current scene.");
            
                endingText.text = "You won!";
                endingText.gameObject.SetActive(true);
                textShown = true;
        }
    }
}
