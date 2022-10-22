using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunBullet : MonoBehaviour
{
    public float life = 3;
    public int gunDamage;

    public int GunDamage
    {
        get => gunDamage;
        set => gunDamage = value;
    }


    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {

        //Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
