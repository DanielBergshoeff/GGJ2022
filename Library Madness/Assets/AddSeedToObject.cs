using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSeedToObject : MonoBehaviour
{
    float seed = 0;
    //public List<GameObject> booksInBookcase;
    private Material bookMat;

    void Start()
    {
        SetSeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSeed()
    {
        seed = Random.Range(0f, 15f);
        //booksInBookcase.Clear();
        bookMat = this.GetComponent<Renderer>().material;
        bookMat.SetFloat("_DisplacementSeed", seed);

        foreach (Transform child in transform)
         {
             if (child.tag == "Book")
             {
                bookMat = child.GetComponent<Renderer>().material;
                bookMat.SetFloat("_DisplacementSeed", seed);
                //booksInBookcase.Add(child.gameObject);
             }
         }
    }
}





