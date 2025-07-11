using System.Collections;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject crossfade;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] Animator animator;
    [SerializeField] Player playerScript;

    public void CameraCrossfade()
    {
        playerScript.canMove = false;
        crossfade.SetActive(true);
        animator.Play("Crossfade_Start");
        StartCoroutine(FadeTime());
    }

    private IEnumerator FadeTime()
    {
        yield return new WaitForSeconds(1f);
        animator.Play("Crossfade_End");
        crossfade.SetActive(false);
        objectManager.ToggleCameraViews();
    }
}
