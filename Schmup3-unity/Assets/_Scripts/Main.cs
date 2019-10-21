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
    public float enemyDefaultPadding = 1.5f;
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[] { WeaponType.blaster, WeaponType.blaster, WeaponType.spread, WeaponType.shield };
    public ScoreScript score;
    private boundsScript bndCheck;

    [Header("Set Dynamically")]
    public float enemySpawnPerSecond = .5f;


    public void ShipDestroyed(Enemy e)
    {
        if (Random.value<=e.powerUpDropChance)
        {
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];
            GameObject go = Instantiate(prefabPowerUp);
            PowerUp pu = go.GetComponent<PowerUp>();
            pu.SetType(puType);
            pu.transform.position = e.transform.position;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        s = this;
        bndCheck = GetComponent<boundsScript>();
        //scoreInt = GetComponent<ScoreScript>();

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

        enemySpawnPerSecond = spawnRate();
        print(enemySpawnPerSecond);
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

    public float spawnRate()
    {
        //public ScoreScript scoreInt;
        //score = scoreInt.GetCompenent<ScoreScript>();

        //score.scoreInt += 1;

        if (score.scoreInt < 10 )
        {
            enemySpawnPerSecond = .3f;
        }
        else if (score.scoreInt > 10 && score.scoreInt < 25)
        {
            enemySpawnPerSecond = .5f;
        }
        else if (score.scoreInt > 25 && score.scoreInt < 50)
        {
            enemySpawnPerSecond = .7f;
        }
        else
        {
            enemySpawnPerSecond = .9f;
        }

        return enemySpawnPerSecond;
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
