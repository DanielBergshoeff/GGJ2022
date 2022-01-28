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

        Book b = collision.collider.GetComponent<Book>();
        if (b == null || b.Thrown)
            return;

        b.transform.parent = BookParent;
        b.transform.localPosition = transform.up * 0.1f * Books.Count;
        b.GetComponent<Rigidbody>().isKinematic = true;
        b.GetComponent<Collider>().enabled = false;
        Books.Add(b);
    }

    private void CheckForThrow() {
        if (!throwing)
            return;

        if (Input.GetMouseButtonUp(0)) {
            Vector3 throwDir = Input.mousePosition - throwStart;
            throwDir = new Vector3(throwDir.x * -1f, throwDir.y, throwDir.z);
            ThrowBook(throwDir.normalized);
            throwing = false;
        }
    }

    private void ThrowBook(Vector3 direction) {
        Book b = Books[Books.Count - 1];
        b.GetComponent<Rigidbody>().isKinematic = false;
        b.GetComponent<Collider>().enabled = true;
        b.transform.parent = null;
        b.GetComponent<Rigidbody>().AddForce(direction * 500f);
        b.Thrown = true;
        b.gameObject.layer = 8;
        Books.Remove(b);
    }

    private void OnMouseDown() {
        if (Books.Count == 0)
            return;

        throwing = true;
        throwStart = Input.mousePosition;
    }
}
