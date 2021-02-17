using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BackgroundsLoader : MonoBehaviour
{
    private static BackgroundsLoader instance;

    public Material[] totalMaterials;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        totalMaterials = Resources.LoadAll("Background/Materials/", typeof(Material)).Cast<Material>().ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Material RandomMaterial()
    {
        int m = Random.Range(0, totalMaterials.Length - 1);
        return totalMaterials[m];
    }

    public static Material GetRandomMaterial()
    {
        return instance.RandomMaterial();
    }
}
