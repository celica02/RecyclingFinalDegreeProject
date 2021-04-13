using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    string imagesPath, //Path to the folder where all the images will be saved.
           srcPath,    //Source path of the 3D models
           destPath;   //Destination path for each image. It'll be a sub-folder of the "imagesPath"

    [HideInInspector]public int materialsTypeCont = 0, photosCont = 0;
    public int photosQuantity = 10;
    string[] materialsPath; //Array for the different materials

    GameObject[] backgrounds;
    float nearestBackground;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");

        //Get the path of the Game data folder
        imagesPath = "../"/*Application.dataPath */+ "ImagesCreated/";
        if (!System.IO.Directory.Exists(imagesPath))
            System.IO.Directory.CreateDirectory(imagesPath);

        srcPath = Application.dataPath + "/Resources/Prefabs/";
        materialsPath = Directory.GetDirectories(srcPath);

        NewMaterial();
        //totalObjs = Resources.LoadAll("Prefabs/Can/", typeof(GameObject)).Cast<GameObject>().ToArray();
        //objectsQuantity = ObjectsGenerator.ObjectsQuantity();
    }
    void NewMaterial()
    {
        if (materialsTypeCont < materialsPath.Length)
        {
            //A new directory is created for each material as a subfolder of the image destination path.
            destPath = imagesPath + materialsPath[materialsTypeCont].Remove(0, srcPath.Length) + "/";
            if (!System.IO.Directory.Exists(destPath))
                System.IO.Directory.CreateDirectory(destPath);

            ObjectsGenerator.NewMaterialObjects(materialsPath[materialsTypeCont].Remove(0, srcPath.Length));
            Debug.Log(materialsPath[materialsTypeCont].Remove(0, srcPath.Length));
            Debug.Log(destPath);

            //Output the Game data path to the console
            //Debug.Log("dataPath : " + destPath);
        }

        //When all the materials and theirs models have been captured the application stops.
        else
        {
                #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
                #else
            Application.Quit();
                #endif
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photosCont < photosQuantity)
        {
            //If all the pictures have been taken a new object is loaded.
            if (photosCont == 0)
                obj = ObjectsGenerator.LoadNewObject();

            //Random position for the object
            obj.GetComponent<RandomPosition>().NewRandomPosition(1.5F, 1.57F, backgrounds[0].transform.position.z);

            
            ScreenShot.TakeCameraScreenshot(Screen.width, Screen.height, destPath + obj.name + System.DateTime.Now.ToString("_ddMMyyyy-HHmmssfff"));

            photosCont++;
        }

        else if (photosCont >= photosQuantity)
        {
            Destroy(obj);
            Debug.Log("Images taken: " + photosCont);

            photosCont = 0;
            if (ObjectsGenerator.AllObjectsLoaded()) //Si ya se han cargado todos los objetos se cierra la aplicación
            {
                Debug.Log("Todos los objetos del tipo " + destPath.Remove(0, imagesPath.Length) + " han sido cargados y fotografiados");
                materialsTypeCont++;
                NewMaterial();
            }
        }
    }
}
