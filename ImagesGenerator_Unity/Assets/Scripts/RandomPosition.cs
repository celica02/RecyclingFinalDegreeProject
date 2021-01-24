using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    private static RandomPosition instance;

    private Camera cam;
    public float maxX = 2.2F, maxY = 1.57F, maxZ = 4.86F;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        instance = this;
    }

    static public Vector3 GenerateRandomPosition()
    {
        return instance.NewRandomPosition();
    }

    static public Quaternion GenerateRandomRotation()
    {
        return Random.rotation;
    }

    private Vector3 NewRandomPosition()
    {
        bool outOfFieldOfView = false;
        while (!outOfFieldOfView)
        {
            newPosition = new Vector3(Random.Range(-maxX, maxX),
                                      Random.Range(-maxY, maxY),
                                      Random.Range(-maxZ, maxZ));
            outOfFieldOfView = IsInFieldOfView(newPosition);
        }
        return newPosition;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
    //        GenerateRandomPosition();
    //}
    
    void OnCollisionEnter()
    {
        //if (coll.gameObject.tag == "MainCamera")
            GenerateRandomPosition();
    }

    bool IsInFieldOfView(Vector3 _newPosition)
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(_newPosition);
        bool onScreen = screenPoint.z > 0f 
                        && screenPoint.x > 0f && screenPoint.x < 1f 
                        && screenPoint.y > 0f && screenPoint.y < 1f;
        return onScreen;
    }
}
