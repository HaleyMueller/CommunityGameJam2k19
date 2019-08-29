using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CameraHandler : MonoBehaviour
{
    bool isInCameraMode = false;

    public Camera c;

    private void ShowLayer()
    {
        c.cullingMask |= 1 << LayerMask.NameToLayer("Camera");
        isInCameraMode = true;
    }

    private void HideLayer()
    {
        c.cullingMask &= ~(1 << LayerMask.NameToLayer("Camera"));
        isInCameraMode = false;
    }
    public Texture GetScreenShot()
    {
        return ScreenCapture.CaptureScreenshotAsTexture();
    }
}
