using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ammoManager : MonoBehaviour
{
    public static ammoManager instance;
    public TextMeshProUGUI ammunitionCountText;
    public int ammunitionCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }


    public bool ConsumeAmmo()
    {
        if (ammunitionCount > 0)
        {
           
            ammunitionCount--;
            UpdateAmmoCountUI();
            return true;
        }
        else
        {
            return false;
        }
    }
    private void UpdateAmmoCountUI()
    {
        ammunitionCountText.text = "Ammo: " + ammunitionCount;
    }

    public void AddAmmunition(int value)
    {
        ammunitionCount += value;
        UpdateAmmoCountUI();
    }
}
