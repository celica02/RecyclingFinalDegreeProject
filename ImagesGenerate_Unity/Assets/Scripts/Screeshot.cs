using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screeshot : MonoBehaviour
{
    void OnMouseDown()
    {
        ScreenCapture.CaptureScreenshot("Prueba1.png");
        Debug.Log("Captura de pantalla");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
