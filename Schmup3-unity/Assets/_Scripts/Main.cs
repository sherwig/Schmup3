using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main s;
    static Dictionary <WeaponType, WeaponDefinition> WEAP_DICT;


    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = .5f;
    public float enemyDefaultPadding = 1.5f;
    public WeaponDefinition[] weaponDefinitions; 

    private boundsScript bndCheck;
    
    // Start is called before the first frame update
    void Awake()
    {
        s = this;
        bndCheck = GetComponent<boundsScript>();

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach(WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }

        
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

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay); 
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }


    static public WeaponDefinition getWeaponDefinition(WeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }

        return (new WeaponDefinition());

    }



    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
