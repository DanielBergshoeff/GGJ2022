using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChaosUI : MonoBehaviour
{
    [Header("Chaos")]
    public FloatVariable Chaos;
    public Slider ChaosSlider;

    [Header("Scores")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighscoreText;

    [Header("Waves")]
    public IntVariable WaveCount;
    public TextMeshProUGUI WaveText;

    private float timer = 0f;
    private static float localHighscore = 0f;

    private void Awake() {
        HighscoreText.text = localHighscore.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        ChaosSlider.value = Chaos.Value;
        ScoreText.text = timer.ToString("F2");
        WaveText.text = (WaveCount.Value + 1).ToString();
    }

    private void OnDestroy() {
        if (timer > localHighscore)
            localHighscore = timer;
    }
}
