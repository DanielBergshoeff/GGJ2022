using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaosUI : MonoBehaviour
{
    public FloatVariable Chaos;
    public Slider ChaosSlider;

    // Update is called once per frame
    void Update()
    {
        ChaosSlider.value = Chaos.Value;
    }
}
