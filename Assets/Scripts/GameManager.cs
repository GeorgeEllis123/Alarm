using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float interval = 5f;
    [SerializeField] private List<GameObject> alarmObjects; // can't figure out how to serialize IAlarm

    private List<IAlarm> alarms = new List<IAlarm>();

    void Start()
    {
        foreach (GameObject alarm in alarmObjects)
        {
            alarms.Add(alarm.GetComponent<IAlarm>());
        }

        StartCoroutine(ActivateSequentialAlarmLoop());
    }

    private IEnumerator ActivateSequentialAlarmLoop()
    {
        foreach (IAlarm alarm in alarms)
        {
            yield return new WaitForSeconds(interval);
            alarm.Activate();
        }
    }
}
