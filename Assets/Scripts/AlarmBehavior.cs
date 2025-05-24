using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlarmBehavior : MonoBehaviour, IAlarm
{
    [SerializeField] private AlarmBehavior leftAlarm;
    [SerializeField] private AlarmBehavior rightAlarm;

    private enum State { Active, Deactivated, Inactive }

    public bool isMainAlarm;
    public bool completed = false;

    private Color activeColor = Color.red;
    private Color deactivatedColor = Color.green;
    private Color inactiveColor = Color.white;

    private State state = State.Inactive;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (audioSource != null && completed)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        if (completed && !isMainAlarm && state != State.Deactivated)
        {
            gameObject.SetActive(false);
        }

        if (leftAlarm != null && rightAlarm != null)
        {
            completed = leftAlarm.completed && rightAlarm.completed;
        }
    }

    public void Activate()
    {
        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
        }
        state = State.Active;
        UpdateColor();
    }

    private void UpdateColor()
    {
        switch (state)
        {
            case State.Active:
                GetComponent<Image>().color = activeColor;
                break;
            case State.Deactivated:
                GetComponent<Image>().color = deactivatedColor;
                break;
            case State.Inactive:
                GetComponent<Image>().color = inactiveColor;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (state == State.Active)
        {
            if (leftAlarm != null && rightAlarm != null)
            {
                leftAlarm.gameObject.SetActive(true);
                rightAlarm.gameObject.SetActive(true);
                leftAlarm.Activate();
                rightAlarm.Activate();
                state = State.Deactivated;
                UpdateColor();
            }
            else
            {
                completed = true;
            }
        }
    }
}
