using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ObjectsGenerator : MonoBehaviour
{
    private static ObjectsGenerator instance;

    public GameObject[] totalObjs;
    GameObject objInst;
    int objectsCont = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        totalObjs = Resources.LoadAll("Prefabs/Can/", typeof(GameObject)).Cast<GameObject>().ToArray();


    }
    public GameObject NewObject()
    {
        if (totalObjs[objectsCont] != null)//Instanciamos el nuevo objeto
        {
            objInst = GameObject.Instantiate(totalObjs[objectsCont], transform.position, transform.rotation);
            objInst.AddComponent<RandomPosition>();
            objectsCont++;

        }

        else //Si el objeto no puede ser cargado, pasamos al siguiente
        {
            Debug.Log("No se ha podido cargar el objeto" + totalObjs[objectsCont]);
            objectsCont++;

        }
        return objInst;
    }

    public static GameObject LoadNewObject()
    {
        return instance.NewObject();
    }

    public static bool AllObjectsLoaded()
    {
        return instance.objectsCont >= instance.totalObjs.Length - 1 ? true : false;
    }
}
