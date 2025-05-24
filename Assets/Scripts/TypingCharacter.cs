using UnityEngine;
using UnityEngine.UI;

public class TypingCharacter : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Image background;

    private Color onColor = Color.green;
    private Color offColor = Color.white;

    [SerializeField] private bool randomChar = true;
    [SerializeField] private char c;
    private bool on = false;

    private void Start()
    {
        if (randomChar)
        {
            c = GenerateRandomChar();
        }
        UpdateUI();


    }

    private void Update()
    {
        KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), c.ToString().ToUpper());
        if (Input.GetKeyDown(key))
        {
            on = true;
            UpdateUI();
        }
    }

    private char GenerateRandomChar()
    {
        string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return characters[Random.Range(0, characters.Length)];
    }

    public bool IsOn()
    {
        return on;
    }

    private void UpdateUI()
    {
        if (on)
        {
            background.color = onColor;
        }
        else
        {
            background.color = offColor;
        }
        text.text = "" + c;
        
    }
}
