using UnityEngine;

public class ShelfBlur : MonoBehaviour
{
    private GameObject player;
    [SerializeField] SpriteRenderer shelfSprite;
    private Color originalColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        originalColor = shelfSprite.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Color color = shelfSprite.color;
            color.a = 0.7f;
            shelfSprite.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shelfSprite.color = originalColor;
        }
    }
}
