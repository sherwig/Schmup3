using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private boundsScript bndCheck;
    // Start is called before the first frame update
    void Awake()
    {
        bndCheck = GetComponent<boundsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
            print("off");
        }
    }

    
}
