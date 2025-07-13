using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashBang : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    private Vector3 direction;
    private ObjectManager objectManager;
    private float secondsToDetonate = 2f;
    void Start()
    {
        objectManager = GameObject.FindGameObjectWithTag("ObjectManager").GetComponent<ObjectManager>();
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0;
        direction = (mouseWorldPosition - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * movementSpeed;

        StartCoroutine(FlashBangDetonateTimer());
    }

    IEnumerator FlashBangDetonateTimer()
    {
        yield return new WaitForSeconds(secondsToDetonate);
        Destroy(gameObject);
        objectManager.SpawnFlash(transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
