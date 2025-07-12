using UnityEngine;

public class Barrel : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer barrelSprite;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        barrelSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            barrelSprite.sortingOrder = 2;
        }
        else
        {
            barrelSprite.sortingOrder = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered barrel jump zone");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited barrel jump zone");
        }
    }
}
