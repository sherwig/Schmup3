using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangerZoneScript : MonoBehaviour
{
    //public float x = 0.1f;
    //public float y = 0.1f;
    //public float z = 0.1f;
    //void Update()
    //{
    //    // Widen the object by x, y, and z values
    //    transform.localScale += new Vector3(x, y, z);
    //}

    // Range over which height varies.
    float heightScale = 7.5f;

    // Distance covered per second along X axis of Perlin plane.
    float xScale = 1.0f;


    void Update()
    {
        float height = (heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f))-40f;
        //print(height);
        Vector3 pos = transform.position;
        pos.y = height;
        transform.position = pos;
    }
}
