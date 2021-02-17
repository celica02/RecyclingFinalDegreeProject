using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ObjectsGenerator : MonoBehaviour
{
    private static ObjectsGenerator instance;

    public GameObject[] totalObjs;
    GameObject objInst;
    int objectsCont=0;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

    }

    private void NewMaterial(string materialType)
    {
        string m_path = "Prefabs/" + materialType + "/";
        Debug.Log(m_path);

        totalObjs = Resources.LoadAll(m_path, typeof(GameObject)).Cast<GameObject>().ToArray();
        objectsCont = 0;

    }

    private GameObject NewObject()
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
        Debug.Log(objInst.name + "object " + objectsCont + " of " + totalObjs.Length);
        return objInst;
    }

    public static GameObject LoadNewObject()
    {
        return instance.NewObject();
    }

    public static bool AllObjectsLoaded()
    {
        return instance.objectsCont >= instance.totalObjs.Length ? true : false;
    }

    public static void NewMaterialObjects(string materialType)
    {
        instance.NewMaterial(materialType);
    }
}
