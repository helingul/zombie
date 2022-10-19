using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class gun : MonoBehaviour
{

    InputAction shoot;


    public Transform fpsCam;
    public float range = 20;
    public float impactForce = 150;
    public int damageAmount = 20;

    public int fireRate = 10;
    private float nextTimeToFire = 0;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public int currentAmmo;
    public int maxAmmo = 10;
    public int magazineAmmo = 30;

    public float reloadTime = 2f;
    public bool isReloading;

    public Animator animator;

    

    void Start()
    {
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        shoot.AddBinding("<Gamepad>/x");

        shoot.Enable();

       
    }
    
    // Update is called once per frame
    void Update()
    {
        bool isShooting = CrossPlatformInputManager.GetButton("shoot");
        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }
    }
    private void Fire()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        if(Physics.Raycast(fpsCam.position, fpsCam.forward,out hit, range))
        {
            /*if(hit.rigidbody != null)
            {
                Debug.Log("aa");
                //ZombieAI zombie = hit.collider.gameObject.GetComponent<ZombieAI>();
                //zombie.health -= 20;
                //Debug.Log("VURDUN, CANII " + zombie.health);

                EnemyAI enemy = hit.collider.gameObject.GetComponent<EnemyAI>();
                enemy.health -= 20;
                Debug.Log("VURDUN, CANII " + enemy.health);
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            EnemyAI enemy = hit.transform.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                //hit.rigidbody.AddForce(-hit.normal * impactForce);

                enemy.TakeDamage(20);
                Debug.Log("VURDUN, CANII " + enemy.health);
            }*/
           





            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
           Destroy(impact, 5);

        }
    }
    
}