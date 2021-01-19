using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    string m_Path;

    // Start is called before the first frame update
    void Start()
    {
        //Get the path of the Game data folder
        m_Path = Application.dataPath + "/ImagesCreated";

        //Output the Game data path to the console
        Debug.Log("dataPath : " + m_Path);

        if (!System.IO.Directory.Exists(m_Path))
            System.IO.Directory.CreateDirectory(m_Path);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            ScreenShot.TakeCameraScreenshot(600, 600, m_Path);
    }
}
