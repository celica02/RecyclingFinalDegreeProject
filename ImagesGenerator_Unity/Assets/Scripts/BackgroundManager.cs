using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private static BackgroundManager instance;

    public GameObject[] planes;
    bool[] planesUsed;
    int nShowingPlanes;
    float z = 9;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        planes = GameObject.FindGameObjectsWithTag("Background");
        for (int i = 0; i < planes.Length; i++)
        {
            planes[i].transform.position = new Vector3(0, 0, z - 0.1f*i);
        }
        planes[0].transform.rotation = Quaternion.Euler(-90, 0, 0);
    }



    /*Background generator
     * 1. How many planes will be shown (at leats one)
     * 2. Set the planes active or inactive as needed. 
     * 3. Set a random position and rotation to the plane, except to the furthest one.
     * 4. Set a random image to the planes to be shown.
    */
    public void Update()
    {
        //How many planes will be shown for the background.
        nShowingPlanes = Random.Range(1, planes.Length + 1);

        for (int i = 0; i < planes.Length; i++)
        {
            if (i < nShowingPlanes)
            {
                planes[i].SetActive(true);
                //Random position and rotation for the planes except the furthest
                if (i > 0)
                {
                    planes[i].transform.position = new Vector3(Random.Range(-2.2F, 2.2F), Random.Range(-1.57F, 1.57F), planes[i].transform.position.z);
                    planes[i].transform.rotation = Quaternion.Euler(Random.Range(0, 360), 90, -90);
                }
                planes[i].GetComponent<MeshRenderer>().material = BackgroundsLoader.GetRandomMaterial();
            }
            else
                planes[i].SetActive(false);
        }
    }
}
