using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera securityCamera;
    [SerializeField] GameObject securityCanvas;
    [SerializeField] Player playerScript;
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
        playerScript.canMove = false;
    }

    private void SwitchToMainCameraView()
    {
        mainCamera.gameObject.SetActive(true);
        securityCamera.gameObject.SetActive(false);
        securityCanvas.gameObject.SetActive(false);
        playerScript.canMove = true;
    }
}
