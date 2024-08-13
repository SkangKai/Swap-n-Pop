using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    float timer, timeToStop = 2f;
    Vector3 startPosition, endPosition;
    bool isZoom = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPosition = new Vector3(transform.position.x, transform.position.y - 4.5f, transform.position.z + 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isZoom)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, timer / timeToStop);
            if(timer >= timeToStop)
            {
                isZoom = false;
                timer = 0;
            }
        }
        
    }
}
