using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHolder : MonoBehaviour
{
    public List<Book> Books;
    public Transform BookParent;

    // Start is called before the first frame update
    void Start()
    {
        Books = new List<Book>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.collider.CompareTag("Book"))
            return;

        Book b = collision.collider.GetComponent<Book>();
        if (b == null)
            return;

        b.transform.parent = BookParent;
        b.transform.localPosition = transform.up * 0.1f * Books.Count;
        b.GetComponent<Rigidbody>().isKinematic = true;
        Books.Add(b);
    }
}
