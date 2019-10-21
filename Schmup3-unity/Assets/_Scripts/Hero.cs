using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero s;

    [Header("Set in Inspector")]

    public float speed = 30;
    public float speedHolder;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;
    Vector3 moveDirectionPush;
    public Rigidbody HeroRigidbody;
    bool shooting = false; 
    public float pushDoneTime;
    public float invincibillity;
    public float invincibillityDuration = 1.5f;
    bool invin;
    public Material[] materials;
    public Color[] orignalColors;


    // int

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;
    private GameObject lastTriggerGo = null;
    protected boundsScript bndCheck;
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;
    public float pushSpeed = -5;
    public float pushDuration = .1f;


    // Start is called before the first frame update
    void Start()
    {
        if (s==null)
        {
            s = this; 
        }

  
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.s");
        }

        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);

        //fireDelegate += TempFire;
        bndCheck = GetComponent<boundsScript>();
        HeroRigidbody = GetComponent<Rigidbody>();
        speedHolder = speed;

        materials = Utils.GetAllMaterials(gameObject);
        orignalColors = new Color[materials.Length];

        for (int i = 0; i < materials.Length; i++)
        {
            orignalColors[i] = materials[i].color;
        }

        print(invincibillityDuration);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Weapon.type);
        // Debug.Log(WEAP_DICT[def.type]);
        //Debug.Log(weapons[0].type);

        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;

        if (!shooting)
        {
            pos.x += xAxis * speed * Time.deltaTime;
            pos.y += yAxis * speed * Time.deltaTime;
            
        }
        else
        {
            //print("here2");

            //WeaponType current = Weapon.get();

            pos.y += pushSpeed * Time.deltaTime;
            pos.x += xAxis * speed * Time.deltaTime;
        }

        if (shooting && Time.time > pushDoneTime)
        {
            //print("here3");
            dontPushBack();
        }

        transform.position = pos;

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TempFire();
        //}

        if (Time.time > invincibillity)
        {
            invin = false;
            //GetComponent<Renderer>().material.color = Color.white;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].color = orignalColors[i];
            }
        }

        if (Input.GetAxis("Jump") == 1f && fireDelegate!=null)
        {
            fireDelegate();
            PushBack();

            //print("here");

            //moveDirectionPush = HeroRigidbody.transform.position;
            ////- other.transform.position;

            //HeroRigidbody.AddForce(moveDirectionPush.normalized * 200f);

            //Vector3 pushBack = -transform.up * 100;
            //HeroRigidbody.AddForce(pushBack);

            //HeroRigidbody.AddForce(new Vector3(0, -300f, 0));

            //Vector3 pos2 = transform.position;
            //Vector3 pushBack = -transform.up * 100;


            //pos2.y += yAxis * speed * Time.deltaTime;
            //transform.position = pos2;
            //transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
        }

       if(weapons[0].type==WeaponType.blaster)
        {
            pushDuration = .5f;
            pushSpeed = -20;
            if (weapons[1].type== WeaponType.blaster)
            {
                pushDuration = .6f;
                pushSpeed = -25;
            }
            if (weapons[2].type == WeaponType.blaster)
            {
                pushDuration = .7f;
                pushSpeed = -35;
            }
            if (weapons[3].type == WeaponType.blaster)
            {
                pushDuration = .8f;
                pushSpeed = -45;
            }
            if (weapons[4].type == WeaponType.blaster)
            {
                pushDuration = .9f;
                pushSpeed = -60;
            }
        }

       else if(weapons[0].type == WeaponType.spread)
        {
            pushDuration = .8f;
            pushSpeed = -40;

            if (weapons[1].type == WeaponType.spread)
            {
                pushDuration = .8f;
                pushSpeed = -45;
            }
            if (weapons[2].type == WeaponType.spread)
            {
                pushDuration = .9f;
                pushSpeed = -50;
            }
            if (weapons[3].type == WeaponType.spread)
            {
                pushDuration = .9f;
                pushSpeed = -60;
            }
            if (weapons[4].type == WeaponType.spread)
            {
                pushDuration = .9f;
                pushSpeed = -70;
            }
        }

        else if (weapons[0].type == WeaponType.phaser)
        {
            pushDuration = .8f;
            pushSpeed = -50;
            if (weapons[1].type == WeaponType.phaser)
            {
                pushDuration = .9f;
                pushSpeed = -60;
            }
            if (weapons[2].type == WeaponType.phaser)
            {
                pushDuration = .9f;
                pushSpeed = -70;
            }
            if (weapons[3].type == WeaponType.phaser)
            {
                pushDuration = .9f;
                pushSpeed = -80;
            }
            if (weapons[4].type == WeaponType.phaser)
            {
                pushDuration = .9f;
                pushSpeed = -90;
            }

        }

        print(pushSpeed);
        //if (bndCheck.heroDown)
        //{
        //    print("Off" );
        //    shieldLevel--;
        //}

    }

    //void TempFire()
    //{
    //    GameObject projGo = Instantiate<GameObject>(projectilePrefab);
    //    projGo.transform.position = transform.position;
    //    Rigidbody rigidB = projGo.GetComponent<Rigidbody>();
    //    //rigidB.velocity = Vector3.up * projectileSpeed;

    //    Projectile proj = projGo.GetComponent<Projectile>();
    //    proj.type = WeaponType.blaster;
    //    float tspeed = Main.getWeaponDefinition(proj.type).velocity;
    //    rigidB.velocity = Vector3.up * tspeed;
    //}

    private void OnTriggerStay(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        lastTriggerGo = go;

        if (invin)
        {
            return;
        }

        else if (go.tag == "offDownCube")
        {
            shieldLevel--;
            Invincibillity(invincibillityDuration);
        }

    }
    //private void OnCollisionEnter(Collision other)
    //{
    //    print("here");

    //    Transform rootT = other.gameObject.transform.root;
    //    GameObject go = rootT.gameObject;
    //    lastTriggerGo = go;


    //    //if (invin)
    //    //{
    //    //    return;

    //    if (invin)
    //    {
    //        return;
    //    }

    //    if(go.tag=="offDownCube")
    //    {
    //        shieldLevel--;
    //        Invincibillity(invincibillityDuration);
    //    }


    //}

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        //if (go==lastTriggerGo)
        //{
        //    return;
        //}

        lastTriggerGo = go;

        if (invin)
        {
            return;
        }


        //print(go.tag);

        

        if (go.tag=="Enemy")
        {
            shieldLevel--;
            Destroy(go);
            Invincibillity(invincibillityDuration);
        }

        else if(go.tag=="PowerUp")
        {
            AbsorbPowerUp(go);
            
        }

        //else if (go.tag == "offDownCube")
        //{
        //    shieldLevel--;
        //    //print("hit");
        //    Invincibillity(invincibillityDuration);
        //}


        else
        {
            print("Triggered by non-Enemy: " + go.name);
        }
  
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();

        switch (pu.type)
        {
            case WeaponType.shield:
                shieldLevel++;
                break;
            case WeaponType.invincibillity:
                Invincibillity(4f);
                break;

            default:
                if(pu.type==weapons[0].type)
                {
                    Weapon w = GetEmptyWeaponSlot();
                    if (w != null)
                    {
                        w.SetType(pu.type); 
                    }
                }
                else
                {
                    ClearWeapons();
                    weapons[0].SetType(pu.type);
                }
                break;
        }
        pu.AbsorbedBy(this.gameObject);

    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }

        set
        {
            _shieldLevel = Mathf.Min(value, 4);

            if (value<0)
            {
                Destroy(this.gameObject);
                Main.s.DelayedRestart(gameRestartDelay);
            }
        }

    }

    //public float type
    //{

    //    get
    //    {
    //        return (_type);
    //    }
    //}

    Weapon GetEmptyWeaponSlot()
    {
        for (int i=0; i<weapons.Length; i++)
        {
            if (weapons[i].type==WeaponType.none)
            {
                return weapons[i];
            }
        }
        return (null);
    }


    void ClearWeapons()
    {
        foreach(Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }

    void PushBack()
    {
        shooting = true;
        //speed = pushSpeed;
        pushDoneTime = Time.time * pushDuration;


    }

    void dontPushBack()
    {

        shooting = false;
        //speed = speedHolder; 

    }

    void Invincibillity(float duration)
    {
        invin = true;
        invincibillity = Time.time+duration;

        // GetComponent<Renderer>().material.color=Color.blue;

        foreach (Material m in materials)
        {
            m.color = Color.blue;
        }

    }
}
