using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookcaseManager : MonoBehaviour
{
    public float TimePerEject = 5f;
    public float EjectOverTimeMultiplier = 0.01f;

    [Header("Waves")]
    public List<Wave> Waves;
    public IntVariable WaveCount;
    public FloatEvent MultiplyPlayerSpeed;

    private float ejectTimer = 0f;
    private List<Bookcase> allBookCases;
    private List<Bookcase> bookCases;
    private float waveTimer = 0f;
    private int currentWave = 0;
    private float standardEject = 0f;

    private bool inWave = false;

    private void Awake() {
        allBookCases = new List<Bookcase>(GetComponentsInChildren<Bookcase>());
        bookCases = allBookCases;
        WaveCount.Value = 0;
    }

    private void Update() {
        ejectTimer += Time.deltaTime;
        TimePerEject = TimePerEject * (1f - EjectOverTimeMultiplier * Time.deltaTime);
        if(ejectTimer >= TimePerEject) {
            EjectRandomBook();
            ejectTimer = 0f;
        }

        if (inWave) {
            WaveUpdate();
            return;
        }

        if (Waves == null)
            return;

        if (currentWave >= Waves.Count)
            return;

        waveTimer += Time.deltaTime;
        if(Waves[currentWave].TimeTillWave < waveTimer) {
            StartWave(Waves[currentWave]);
        }
    }

    private void WaveUpdate() {
        waveTimer += Time.deltaTime;
        if (waveTimer >= Waves[currentWave].WaveDuration)
            EndWave();
    }

    private void StartWave(Wave wave) {
        standardEject = TimePerEject;
        TimePerEject = wave.TimePerEject;
        waveTimer = 0f;
        bookCases = wave.SelectedBookcases;
        inWave = true;
    }

    private void EndWave() {
        TimePerEject = standardEject * (1 - Waves[currentWave].PostWaveEjectMultiplier);
        waveTimer = 0f;
        bookCases = allBookCases;
        inWave = false;
        MultiplyPlayerSpeed.Raise(Waves[currentWave].PlayerSpeedMultiplier);
        currentWave++;
        WaveCount.Value = currentWave;
    }

    private void EjectRandomBook() {
        int bookCase = Random.Range(0, bookCases.Count);
        bookCases[bookCase].EjectBook();
    }
}

[System.Serializable]
public class Wave
{
    public List<Bookcase> SelectedBookcases;
    public float TimePerEject = 1f;
    public float TimeTillWave = 60f;
    public float WaveDuration = 10f;
    public float PostWaveEjectMultiplier = 0.3f;
    public float PlayerSpeedMultiplier = 0.3f;
}
