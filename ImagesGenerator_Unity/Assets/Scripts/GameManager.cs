﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    string m_Path;
    public int objectsCont = 0, photosCont = 0;
    public int photosQuantity = 10, objectsQuantity;
    //public GameObject[] totalObjs;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        //Get the path of the Game data folder
        m_Path = Application.dataPath + "/ImagesCreated/";

        //Output the Game data path to the console
        Debug.Log("dataPath : " + m_Path);

        if (!System.IO.Directory.Exists(m_Path))
            System.IO.Directory.CreateDirectory(m_Path);

        //totalObjs = Resources.LoadAll("Prefabs/Can/", typeof(GameObject)).Cast<GameObject>().ToArray();
        //objectsQuantity = ObjectsGenerator.ObjectsQuantity();
    }

    // Update is called once per frame
    void Update()
    {
        //Si no hemos terminado la vuelta de fotos ni la vuelta a los objetos
        if (photosCont < photosQuantity - 1)
        {
            //Si acabamos de empezar la vuelta de fotos cargamos el nuevo objeto
            if (photosCont == 0)
                obj = ObjectsGenerator.LoadNewObject();

            //Pos aleatoria
            obj.GetComponent<RandomPosition>().NewRandomPosition();

            //Hacer foto
            ScreenShot.TakeCameraScreenshot(Screen.width, Screen.height, m_Path + obj.name + System.DateTime.Now.ToString("_ddMMyyyy-HHmmssfff"));

            photosCont++;
        }

        else if (photosCont >= photosQuantity - 1 )
        {
            //Destruir objeto de la escena
            Destroy(obj);
            photosCont = 0;
            if (ObjectsGenerator.AllObjectsLoaded()) //Si ya se han cargado todos los objetos se cierra la aplicación
            {
                Debug.Log("Todos los objetos han sido cargados y fotografiados");
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif

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
