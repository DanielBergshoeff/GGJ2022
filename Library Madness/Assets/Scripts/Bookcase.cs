using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookcase : MonoBehaviour
{
    public GameObject BookPrefab;
    public Transform SectionsParent;

    private Book[,] bookSpots;
    private List<Transform> Sections;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            EjectBook();
        }
    }

    private void Awake() {
        Sections = new List<Transform>();
        for (int i = 0; i < SectionsParent.childCount; i++) {
            Sections.Add(SectionsParent.GetChild(i));
        }

        bookSpots = new Book[Sections.Count,8];
        for (int i = 0; i < Sections.Count; i++) {
            for (int j = 0; j < 8; j++) {
                GameObject go = Instantiate(BookPrefab, Sections[i]);
                go.transform.position = Sections[i].position + transform.right * -0.1f * j + Vector3.up * 0.225f;
                bookSpots[i, j] = go.GetComponent<Book>();
                go.GetComponent<Book>().Stored = true;
            }
        }
    }

    public void EjectBook() {
        int section = Random.Range(0, Sections.Count);
        int number = Random.Range(0, 8);
        Book bookToEject = bookSpots[section, number];

        if (!bookToEject.Stored)
            return;

        bookToEject.Stored = false;
        bookToEject.Drop();
        bookToEject.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(300f, 600f));
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.collider.CompareTag("Book"))
            return;

        if (collision.collider.attachedRigidbody == null)
            return;

        Book b = collision.collider.attachedRigidbody.GetComponent<Book>();

        if (!b.PlayerThrown)
            return;

        for (int i = 0; i < Sections.Count; i++) {
            for (int j = 0; j < 8; j++) {
                if (bookSpots[i, j] == b) {
                    b.transform.position = Sections[i].position + transform.right * -0.1f * j + Vector3.up * 0.225f;
                    b.transform.rotation = BookPrefab.transform.rotation;
                    b.PickUp();
                    b.Stored = true;
                    b.PlayerThrown = false;
                }
            }
        }
    }
}
