using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEvent<T> : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<IEventListener<T>> eventListeners =
        new List<IEventListener<T>>();

    public void Raise(T item) {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(item);
    }

    public void RegisterListener(IEventListener<T> gameEventListener) {
        if (!eventListeners.Contains(gameEventListener))
            eventListeners.Add(gameEventListener);
    }

    public void UnregisterListener(IEventListener<T> gameEventListener) {
        if (eventListeners.Contains(gameEventListener))
            eventListeners.Remove(gameEventListener);
    }
}
