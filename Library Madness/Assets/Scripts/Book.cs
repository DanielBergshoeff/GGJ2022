using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Book : MonoBehaviour
{
    public bool Thrown = false;

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
        SetRandomDestination();
    }

    private void Update() {
        if (myNavMeshAgent == null || myNavMeshAgent.enabled == false)
            return;

        timer += Time.deltaTime;
        if ((targetPosition - transform.position).sqrMagnitude < 0.1f || timer > 5f)
            SetRandomDestination();
    }

    public void PickUp() {
        myNavMeshAgent.enabled = false;
        myRigidbody.isKinematic = true;
        myCollider.enabled = false;
    }

    public void Drop() {
        myRigidbody.isKinematic = false;
        myCollider.enabled = true;
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
