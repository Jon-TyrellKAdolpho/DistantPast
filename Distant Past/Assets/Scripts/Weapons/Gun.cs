using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    [SerializeField] GunManager manager;
    public int gunImage;
    public bool locked;

    public int type;
    public int shotCount;
    [Tooltip("1 - 60 for how many shoots per second")]
    public float fireRate;
    // 0 = blue, 1 = yellow, 2 = green
    public int energy;
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

    public int retroScope;
    public int hdScope;
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

    public void Shoot()
    {
        if (energy == 0 && (manager.blue.trueRechargeTime > 0 || manager.blue.currentEnergy <= minimal)) return;
        if (energy == 1 && (manager.yellow.trueRechargeTime > 0 || manager.yellow.currentEnergy <= minimal)) return;
        if (energy == 2 && (manager.green.trueRechargeTime > 0 || manager.green.currentEnergy <= minimal)) return;


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
        bullet.Play();

        GameObject soundobject = Instantiate(soundPrefab);
        if (energy == 0) manager.blue.UseEnergy(energyPerShot); else if (energy == 1) 
            manager.yellow.UseEnergy(energyPerShot); else if (energy == 2) manager.green.UseEnergy(energyPerShot);

    }

    public void Unlock()
    {
        locked = false;
    }
    public void Lock()
    {
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
