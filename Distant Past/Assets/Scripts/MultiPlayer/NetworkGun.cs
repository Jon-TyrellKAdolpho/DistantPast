using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkGun : NetworkBehaviour
{
    Energy energy;
    Transform cam;

    [SerializeField] int damage;
    [SerializeField] float energyPerShot;

    [SerializeField] LayerMask hittable;


    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioSource hitSound;

    // Start is called before the first frame update
    private void Start()
    {
        if (!IsOwner) return;
        energy = GetComponentInParent<NetworkPlayer>().GetComponentInChildren<Energy>();
        cam = GetComponentInParent<NetworkPlayerLook>().GetComponent<Camera>().transform; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
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
        muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 100, hittable))
        {
            if (hit.transform != null)
            {
                Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                hitSound.Play();
                NetworkPlayer health = hit.transform.GetComponent<NetworkPlayer>();
                if (health != null)
                {
                    health.TakeDamageServerRPC(damage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * damage);
                }
            }
        }
        energy.UseEnergy(energyPerShot);

    }
}
