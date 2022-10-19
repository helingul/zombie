using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;

public class gunProjectiler : MonoBehaviour
{
    InputAction shoot;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public int fireRate = 10;
    private float nextTimeToFire = 0;



    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    void Start()
    {
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        shoot.AddBinding("<Gamepad>/x");

        shoot.Enable();


    }



    void Update()
    {
   
        
        bool isShooting = CrossPlatformInputManager.GetButton("shoot");
        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }

        // spcace tuþuyla shootluyo
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }*/
    }
    private void Fire()
    {
        // RaycastHit hit;
        muzzleFlash.Play();
        
        

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

        /*
        Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
        GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
        Destroy(impact, 5);*/
    }

    }
