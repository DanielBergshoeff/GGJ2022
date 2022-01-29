using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Book : MonoBehaviour
{
    public bool Thrown = false;
    public bool Stored = false;
    public bool PlayerThrown = false;

    public Vector3Variable SpawnPosition;

    private NavMeshAgent myNavMeshAgent;
    private Rigidbody myRigidbody;
    private Collider myCollider;
    private Vector3 targetPosition;

    private float timer = 0f;

    private void Awake() {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponentInChildren<Collider>();
    }
    private void Start() {
        if(myNavMeshAgent.enabled)
            SetRandomDestination();
    }

    private void Update() {
        if (transform.position.y < -10f)
            Respawn();

        if (myNavMeshAgent == null || myNavMeshAgent.enabled == false)
            return;

        timer += Time.deltaTime;
        if ((targetPosition - transform.position).sqrMagnitude < 0.1f || timer > 5f)
            SetRandomDestination();
    }

    private void Respawn() {
        transform.position = SpawnPosition.Value;
    }

    public void PickUp() {
        myNavMeshAgent.enabled = false;
        myRigidbody.isKinematic = true;
        myCollider.enabled = false;
    }

    public void Drop() {
        myRigidbody.isKinematic = false;
        myCollider.enabled = true;
        gameObject.layer = 8;
        transform.GetChild(0).gameObject.layer = 8;
        Thrown = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.CompareTag("Floor")) {
            if (myNavMeshAgent.enabled == false)
                myNavMeshAgent.enabled = true;
        }

        if (!Thrown)
            return;

        if (collision.collider.attachedRigidbody == null) {
            gameObject.layer = 0;
            transform.GetChild(0).gameObject.layer = 0;
            Thrown = false;
        }
    }

    private void SetRandomDestination() {
        Vector3 randomDirection = Random.insideUnitSphere * 20f;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(randomDirection, out hit, 20f, 1))
            return;
        
        targetPosition = hit.position;
        myNavMeshAgent.SetDestination(targetPosition);
        timer = 0f;
    }
}
