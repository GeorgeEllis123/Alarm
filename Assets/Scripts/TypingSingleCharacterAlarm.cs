using UnityEngine;

public class TypingSingleCharacterAlarm : AAlarm
{
    [SerializeField] private TypingCharacter character;

    protected override bool CheckCompletion()
    {
        return character.IsOn();
    }

    protected override void OnDeactivated()
    {
        character.gameObject.SetActive(false);
        if (audioSource != null)
            audioSource.Stop();
    }

    protected override void OnActivatedClick()
    {
        character.gameObject.SetActive(true);
    }
}
