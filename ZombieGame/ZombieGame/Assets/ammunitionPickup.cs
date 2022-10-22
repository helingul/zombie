using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammunitionPickup : MonoBehaviour
{
    private int ammunitionCount = 10;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            ammoManager.instance.AddAmmunition(ammunitionCount);
            Destroy(gameObject);
        }
    }
}
