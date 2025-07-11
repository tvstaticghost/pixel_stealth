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

    private Vector2 movementInput = Vector2.zero;
    private Direction direction = Direction.DOWN;
    public bool canActivateComputer = false;
    public bool canMove = true;

    public void Move(InputAction.CallbackContext context)
    {
        if (canMove)
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
            PlayIdleAnimation();
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
}