using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 2.0f;
    public Vector3Variable RespawnPosition;

    private Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        CheckForRespawn();
    }

    private void HandleMovement() {
        Vector3 move = Vector3.zero;
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            move -= Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            move += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            move -= Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            move += Vector3.left;
        }

        if (move.sqrMagnitude < 0.01f) {
            myAnimator.SetBool("Running", false);
            return;
        }

        myAnimator.SetBool("Running", true);
        transform.position += move.normalized * MoveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(move);
    }

    private void CheckForRespawn() {
        if (transform.position.y > -10f)
            return;

        transform.position = RespawnPosition.Value;
    }
}
