using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public bool Thrown = false;

    private void OnCollisionEnter(Collision collision) {
        if (!Thrown)
            return;

        if (collision.collider.attachedRigidbody == null) {
            gameObject.layer = 0;
            Thrown = false;
        }
    }
}
