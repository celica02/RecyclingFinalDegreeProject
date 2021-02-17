using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private static BackgroundManager instance;

    public GameObject[] planes;
    bool[] planesUsed;
    int nShowingPlanes;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        planes = GameObject.FindGameObjectsWithTag("Background");
        planes[0].transform.position = new Vector3(0, 0, 6);
        planes[0].transform.rotation = Quaternion.Euler(-90, 0, 0);
    }


    /*1. Cuántos planos van a mostrarse (mínimo tiene que haber uno)
     *2. Poner los planos a visibles o no visibles según corresponda
     *3. Ponerles a los planos, menos al de por defecto, una posición y rotación aleatoria
     *4. Ponerles una imagen aleatoria a todos los planos que se vayan a mostrar
    */

    public void Update()
    {
        nShowingPlanes = Random.Range(1, planes.Length);
        //Debug.Log(nShowingPlanes);

        for (int i = 0; i < planes.Length - 1; i++)
        {
            if (i < nShowingPlanes)
            {
                planes[i].SetActive(true);
                if (i > 0)
                {
                    planes[i].transform.position = new Vector3(Random.Range(-2.2F, 2.2F), Random.Range(-1.57F, 1.57F), 6F - i * 0.1f);
                    //planes[i].transform.rotation = Random.rotation;
                    planes[i].transform.rotation = Quaternion.Euler(Random.Range(0, 360), 90, -90);
                }
                planes[i].GetComponent<MeshRenderer>().material = BackgroundsLoader.GetRandomMaterial();
            }
            else
            {
                planes[i].SetActive(false);
            }
        }
    }
    //public static void LoadNewBackground()
    //{
    //    instance.GenerateBackground();
    //}
}
