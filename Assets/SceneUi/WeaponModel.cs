using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon/Gun")]
public class WeaponSo : ScriptableObject
{
    [System.Serializable]
    public struct Tir
    {
        public string weaponName;

        public GameObject Gun;


        public int magazineSize;

        public float fireRate;
        public int damages;

        public int numberBallSprea;
        public int AngleShoot;
        public bool Projectile;
        public GameObject PrefabProjectile;
        public GameObject MuzzleEffect;
    }

    public Tir[] Test;

   
    public string weaponName;

    public GameObject Gun;


    public int magazineSize;

    public float fireRate;
    public int damages;

    public int numberBallSprea;
    public int AngleShoot;
    public bool Projectile;
    public GameObject PrefabProjectile;
    public GameObject MuzzleEffect;

}