using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class SwitchBehavior : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform up; // on
    [SerializeField] private Transform down; // off
    [SerializeField] private Transform slider; 
    [SerializeField] private Image background;

    private Color onColor = Color.green;
    private Color offColor = Color.white;

    private bool on = false;

    private ThreeSwitchAlarm parentAlarm;

    private void Start()
    {
        parentAlarm = GetComponentInParent<ThreeSwitchAlarm>();
        UpdateUI();
    }

    public void Toggle()
    {
        Debug.Log(gameObject.name + " flipped");
        on = !on;  
        UpdateUI();
        parentAlarm.Flipped(this);
    }

    public bool IsOn()
    {
        return on;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    private void UpdateUI()
    {
        StopAllCoroutines();
        if (on)
        {
            StartCoroutine(AnimateSlider(up.position));
        }
        else
        {
            StartCoroutine(AnimateSlider(down.position));
        }
    }

    private IEnumerator AnimateSlider(Vector3 targetPosition)
    {
        Debug.Log(gameObject.name);

        float duration = 0.1f;
        float elapsed = 0f;
        Vector3 startingPos = slider.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            slider.position = Vector3.Lerp(startingPos, targetPosition, t);
            yield return null;
        }

        slider.position = targetPosition;
        if (on)
        {
            background.color = onColor;
        }
        else
        {
            background.color = offColor;
        }
        
    }
}
