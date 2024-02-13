using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiGun : MonoBehaviour
{

    [SerializeField]
    GameObject CamUi;

    [SerializeField]
    public List<WeaponSo> GunList = new List<WeaponSo>();

    [SerializeField] private PlayerScript player;


    List<int> AmmoReserve = new List<int>();
    public int currentAmmo = 0;
    public int MaxAmmo = 0;
    GameObject Gun = null;

    int GunEquipedId = 0;

    float TimerShoot = 0;
    float keyTimer;

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
            AmmoReserve.Add(GunList[GunEquipedId].magazineSize);
        }
    }

    void Update()
    {
        TimerShoot += Time.deltaTime;
        keyTimer += Time.deltaTime;
        ShootAndGunManager();

    }

    void ShootAndGunManager()
    {
        if (Gun != null)
        {
            int typeShootTmp;
            if (player.controler == CONTROLER.CLAVIER)
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
                {

                    if (Input.GetMouseButton(0))
                    {
                        typeShootTmp = 0;
                    }
                    else
                    {
                        typeShootTmp = 1;
                    }

                    CurrentWeaponD = Gun.GetComponent<GunScript>().weaponDetaille;

                    // Debug.Log(CurrentWeaponAnim.GetFloat("TimerShootOui"));
                    if (typeShootTmp < CurrentWeaponD.ShootList.Length && AmmoReserve[GunEquipedId] > 0)
                    {

                        if (TimerShoot >= 1 / CurrentWeaponD.ShootList[typeShootTmp].fireRate)
                        {
                            shooting(CurrentWeaponD, typeShootTmp);
                            --AmmoReserve[GunEquipedId];
                            TimerShoot = 0;

                        }
                    }
                }
            }
            else
            {
                if (player.MyControler.rightTrigger.isPressed || player.MyControler.leftTrigger.isPressed)
                {

                    if (player.MyControler.rightTrigger.isPressed)
                    {
                        typeShootTmp = 0;
                    }
                    else
                    {
                        typeShootTmp = 1;
                    }

                    CurrentWeaponD = Gun.GetComponent<GunScript>().weaponDetaille;

                    // Debug.Log(CurrentWeaponAnim.GetFloat("TimerShootOui"));
                    if (typeShootTmp < CurrentWeaponD.ShootList.Length && AmmoReserve[GunEquipedId] > 0)
                    {

                        if (TimerShoot >= 1 / CurrentWeaponD.ShootList[typeShootTmp].fireRate)
                        {
                            shooting(CurrentWeaponD, typeShootTmp);
                            --AmmoReserve[GunEquipedId];
                            TimerShoot = 0;

                        }
                    }
                }
            }

        }
    }

    Quaternion Calcul(int angleRecup)
    {
        Vector3 machin = Random.insideUnitSphere * angleRecup;

        return Quaternion.Euler(machin.x, machin.y, machin.z) * CamUi.transform.rotation;
    }


    private void shooting(WeaponSo ModelWeapon, int typeTir)
    {
        for (int i = 0; i < ModelWeapon.ShootList[typeTir].numberBallSprea; i++)
        {

            GameObject Projectile = Instantiate(ModelWeapon.ShootList[typeTir].PrefabProjectile, CamUi.transform.position + CamUi.transform.right * 0.1f + -CamUi.transform.up * 0.2f,
               Calcul(ModelWeapon.ShootList[typeTir].AngleShoot));


        }


    }

    void SwapGunManager()
    {
        Destroy(Gun);
        if (player.controler == CONTROLER.CLAVIER)
        {
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
        }
        else
        {
            if (player.MyControler.buttonNorth.isPressed && keyTimer > 0.2)
            {
                GunEquipedId++;
                if (GunEquipedId >= GunList.Count)
                {
                    GunEquipedId = 0;
                }
                keyTimer = 0;
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
        AmmoReserve.Add(GunList[GunEquipedId].magazineSize);

    }

    // Update is called once per frame


    private void LateUpdate()
    {

        if (Input.mouseScrollDelta.y != 0 && GunList.Count > 1)
        {
        }
            SwapGunManager();
        MaxAmmo = GunList[GunEquipedId].magazineSize;
        currentAmmo = AmmoReserve[GunEquipedId];
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
