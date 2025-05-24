using UnityEngine;

public class ThreeSwitchAlarm : AlarmBase
{
    [SerializeField] private SwitchBehavior leftSwitch;
    [SerializeField] private SwitchBehavior middleSwitch;
    [SerializeField] private SwitchBehavior rightSwitch;
    [SerializeField] private int[] pattern;

    protected override bool CheckCompletion()
    {
        return leftSwitch.IsOn() && middleSwitch.IsOn() && rightSwitch.IsOn();
    }

    protected override void OnDeactivated()
    {
        leftSwitch.gameObject.SetActive(false);
        middleSwitch.gameObject.SetActive(false);
        rightSwitch.gameObject.SetActive(false);
        audioSource?.Stop();
    }

    protected override void OnActivatedClick()
    {
        leftSwitch.gameObject.SetActive(true);
        middleSwitch.gameObject.SetActive(true);
        rightSwitch.gameObject.SetActive(true);
    }

    public void Flipped(SwitchBehavior s)
    {
        int index = s == leftSwitch ? 0 : s == middleSwitch ? 1 : 2;
        int target = pattern[index];

        if (target == -1) return;

        var targets = new[] { leftSwitch, middleSwitch, rightSwitch };
        if (target >= 0 && target < targets.Length)
        {
            targets[target].Toggle();
        }
        else
        {
            Debug.LogWarning($"Invalid switch pattern index: {target}");
        }
    }
}
