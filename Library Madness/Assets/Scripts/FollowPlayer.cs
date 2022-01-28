using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    public float Speed = 4f;

    private Vector3 relativePosition;

    // Start is called before the first frame update
    void Start()
    {
        relativePosition = transform.position - Player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Player.position + relativePosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
    }
}
