using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float interval = 2f;
    [SerializeField] private List<GameObject> alarmObjects; // can't figure out how to serialize IAlarm
    [SerializeField] private float timeToWin = 6f;

    private List<IAlarm> alarms = new List<IAlarm>();
    private int currentIndex = 0;
    private float spawnTimer = 0f;
    private float winTimer = 0f;

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
    }

    void Update()
    {
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
        if (timeToWin >= winTimer)
        {
            Debug.Log("Win!");
        }
        else
        {
            Debug.Log("Lose!");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
