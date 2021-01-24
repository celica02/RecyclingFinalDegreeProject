using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private static ScreenShot instance;

    private Camera myCamera;
    private bool takeScreenShotOnNextFrame;
    private string path, imageName;

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
     
    }

    private void OnPostRender()
    {
        if (takeScreenShotOnNextFrame)
        {
            takeScreenShotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);


            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(path + ".png", byteArray);
            //Debug.Log("Created 'Photo.png'");
            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }

    }

    private void TakeScreenshots(int width, int height, string savingPath)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenShotOnNextFrame = true;
        path = savingPath;
    }

    public static void TakeCameraScreenshot (int width, int height, string savingPath)
    {
        instance.TakeScreenshots(width, height, savingPath);
    }
}
