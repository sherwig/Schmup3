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

    float heightScale;

    [Header("Set in Inspector")]
    public ScoreScript score;

    //void Start()
    //{
    //    // Range over which height varies.
    //   // float heightScale = 3f;

    //}

    // Distance covered per second along X axis of Perlin plane.
    float xScale = 1.0f;


    void Update()
    {
        heightScale = heightSet();
        float height = (heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f))-45f;
        //print(height);
        Vector3 pos = transform.position;
        pos.y = height;
        transform.position = pos;
    }

    public float heightSet()
    {
        if(score.scoreInt<10)
        {
            heightScale = 5f;
        }

       else if (score.scoreInt >=10 && score.scoreInt<25)
        {
            heightScale = 7.5f;
        }
        else if (score.scoreInt >= 25 && score.scoreInt < 50)
        {
            heightScale = 10f;
        }
        else
        {
            heightScale = 14f;
        }

        return heightScale;

    }
}
