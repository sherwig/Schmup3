using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int score = 100;
    public float showDamageDuration = .1f;
    public float powerUpDropChance = 1f;

    [Header("Set in Inspector: Enemy")]

    public float speed = 10f; 
    public float fireRate=.3f;
    public float health = 10;
    public Color[] orignalColors;
    public Material[] materials;
    public bool showingdamage = false;
    public float damageDoneTime;
    public bool notifiedOfDestruction = false;
    

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
        bndCheck = GetComponent<boundsScript>();

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
        Move();

        if(showingdamage && Time.time>damageDoneTime)
        {
            UnShowDamage();
        }

        if (bndCheck!=null && bndCheck.offDown)
        {    
          Destroy(gameObject); 
        }
    }

 

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
                    if (!notifiedOfDestruction)
                    {
                        Main.s.ShipDestroyed(this);
                    }
                    notifiedOfDestruction = true;
                    Destroy(this.gameObject);
                }

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
