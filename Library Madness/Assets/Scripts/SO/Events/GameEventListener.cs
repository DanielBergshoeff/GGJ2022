using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class GameEventListener<T, E, UER> : MonoBehaviour, IEventListener<T>
    where E : GameEvent<T>
    where UER : UnityEvent<T>
{
    public E Event;

    public UER Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(T item) {
        Response.Invoke(item);
    }
}
