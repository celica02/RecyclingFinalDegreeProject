using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    private Camera cam;

    //public float maxX = 2.2F, maxY = 1.57F, maxZ = 2F;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
    }

    public void NewRandomPosition(float maxX, float maxY, float maxZ)
    {
        bool inFieldOfView = false;
        while (!inFieldOfView)
        {
            float newZ = Random.Range(cam.transform.position.z + 2, maxZ);

            newPosition = new Vector3(Random.Range(-newZ, newZ),
                                      Random.Range(-maxY, maxY),
                                      Random.Range(cam.transform.position.z + 2, maxZ));
            inFieldOfView = IsInFieldOfView(newPosition);
        }
        gameObject.transform.position = newPosition;
        gameObject.transform.rotation = Random.rotation;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
    //        GenerateRandomPosition();
    //}
    
    //void OnCollisionEnter()
    //{
    //    //if (coll.gameObject.tag == "MainCamera")
    //    NewRandomPosition();
    //}

    bool IsInFieldOfView(Vector3 _newPosition)
    {
        Vector3 objectScreenPoint = cam.WorldToViewportPoint(_newPosition);
        bool onScreen = objectScreenPoint.z > 0.1f 
                        && objectScreenPoint.x > 0.1f && objectScreenPoint.x < 0.9f 
                        && objectScreenPoint.y > 0.1f && objectScreenPoint.y < 0.9f;
        return onScreen;
    }
}
