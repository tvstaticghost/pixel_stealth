using System.Security.Cryptography;
using UnityEngine;

public class TestMonster : Monster
{
    [SerializeField] CircleCollider2D detectionRadius;
    [SerializeField] GameObject questionMarkSprite;
    private void Start()
    {
        Debug.Log("Test Monster Instantiated");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isFlashed)
            {
                questionMarkSprite.SetActive(true);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            questionMarkSprite.SetActive(false);
        }
    }
}