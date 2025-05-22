using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float interval = 5f;

    private List<AlarmBehavior> availableMainAlarms = new List<AlarmBehavior>();

    void Start()
    {
        AlarmBehavior[] allAlarms = FindObjectsByType<AlarmBehavior>(FindObjectsSortMode.None);

        foreach (var alarm in allAlarms)
        {
            if (alarm != null && alarm.isMainAlarm)
            {
                availableMainAlarms.Add(alarm);
            }
        }

        StartCoroutine(ActivateRandomAlarmLoop());
    }

    private IEnumerator ActivateRandomAlarmLoop()
    {
        while (availableMainAlarms.Count > 0)
        {
            yield return new WaitForSeconds(interval);

            int index = Random.Range(0, availableMainAlarms.Count);
            AlarmBehavior selectedAlarm = availableMainAlarms[index];

            if (selectedAlarm != null)
            {
                if (!selectedAlarm.gameObject.activeSelf)
                    selectedAlarm.gameObject.SetActive(true);

                selectedAlarm.Activate();

                availableMainAlarms.RemoveAt(index);
            }
        }
    }
}
