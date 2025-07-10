using System.Collections.Generic;
using UnityEngine;

public class CrateBlur : MonoBehaviour
{
    [SerializeField] GameObject leftCrate;
    [SerializeField] GameObject rightCrate;
    [SerializeField] GameObject topCrate;
    [SerializeField] GameObject player;
    List<GameObject> crates = new List<GameObject>();
    private Color originalColor;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        crates.Add(leftCrate);
        crates.Add(rightCrate);
        crates.Add(topCrate);

        originalColor = leftCrate.GetComponent<SpriteRenderer>().color;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Entered");
            foreach (GameObject crate in crates)
            {
                SpriteRenderer sr = crate.GetComponent<SpriteRenderer>();
                Color color = sr.color;
                color.a = 0.7f;
                sr.color = color;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject crate in crates)
            {
                SpriteRenderer sr = crate.GetComponent<SpriteRenderer>();
                sr.color = originalColor;
            }
        }
    }
}
