using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHolder : MonoBehaviour
{
    public List<Book> Books;
    public Transform BookParent;

    private bool throwing = false;
    private Vector3 throwStart;

    // Start is called before the first frame update
    void Start()
    {
        Books = new List<Book>();
    }

    private void Update() {
        CheckForThrow();
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.collider.CompareTag("Book"))
            return;

        if (collision.collider.attachedRigidbody == null)
            return;

        Book b = collision.collider.attachedRigidbody.GetComponent<Book>();
        if (b == null || b.Thrown || b.Stored)
            return;

        b.transform.parent = BookParent;
        b.transform.localPosition = transform.up * 0.1f * Books.Count;
        b.transform.rotation = Quaternion.Euler(0f, b.transform.rotation.eulerAngles.y, 0f);
        b.PickUp();
        Books.Add(b);
    }

    private void CheckForThrow() {
        if (!throwing)
            return;

        if (Input.GetMouseButtonUp(0)) {
            Vector3 throwDir = Input.mousePosition - throwStart;
            throwDir = new Vector3(throwDir.x * -1f, throwDir.z, -throwDir.y);
            ThrowBook(throwDir.normalized);
            throwing = false;
        }
    }

    private void ThrowBook(Vector3 direction) {
        Book b = Books[Books.Count - 1];
        b.Drop();
        b.PlayerThrown = true;
        b.transform.parent = null;
        b.GetComponent<Rigidbody>().AddForce(direction * 500f);
        Books.Remove(b);
    }

    private void OnMouseDown() {
        if (Books.Count == 0)
            return;

        throwing = true;
        throwStart = Input.mousePosition;
    }
}
