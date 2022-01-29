using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookcaseManager : MonoBehaviour
{
    public float TimePerEject = 5f;

    private float ejectTimer = 0f;
    private List<Bookcase> bookCases;

    private void Awake() {
        bookCases = new List<Bookcase>(GetComponentsInChildren<Bookcase>());
    }

    private void Update() {
        ejectTimer += Time.deltaTime;
        if(ejectTimer >= TimePerEject) {
            EjectRandomBook();
            ejectTimer = 0f;
        }
    }

    private void EjectRandomBook() {
        int bookCase = Random.Range(0, bookCases.Count);
        bookCases[bookCase].EjectBook();
    }
}
