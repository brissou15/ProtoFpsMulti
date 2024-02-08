using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGun : MonoBehaviour
{

    [SerializeField]
    GameObject CamUi;

    [SerializeField]
    public List<WeaponSo> GunList = new List<WeaponSo>();

    GameObject Gun = null;

    int GunEquipedId = 0;

    float TimerShoot = 0;

    [SerializeField]
    GameObject Trail;

    WeaponSo CurrentWeaponD;
    // Start is called before the first frame update
    void Start()
    {
        if (GunList.Count > 0)
        {
            Gun = Instantiate(GunList[GunEquipedId].Gun);
            Gun.GetComponent<GunScript>().GunEquiped = true;
            Gun.transform.SetParent(CamUi.transform, false);
        }
    }

    void Update()
    {
        TimerShoot += Time.deltaTime;
        ShootAndGunManager();
     
    }





    Vector3 direction;
    void Calcul(int angleRecup)
    {
        Vector3 machin = Random.insideUnitSphere * angleRecup;

        direction = Quaternion.Euler(machin.x, machin.y, machin.z) * CamUi.transform.forward;
    }
    void ShootAndGunManager()
    {
        if (Gun != null)
        {
            if (Input.GetMouseButton(0))
            {

                CurrentWeaponD = Gun.GetComponent<GunScript>().weaponDetaille;

                // Debug.Log(CurrentWeaponAnim.GetFloat("TimerShootOui"));

                if (TimerShoot >= 1 / CurrentWeaponD.fireRate)
                {
                    shooting(CurrentWeaponD);
                    TimerShoot = 0;
                }
            }
        }
    }

    private void shooting(WeaponSo ModelWeapon)
    {

        //Grenade grenade = Instantiate(GrenadePreFab, transform.position + transform.forward + transform.up, Quaternion.identity);

        //grenade.throwDir = fpsCamera.transform.forward;

        if (ModelWeapon.Projectile)
        {
            GameObject Projectile = Instantiate(ModelWeapon.PrefabProjectile, CamUi.transform.position + CamUi.transform.right * 0.5f + -CamUi.transform.up * 0.2f, Quaternion.identity);

            Projectile.GetComponent<ProjectileScriptRoquette>().throwDir = CamUi.transform.forward;
            Projectile.transform.localRotation = CamUi.transform.rotation;
            Projectile.GetComponent<ProjectileScriptRoquette>().LayerName = LayerMask.LayerToName(gameObject.layer);
            Debug.Log(LayerMask.LayerToName(gameObject.layer));

        }
        else
        {
            if (ModelWeapon.MuzzleEffect != null)
            {
                GameObject MuzzleEffect = Instantiate(ModelWeapon.MuzzleEffect, ModelWeapon.MuzzleEffect.transform.position, Quaternion.identity);
                MuzzleEffect.transform.SetParent(CamUi.transform, false);

                //Rail.transform.localRotation = cam.transform.rotation;

            }
            for (int i = 0; i < ModelWeapon.numberBallSprea; i++)
            {

                GameObject Rail = Instantiate(Trail, CamUi.transform.position + CamUi.transform.right * 0.5f + -CamUi.transform.up * 0.4f, Quaternion.identity);
                Calcul(ModelWeapon.AngleShoot);
                Rail.GetComponent<TrailGun>().throwDir = direction;
                //RandomX = Random.Range(RANGEMIN, RANGEMAX);
                Ray ray = new Ray(CamUi.transform.position, direction);



                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("Projectile") | (1 << LayerMask.NameToLayer("Corps")))))
                {
                    //    if (hit.collider.tag == "Ennemis")
                    //    {



                    //        ParticleSystem ps = Instantiate(ImpactEnnemis, hit.point, Quaternion.LookRotation(hit.normal));

                    //    }
                    //    else
                    //    {
                    //        ParticleSystem ps = Instantiate(ImpactHit, hit.point, Quaternion.LookRotation(hit.normal));
                    //    }
                }
            }
        }

    }

    void SwapGunManager()
    {

        Destroy(Gun);
        if (Input.mouseScrollDelta.y > 0.1)
        {
            GunEquipedId++;
            if (GunEquipedId >= GunList.Count)
            {
                GunEquipedId = 0;
            }
        }
        if (Input.mouseScrollDelta.y < -0.1)
        {
            GunEquipedId--;
            if (GunEquipedId < 0)
            {
                GunEquipedId = GunList.Count - 1;
            }
        }
        Gun = Instantiate(GunList[GunEquipedId].Gun);
        Gun.GetComponent<GunScript>().GunEquiped = true;
        Gun.transform.SetParent(CamUi.transform, false);
        //CurrentWeaponAnim = Gun.GetComponent<GunScript>().AnimeGun;

        CurrentWeaponD = Gun.GetComponent<GunScript>().weaponDetaille;

    }
    void TakingGunManager()
    {
        //canShoot = false;
        Destroy(Gun);
        Gun = Instantiate(GunList[GunList.Count - 1].Gun);
        Gun.GetComponent<GunScript>().GunEquiped = true;
        Gun.transform.SetParent(CamUi.transform, false);
        //CurrentWeaponAnim = Gun.GetComponent<GunScript>().AnimeGun;
        CurrentWeaponD = Gun.GetComponent<GunScript>().weaponDetaille;
        GunEquipedId++;
    }

    // Update is called once per frame


    private void LateUpdate()
    {

        if (Input.mouseScrollDelta.y != 0 && GunList.Count > 1)
        {
            SwapGunManager();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BoxGun>() != null)
        {
            GunList.Add(other.GetComponent<BoxGun>().GunInBox);

            Destroy(other.gameObject);

            TakingGunManager();
        }
    }

}
