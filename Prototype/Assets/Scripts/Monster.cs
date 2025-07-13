using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected float flashDuration = .10f;
    [SerializeField] SpriteRenderer spriteRenderer;
    public IEnumerator FlashWhiteOverlay()
    {
        Color originalColor = spriteRenderer.color;
        Color fadedColor = originalColor;
        fadedColor.a = 150f / 255f;
        spriteRenderer.color = fadedColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}
