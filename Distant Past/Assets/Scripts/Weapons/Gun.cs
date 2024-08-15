using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    [SerializeField] GunManager manager;
    public Image gunImage;
    public bool locked;
    [HideInInspector]
    public bool canShoot;
    [SerializeField] int type;
    [SerializeField] float fireRate = .125f;
    float trueFireRate;
    [SerializeField] int perShot = 3;
    int truePerShot;
    public Energy energy;
    Transform cam;

    public float aimFOV = 30;

    public int damage;
    public float energyPerShot;
    [SerializeField] float minimal;

    [SerializeField] LayerMask hittable;

    [SerializeField] ParticleSystem bullet;
    public float baseAngle = 2;
    public float aimAngle = 1;

    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject soundPrefab;

    public GameObject retroScope;
    public GameObject hdScope;
    // Start is called before the first frame update
    Transform target;
    private void Start()
    {
        target = FindObjectOfType<GunManager>().target;
        cam = FindObjectOfType<FirstPersonLook>().GetComponent<Camera>().transform;
        bullet.GetComponent<Bullet>().SetDamage(damage);
        var shapemodule = bullet.shape;
        shapemodule.angle = baseAngle;
    }

    // Update is called once per frame
    void Update()
    {
        FireWeapon();
    }
    public void FireWeapon()
    {
        if (locked != true && PauseHandler.instance.isPaused != true)
        {
            if (type == 0)
            {
                HandleBasicShooting();
            }
            if (type == 1)
            {
                HandleAutoShooting();
            }
            if (type == 2)
            {
                HandleSemiAutoShooting();
            }

        }
    }
    void HandleBasicShooting()
    {
        if (Input.GetKeyDown(manager.shootKey))
        {
            Shoot();
        }
    }
    void HandleAutoShooting()
    {
        if (Input.GetKeyDown(manager.shootKey))
        {
            canShoot = true;
        }
        if (Input.GetKeyUp(manager.shootKey))
        {
            canShoot = false;
        }
        if (canShoot == true)
        {
            trueFireRate -= Time.deltaTime;
            if (trueFireRate <= 0)
            {
                Shoot();
                trueFireRate = fireRate;
            }
        }
    }
    void HandleSemiAutoShooting()
    {
        if (Input.GetKeyDown(manager.shootKey))
        {
            canShoot = true;
            truePerShot = perShot;
        }
        if (Input.GetKeyUp(manager.shootKey))
        {
            canShoot = false;
 
        }
        if (canShoot == true)
        {
            trueFireRate -= Time.deltaTime;
            if (trueFireRate <= 0)
            {
                Shoot();
                trueFireRate = fireRate;
                truePerShot--;
            }
        }
        if(truePerShot <= 0)
        {
            canShoot = false;
        }
    }

    void Shoot()
    {
        if(energy.trueRechargeTime > 0)
        {
            return;
        }
        if(energy.currentEnergy <= minimal)
        {
            return;
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hittable))
        {
            target.position = hit.point;
        }
        else
        {
            target.position = ray.origin + ray.direction * 500f;
        }
        if (bullet.GetComponent<Bullet>().GetDamage() != damage)
        {
            bullet.GetComponent<Bullet>().SetDamage(damage);
        }
        bullet.transform.LookAt(target);
       // bullet.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        bullet.Play();

        GameObject soundobject = Instantiate(soundPrefab);
        energy.UseEnergy(energyPerShot);

    }

    public void Unlock()
    {
        gunImage.gameObject.SetActive(true);
        locked = false;
    }
    public void Lock()
    {
        gunImage.gameObject.SetActive(false);
        locked = true;
    }

    public void SetAngle(bool main)
    {
        var shapemodule = bullet.shape;
        if (main)
        {
            shapemodule.angle = baseAngle;
        }
        else
        {
            shapemodule.angle = aimAngle;
        }

    }
}
