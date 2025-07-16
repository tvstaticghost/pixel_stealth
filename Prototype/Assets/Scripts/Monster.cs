using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected float flashDuration = 2f;
    [SerializeField] SpriteRenderer spriteRenderer;
    protected bool isFlashed = false;
    public IEnumerator FlashWhiteOverlay()
    {
        if (spriteRenderer == null)
        {
            Debug.Log("Sprite Renderer is null!");
        }
        isFlashed = true;
        Color originalColor = spriteRenderer.color;
        Color fadedColor = originalColor;
        fadedColor.a = 150f / 255f;
        spriteRenderer.color = fadedColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
        isFlashed = false;
    }
}
