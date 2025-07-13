using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class FlashBang : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] CircleCollider2D blastRadius;
    private Vector3 direction;
    private ObjectManager objectManager;
    private Player playerScript;
    private float secondsToDetonate = 2f;
    void Start()
    {
        objectManager = GameObject.FindGameObjectWithTag("ObjectManager").GetComponent<ObjectManager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0;
        direction = (mouseWorldPosition - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * movementSpeed;

        StartCoroutine(FlashBangDetonateTimer());
    }

    private void CheckForBlastCollisions()
    {
        Vector2 center = blastRadius.bounds.center;
        float radius = blastRadius.radius * blastRadius.transform.lossyScale.x;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.name == "Player")
            {
                Debug.Log($"HIT PLAYER!");
                playerScript.FlashBanged();
            }

            if (hit.CompareTag("Monster"))
            {
                Debug.Log("Hit a monster");
                Monster monster = hit.GetComponent<Monster>();
                if (monster == null) 
                { 
                    Debug.Log("Monster is null"); 
                }
                else
                {
                    Debug.Log($"Monster is real: {monster}");
                }
                StartCoroutine(monster.FlashWhiteOverlay());
            }
        }
    }

    IEnumerator FlashBangDetonateTimer()
    {
        yield return new WaitForSeconds(secondsToDetonate);
        //Check if anything is in the
        CheckForBlastCollisions();
        Destroy(gameObject);
        objectManager.SpawnFlash(transform.position, transform.rotation);
    }
}
