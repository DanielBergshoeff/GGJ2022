using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : Menu
{
    public GameObject Interface;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Interface.SetActive(!Interface.activeSelf);
        }
    }
}
