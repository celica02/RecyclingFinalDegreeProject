using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    string destPath, srcPath, currentPath;
    public int materialsTypeCont = 0, photosCont = 0;
    public int photosQuantity = 10;
    string[] typesPaths;
    //public GameObject[] totalObjs;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        //Get the path of the Game data folder
        destPath = "../"/*Application.dataPath */+ "ImagesCreated/";
        if (!System.IO.Directory.Exists(destPath))
            System.IO.Directory.CreateDirectory(destPath);

        srcPath = Application.dataPath + "/Resources/Prefabs/";
        typesPaths = Directory.GetDirectories(srcPath);

        NewMaterial();
        //totalObjs = Resources.LoadAll("Prefabs/Can/", typeof(GameObject)).Cast<GameObject>().ToArray();
        //objectsQuantity = ObjectsGenerator.ObjectsQuantity();
    }
    void NewMaterial()
    {
        if (materialsTypeCont < typesPaths.Length)
        {
            currentPath = destPath + typesPaths[materialsTypeCont].Remove(0, srcPath.Length) + "/";
            if (!System.IO.Directory.Exists(currentPath))
                System.IO.Directory.CreateDirectory(currentPath);

            ObjectsGenerator.NewMaterialObjects(typesPaths[materialsTypeCont].Remove(0, srcPath.Length));
            Debug.Log(typesPaths[materialsTypeCont].Remove(0, srcPath.Length));
            Debug.Log(currentPath);

            //Output the Game data path to the console
            //Debug.Log("dataPath : " + destPath);
        }
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
        //Si no hemos terminado la vuelta de fotos ni la vuelta a los objetos
        if (photosCont < photosQuantity)
        {
            //Si acabamos de empezar la vuelta de fotos cargamos el nuevo objeto
            if (photosCont == 0)
                obj = ObjectsGenerator.LoadNewObject();

            //Pos aleatoria
            obj.GetComponent<RandomPosition>().NewRandomPosition(2.2F, 1.57F, 2F);

            //Background aleatorio
            //BackgroundManager.LoadNewBackground();

            //Hacer foto
            ScreenShot.TakeCameraScreenshot(Screen.width, Screen.height, currentPath + obj.name + System.DateTime.Now.ToString("_ddMMyyyy-HHmmssfff"));

            photosCont++;
        }

        else if (photosCont >= photosQuantity)
        {
            //Destruir objeto de la escena
            Destroy(obj);
            Debug.Log("Images taken: " + photosCont);

            photosCont = 0;
            if (ObjectsGenerator.AllObjectsLoaded()) //Si ya se han cargado todos los objetos se cierra la aplicación
            {
                Debug.Log("Todos los objetos del tipo " + currentPath.Remove(0, destPath.Length) + " han sido cargados y fotografiados");
                materialsTypeCont++;
                NewMaterial();
            }
        }


        //if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        //    ScreenShot.TakeCameraScreenshot(Screen.width, Screen.height, m_Path);

        //if (photosCont == 100)
        //{
        //    //var obj = Resources.Load("Prefabs/Can/opened_tunacan2") as GameObject;
        //    if (totalObjs[objectsCont] != null)
        //    {
        //        var objInst = GameObject.Instantiate(totalObjs[objectsCont], transform.position, transform.rotation);
        //    }
        //    else
        //        Debug.Log("No se ha podido cargar el objeto indicado");

        //    photosCont = 0;
        //    if (objectsCont >= totalObjs.Length-1)
        //        objectsCont = 0;
        //    else
        //        objectsCont++;
        //    Debug.Log("Object created");
        //}

        //else photosCont++;

    }
}
