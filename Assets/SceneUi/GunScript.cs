using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    public WeaponSo weaponDetaille;
    // Start is called before the first frame update

   public bool GunEquiped = false;


    public Animator AnimeGun = null;


    void Start()
    {
        if (GunEquiped)
        {
            AnimeGun = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
