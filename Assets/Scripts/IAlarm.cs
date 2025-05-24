using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IAlarm : IPointerClickHandler
{
    public void Activate();

    public bool IsActive();

    public bool IsDeactivated();

    public bool IsInactive();
}
