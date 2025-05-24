using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices.WindowsRuntime;

public abstract class AAlarm : MonoBehaviour, IAlarm, IPointerClickHandler
{
    protected enum State { Active, Deactivated, Inactive }

    [SerializeField] protected Color activeColor = Color.red;
    [SerializeField] protected Color deactivatedColor = Color.green;
    [SerializeField] protected Color inactiveColor = Color.white;

    protected State state = State.Inactive;
    protected AudioSource audioSource;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (state == State.Active && CheckCompletion())
        {
            state = State.Deactivated;
            UpdateColor();
            OnDeactivated();
        }
    }

    public virtual void Activate()
    {
        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
        }
        state = State.Active;
        UpdateColor();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (state == State.Active)
        {
            OnActivatedClick();
        }
    }

    protected virtual void UpdateColor()
    {
        Image img = GetComponent<Image>();
        if (img != null)
        {
            switch (state)
            {
                case State.Active: 
                    img.color = activeColor; 
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

    public bool IsActive()
    {
        return state == State.Active;
    }

    public bool IsDeactivated()
    {
        return state == State.Deactivated;
    }

    public bool IsInactive()
    {
        return state == State.Inactive;
    }

    protected abstract bool CheckCompletion();
    protected abstract void OnDeactivated();
    protected abstract void OnActivatedClick();

}