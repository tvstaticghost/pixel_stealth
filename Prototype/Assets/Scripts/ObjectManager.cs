using System.Collections;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera securityCamera;
    [SerializeField] GameObject securityCanvas;
    [SerializeField] Player playerScript;
    [SerializeField] GameObject flashBang;
    [SerializeField] GameObject flashLight;
    private bool mainCameraView = false;

    public void ToggleCameraViews()
    {
        mainCameraView = !mainCameraView;
        SwitchCameras();
    }

    private void SwitchCameras()
    {
        if (mainCameraView)
        {
            SwitchToSecurityCameraView();
        }
        else
        {
            SwitchToMainCameraView();
        }
    }

    private void SwitchToSecurityCameraView()
    {
        mainCamera.gameObject.SetActive(false);
        securityCamera.gameObject.SetActive(true);
        securityCanvas.gameObject.SetActive(true);
    }

    private void SwitchToMainCameraView()
    {
        mainCamera.gameObject.SetActive(true);
        securityCamera.gameObject.SetActive(false);
        securityCanvas.gameObject.SetActive(false);
        playerScript.canMove = true;
    }

    public void SpawnFlash(Vector3 pos, Quaternion rot)
    {
        GameObject flashInstance = Instantiate(flashLight, pos, rot);
        StartCoroutine(FlashBangFlashTimer(flashInstance));
    }

    IEnumerator FlashBangFlashTimer(GameObject flash)
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(flash);
    }
}
