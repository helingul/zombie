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
    public int damageAmount;
    public GameObject enemy;
    [SerializeField] private ammoManager ammoManager;

   



    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    void Start()
    {
        ammoManager = ammoManager.instance;
        EnemyAI zombie = enemy.GetComponent<EnemyAI>();
        zombie.SetDamageAmount(damageAmount);

        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        shoot.AddBinding("<Gamepad>/x");

        shoot.Enable();
        

    }



    void Update()
    {


        // MOBIL ICIN BURAYI AC

        /*bool isShooting = CrossPlatformInputManager.GetButton("shoot");
        if ((isShooting) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }*/
        // PC ICIN BURAYI AC
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire )
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }

       

    }
    private void Fire()
    {

        if (ammoManager.ConsumeAmmo())
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

    }
