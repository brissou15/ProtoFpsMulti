using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon/Gun")]
public class WeaponSo : ScriptableObject
{



    public string weaponName;

    public GameObject Gun;

    [System.Serializable]
    public struct Tir
    {
        public int magazineSize;

        public float fireRate;
        public int damages;

        public int numberBallSprea;
        public int AngleShoot;
        //public bool Projectile;
        public GameObject PrefabProjectile;
    }

    public Tir[] ShootList;

   
    //public string weaponName;

    //public GameObject Gun;


    //public int magazineSize;

    //public float fireRate;
    //public int damages;

    //public int numberBallSprea;
    //public int AngleShoot;
    //public bool Projectile;
    //public GameObject PrefabProjectile;
    //public GameObject MuzzleEffect;

}