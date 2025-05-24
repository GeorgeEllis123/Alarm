using UnityEngine;

public class BasicAlarm : AAlarm
{
    private bool completed = false;
    protected override bool CheckCompletion()
    {
        return completed;
    }

    protected override void OnActivatedClick()
    {
        completed = true;
    }

    protected override void OnDeactivated()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}
