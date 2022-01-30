using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookcase : MonoBehaviour
{
    public GameObject BookPrefab;
    public Transform SectionsParent;

    public List<AudioClip> EjectSounds;
    public List<AudioClip> InjectSounds;

    private Book[,] bookSpots;
    private List<Transform> Sections;
    private AudioSource myAudioSource;
    private float displacementSeed = 0f;
    private Material myMaterial;
    private int booksPerRow = 7;

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

        myMaterial = GetComponent<Renderer>().material;
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        displacementSeed = Random.Range(0f, 15f);
        myMaterial.SetFloat("_DisplacementSeed", displacementSeed);

        bookSpots = new Book[Sections.Count, booksPerRow];
        for (int i = 0; i < Sections.Count; i++) {
            for (int j = 0; j < booksPerRow; j++) {
                GameObject go = Instantiate(BookPrefab, Sections[i]);
                go.transform.position = Sections[i].position + transform.right * -0.1f * j + Vector3.up * 0.15f;
                bookSpots[i, j] = go.GetComponent<Book>();
                go.GetComponent<Book>().Stored = true;
                go.GetComponent<Book>().SetBookMaterial(displacementSeed);
            }
        }
    }

    public void EjectBook() {
        int section = Random.Range(0, Sections.Count);
        int number = Random.Range(0, booksPerRow);
        Book bookToEject = bookSpots[section, number];

        if (!bookToEject.Stored)
            return;

        BookManager.EscapedBooks++;
        bookToEject.Stored = false;
        bookToEject.Drop();
        bookToEject.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(300f, 600f));
        bookToEject.StopBookMaterial();

        bookToEject.transform.Rotate(0f, 0f, -90f);
        bookToEject.transform.GetChild(0).Rotate(0f, 0f, 90f);

        myAudioSource.PlayOneShot(EjectSounds[Random.Range(0, EjectSounds.Count)]);
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
            for (int j = 0; j < booksPerRow; j++) {
                if (bookSpots[i, j] == b) {
                    b.transform.parent = Sections[i];
                    b.transform.position = Sections[i].position + transform.right * -0.1f * j + Vector3.up * 0.15f;
                    b.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    b.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
                    b.PickUp();
                    b.Stored = true;
                    b.PlayerThrown = false;
                    b.SetBookMaterial(displacementSeed);


                    BookManager.EscapedBooks--;
                    myAudioSource.PlayOneShot(InjectSounds[Random.Range(0, InjectSounds.Count)]);
                }
            }
        }
    }
}
