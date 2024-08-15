using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public bool locked;
    public Energy energy;
    public Transform firePoint; // The point where the projectile will be spawned
    public GameObject projectilePrefab; // The projectile prefab
    [SerializeField] float energyPerShot;
    public float projectileForce = 10f; // The force applied to the projectile
    [SerializeField] Transform trackPoint;
    [SerializeField] Transform cam;
    [SerializeField] LayerMask hittable;
    [SerializeField] ParticleSystem muzzleEffect;
    // Update is called once per frame

    void Update()
    {
        if(locked != true)
        {
            Ray ray = new Ray(cam.position, cam.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200, hittable))
            {
                // If the ray hits an object, move the target to the hit point
                trackPoint.position = hit.point;
            }
            else
            {
                // If no object is hit, move the target 200 units away from the camera
                trackPoint.position = ray.GetPoint(200);
            }
            firePoint.LookAt(trackPoint.position);
            if (Input.GetKeyDown(KeyCode.Mouse0)) // Assuming "Fire1" is the input for firing (can be configured in Unity Input settings)
            {
                Shoot();
            }
        }
        
    }

    void Shoot()
    {
        if (energy.trueRechargeTime > 0)
        {
            return;
        }
        if (energy.currentEnergy <= 0)
        {
            return;
        }
        muzzleEffect.Play();
        GameObject cannonball = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rbCannonball = cannonball.GetComponent<Rigidbody>();

        rbCannonball.AddForce(firePoint.forward * projectileForce, ForceMode.Impulse);
        // Apply force to the cannonball in the forward direction
        energy.UseEnergy(energyPerShot);

    }
}