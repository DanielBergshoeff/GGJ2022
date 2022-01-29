using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : Menu
{
    public GameObject Interface;
    private static InGameMenu Instance;

    private void Awake() {
        Instance = this;
    }

    public static void RestartGame() {
        Instance.StartGame();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Interface.SetActive(!Interface.activeSelf);
        }
    }
}
