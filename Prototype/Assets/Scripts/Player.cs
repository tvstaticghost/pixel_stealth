using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    enum Direction { DOWN, UP, LEFT, RIGHT }

    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] float grenadeCooldown;
    [SerializeField] GameObject rightGrenadeSpawnPoint;
    [SerializeField] GameObject leftGrenadeSpawnPoint;
    [SerializeField] GameObject upGrenadeSpawnPoint;
    [SerializeField] GameObject downGrenadeSpawnPoint;
    [SerializeField] GameObject flashBang;
    [SerializeField] GameObject flashBangCanvas;
    [SerializeField] Camera mainCamera;

    private Vector2 movementInput = Vector2.zero;
    private Direction direction = Direction.DOWN;
    public bool canActivateComputer = false;
    public bool canMove = true;
    public bool canThrow = true;
    public bool isThrowing = false;

    public void Move(InputAction.CallbackContext context)
    {
        if (canMove && !isThrowing)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }

    public void UseComputer(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (canActivateComputer)
            {
                movementInput = Vector2.zero;
                canMove = false;
                rb.linearVelocity = Vector2.zero;
                levelLoader.CameraCrossfade();
            }
        }
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if (context.canceled && canThrow)
        {
            Vector2 mousePos = context.ReadValue<Vector2>();
            Debug.Log("mouse position: " + Camera.main.ScreenToWorldPoint(mousePos));
            Debug.Log("Player position: " + transform.position);
            canThrow = false;
            isThrowing = true;
            StartCoroutine(ThrowFlashBangAtRightTime());
            StartCoroutine(GrenadeCooldown());
            StartCoroutine(ThrowAnimationTimer());
        }
    }

    IEnumerator GrenadeCooldown()
    {
        yield return new WaitForSeconds(grenadeCooldown);
        canThrow = true;
    }

    IEnumerator ThrowAnimationTimer()
    {
        yield return new WaitForSeconds(0.35f);
        isThrowing = false;
    }

    IEnumerator ThrowFlashBangAtRightTime()
    {
        yield return new WaitForSeconds(0.21f);
        ThrowFlashBang();
    }

    void FixedUpdate()
    {
        if (canMove && !isThrowing)
        {
            rb.linearVelocity = movementInput * movementSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (movementInput != Vector2.zero && rb.linearVelocity != Vector2.zero)
        {
            if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
            {
                if (movementInput.x > 0)
                {
                    direction = Direction.RIGHT;
                    animator.Play("Walk_Right");
                }
                else
                {
                    direction = Direction.LEFT;
                    animator.Play("Walk_Left");
                }
            }
            else
            {
                if (movementInput.y > 0)
                {
                    direction = Direction.UP;
                    animator.Play("Walk_Up");
                }
                else
                {
                    direction = Direction.DOWN;
                    animator.Play("Walk_Down");
                }
            }
        }
        else
        {
            if (isThrowing)
            {
                PlayThrowAnimation();
            }
            else
            {
                PlayIdleAnimation();
            }
        }
    }

    private void PlayIdleAnimation()
    {
        switch (direction)
        {
            case Direction.DOWN:
                animator.Play("Idle_Down");
                break;
            case Direction.UP:
                animator.Play("Idle_Up");
                break;
            case Direction.LEFT:
                animator.Play("Idle_Left");
                break;
            case Direction.RIGHT:
                animator.Play("Idle_Right");
                break;
        }
    }

    private void PlayThrowAnimation()
    {
        switch (direction)
        {
            case Direction.DOWN:
                animator.Play("Throw_Down");
                break;
            case Direction.UP:
                animator.Play("Throw_Up");
                break;
            case Direction.LEFT:
                animator.Play("Throw_Left");
                break;
            case Direction.RIGHT:
                animator.Play("RightThrow");
                break;
        }
    }

    private void ThrowFlashBang()
    {
        switch (direction)
        {
            case Direction.DOWN:
                Instantiate(flashBang, downGrenadeSpawnPoint.transform.position, downGrenadeSpawnPoint.transform.rotation);
                break;
            case Direction.UP:
                Instantiate(flashBang, upGrenadeSpawnPoint.transform.position, upGrenadeSpawnPoint.transform.rotation);
                break;
            case Direction.LEFT:
                Instantiate(flashBang, leftGrenadeSpawnPoint.transform.position, leftGrenadeSpawnPoint.transform.rotation);
                break;
            case Direction.RIGHT:
                Instantiate(flashBang, rightGrenadeSpawnPoint.transform.position, rightGrenadeSpawnPoint.transform.rotation);
                break;
        }
    }

    public void FlashBanged()
    {
        Debug.Log("Called flash banged");
        GameObject canvasInstance = Instantiate(flashBangCanvas);
        canvasInstance.GetComponent<Canvas>().worldCamera = mainCamera;

        Destroy(canvasInstance, 0.24f);
    }
}