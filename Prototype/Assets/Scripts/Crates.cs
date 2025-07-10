using System.Collections.Generic;
using UnityEngine;

public class Crates : MonoBehaviour
{
    [SerializeField] GameObject leftCrate;
    [SerializeField] GameObject rightCrate;
    [SerializeField] GameObject topCrate;
    [SerializeField] GameObject player;

    List<GameObject> crates = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
        crates.Add(leftCrate);
        crates.Add(rightCrate);
        crates.Add(topCrate);
    }

    private void UpdateZOrder(bool inFront)
    {
        if (inFront)
        {
            foreach (GameObject crate in crates)
            {
                crate.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
        }
        else
        {
            foreach (GameObject crate in crates)
            {
                crate.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
        }

        topCrate.GetComponent<SpriteRenderer>().sortingOrder += 1;
    }

    void Update()
    {
        if (player.transform.position.y > leftCrate.transform.position.y)
        {
            UpdateZOrder(true);
        }
        else
        {
            UpdateZOrder(false);
        }
    }
}
