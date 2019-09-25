using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main s;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = .5f;
    public float enemyDefaultPadding = 1.5f;

    private boundsScript bndCheck;
    
    // Start is called before the first frame update
    void Awake()
    {
        s = this;
        bndCheck = GetComponent<boundsScript>();

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

        
    }


    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
        float enemyPadding = enemyDefaultPadding;

        if (go.GetComponent<boundsScript>()!=null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<boundsScript>().radius);
        }

        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

    }
    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
