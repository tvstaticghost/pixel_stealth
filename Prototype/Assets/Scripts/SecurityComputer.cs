using UnityEngine;

public class SecurityComputer : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Player>().canActivateComputer = true;
            Debug.Log("Player entered the computer area");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Player>().canActivateComputer = false;
            Debug.Log("Player left the computer area");
        }
    }
}
