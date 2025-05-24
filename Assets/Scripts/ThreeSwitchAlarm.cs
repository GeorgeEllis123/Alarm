using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ThreeSwitchAlarm : MonoBehaviour, IAlarm
{
    [SerializeField] private SwitchBehavior leftSwitch;
    [SerializeField] private SwitchBehavior middleSwitch;
    [SerializeField] private SwitchBehavior rightSwitch;

    // each index corrisponds to a switch (0=left, 1=mid, 2=right) and
    // the number at that index is what switch it flips (-1=none)
    [SerializeField] private int[] pattern;

    private enum State { Active, Deactivated, Inactive }

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
        if (state == State.Deactivated || state == State.Inactive)
            return;

        if (AllSwitchesOn())
        {
            state = State.Deactivated;
            UpdateColor();
            leftSwitch.gameObject.SetActive(false);
            middleSwitch.gameObject.SetActive(false);
            rightSwitch.gameObject.SetActive(false);
            audioSource.Stop();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (state == State.Active)
        {
            leftSwitch.gameObject.SetActive(true);
            middleSwitch.gameObject.SetActive(true);
            rightSwitch.gameObject.SetActive(true);
        }
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

    private bool AllSwitchesOn()
    {
        return (leftSwitch.IsOn() && rightSwitch.IsOn() && middleSwitch.IsOn());
    }

    public void Flipped(SwitchBehavior s)
    {
        if (s.Equals(leftSwitch))
        {
            Debug.Log("left flipped");
            switch (pattern[0])
            {
                case 1:
                    Debug.Log("causing middle to flip");
                    middleSwitch.Toggle();
                    break;
                case 2:
                    Debug.Log("causing right to flip");
                    rightSwitch.Toggle();
                    break;
                case -1:
                    break;
                default:
                    Debug.LogWarning("invalid switch connection selected for left switch");
                    break;
            }
        }

        else if (s.Equals(middleSwitch))
        {
            Debug.Log("middle flipped");
            switch (pattern[1])
            {
                case 0:
                    Debug.Log("causing left to flip");
                    leftSwitch.Toggle();
                    break;
                case 2:
                    Debug.Log("causing right to flip");
                    rightSwitch.Toggle();
                    break;
                case -1:
                    break;
                default:
                    Debug.LogWarning("invalid switch connection selected for middle switch");
                    break;
            }
        }

        else if (s.Equals(rightSwitch))
        {
            Debug.Log("right flipped");
            switch (pattern[2])
            {
                case 0:
                    Debug.Log("causing left to flip");
                    leftSwitch.Toggle();
                    break;
                case 1:
                    Debug.Log("causing middle to flip");
                    middleSwitch.Toggle();
                    break;
                case -1:
                    break;
                default:
                    Debug.LogWarning("invalid switch connection selected for right switch");
                    break;
            }
        }
    }
}
