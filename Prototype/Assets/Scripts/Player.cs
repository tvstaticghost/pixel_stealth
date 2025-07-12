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
                levelLoader.CameraCrossfade();
                Debug.Log("Using Computer");
            }
        }
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if (context.canceled && canThrow)
        {
            Debug.Log("Throw");
            canThrow = false;
            isThrowing = true;
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

    void FixedUpdate()
    {
        rb.linearVelocity = movementInput * movementSpeed;

        if (movementInput != Vector2.zero)
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
}