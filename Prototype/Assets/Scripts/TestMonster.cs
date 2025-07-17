using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class TestMonster : Monster
{
    [SerializeField] CircleCollider2D detectionRadius;
    [SerializeField] GameObject questionMarkSprite;

    System.Random rand = new System.Random();
    private Vector3 movementDirection;
    private float movementTime = 2;

    private float movementSpeed = 0.01f;
    private bool unaware = true;
    private bool isWaiting = false;
    private void Start()
    {
        PickDirection();
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

    private void FixedUpdate()
    {
        if (isWaiting)
        {
            return;
        }

        if (unaware)
        {
            movementTime -= Time.deltaTime;

            if (movementTime <= 0)
            {
                StartCoroutine(WalkWaitTimer());
            }
            
            transform.position += movementDirection * (float)movementSpeed;
        }
    }

    private void PickDirection()
    {
        int randomNumber = rand.Next(2);

        if (randomNumber == 0)
        {
            movementDirection = Vector3.left;
        }
        else
        {
            movementDirection = Vector3.right;
        }

        movementTime = (float)rand.Next(1, 4);
    }

    IEnumerator WalkWaitTimer()
    {
        isWaiting = true;
        int waitTime = rand.Next(2, 4);
        movementDirection = Vector3.zero;

        yield return new WaitForSeconds(waitTime);
        PickDirection();
        isWaiting = false;
    }

    //Add monster aggression logic and flash bang stunning logic.
}