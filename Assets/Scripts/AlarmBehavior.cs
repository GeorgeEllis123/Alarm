using UnityEngine;
using UnityEngine.UI;

public class AlarmBehavior : AAlarm
{
    [SerializeField] private AlarmBehavior leftAlarm;
    [SerializeField] private AlarmBehavior rightAlarm;

    public bool isMainAlarm;
    public bool completed = false;
    private bool fakeDeactivate = false;

    protected override bool CheckCompletion()
    {
        if (leftAlarm && rightAlarm)
        {
            completed = leftAlarm.completed && rightAlarm.completed;
        }

        return completed;
    }

    protected override void OnDeactivated()
    {
        Debug.Log("deactivated " + gameObject.name);
        if (leftAlarm && rightAlarm)
        {
            leftAlarm.gameObject.SetActive(false);
            rightAlarm.gameObject.SetActive(false);
        }
        if (audioSource != null)
            audioSource.Stop();
    }

    protected override void OnActivatedClick()
    {
        if (leftAlarm && rightAlarm)
        {
            leftAlarm.gameObject.SetActive(true);
            rightAlarm.gameObject.SetActive(true);
            leftAlarm.Activate();
            rightAlarm.Activate();
            fakeDeactivate = true;
            UpdateColor();
        }
        else
        {
            completed = true;
        }
    }

    protected override void UpdateColor()
    {
        Image img = GetComponent<Image>();
        if (img != null)
        {
            switch (state)
            {
                case State.Active:
                    if (fakeDeactivate)
                    {
                        img.color = deactivatedColor;
                    }
                    else
                    {
                        img.color = activeColor;
                    }
                    break;
                case State.Deactivated:
                    img.color = deactivatedColor;
                    break;
                case State.Inactive:
                    img.color = inactiveColor;
                    break;
            }
        }
    }
}
