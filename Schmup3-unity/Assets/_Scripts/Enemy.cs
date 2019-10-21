using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //public int score = 100;
    public float showDamageDuration = .1f;
    public float powerUpDropChance = 1f;
   

   [Header("Set in Inspector: Enemy")]
    public float speed=10f;
    public ScoreScript score;
    public float fireRate=.3f;
    public float health = 10;
    public Color[] orignalColors;
    public Material[] materials;
    public bool showingdamage = false;
    public float damageDoneTime;
    public bool notifiedOfDestruction = false;
    private int kill = 0;
    float perlin = 3;


    protected boundsScript bndCheck;

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    private void Awake()
    {
        //speed = 10f;
        bndCheck = GetComponent<boundsScript>();
       // score = GetComponent<ScoreScript>().UpdateScore();

        materials = Utils.GetAllMaterials(gameObject);
        orignalColors = new Color[materials.Length];

        for (int i=0; i<materials.Length; i++)
        {
            orignalColors[i] = materials[i].color;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //speed = speed * Mathf.PerlinNoise(Time.time * perlin, 0.0f);

        Move();

       // speed = setSpeed();
        //print(speed);
        if (showingdamage && Time.time>damageDoneTime)
        {
            UnShowDamage();
        }

        if (bndCheck!=null && bndCheck.offDown)
        {    
          Destroy(gameObject); 
        }
    }

    //public float setSpeed()
    //{
    //    if (score.scoreInt < 10)
    //    {
    //        speed = 10f;
    //    }
    //    else if (score.scoreInt > 10 && score.scoreInt < 25)
    //    {
    //        speed = 15f;
    //    }
    //    else if (score.scoreInt > 25 && score.scoreInt < 50)
    //    {
    //        speed = 20f;
    //    }
    //    else
    //    {
    //        speed = 25f;
    //    }


    //    return speed;

    //}
 

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGo = collision.gameObject;
     
        switch (otherGo.tag)
        {
            case "ProjectileHero":
                Projectile p = otherGo.GetComponent<Projectile>();
                showDamage();
                
                if (!bndCheck.isOnScreen)
                {
                    
                    Destroy(otherGo);
                    break;
                }

                health -= Main.getWeaponDefinition(p.type).damageOnHit;
                if(health<=0)
                {
                    
                    //kill = 1;
                    if (!notifiedOfDestruction)
                    {
                        Main.s.ShipDestroyed(this);
                    }
                    notifiedOfDestruction = true;
                    Destroy(this.gameObject);
                    ScoreScript.S.UpdateScore();
                }

                //else
                //{
                //    kill = 0;
                //}

               
                Destroy(otherGo);
                break;

            default:
                print("Enemy hit by non-projectileHero: " + otherGo.name);
                break;
        }

       
    }

    void showDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        showingdamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = orignalColors[i];
        }
        showingdamage = false;
    }
}
