using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    void OnMouseDown()
    {
        ScreenCapture.CaptureScreenshot("Prueba1.png");
        Debug.Log("Captura de pantalla");
    }
}
