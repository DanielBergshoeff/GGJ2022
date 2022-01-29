using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public int MaxEscapedBooks;
    
    public static int EscapedBooks;

    public FloatVariable Chaos;

    private void Update() {
        Chaos.Value = (float)EscapedBooks / MaxEscapedBooks;
        if (Chaos.Value >= 1f) {
            EscapedBooks = 0;
            InGameMenu.RestartGame();
        }
    }
}
