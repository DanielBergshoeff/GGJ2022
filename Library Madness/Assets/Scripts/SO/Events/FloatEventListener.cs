using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatEventListener : GameEventListener<float, FloatEvent, UnityFloatEvent>
{

}

[System.Serializable]
public class UnityFloatEvent : UnityEvent<float> { }
