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


    public List<int> AmmoReserve = new List<int>();
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
        ResetGun();
    }

    void Update()
    {
        TimerShoot += Time.deltaTime;
        keyTimer += Time.deltaTime;
        ShootAndGunManager();

    }

    public void ResetGun()
    {
        Destroy(Gun);
        GunEquipedId = 0;

        if (GunList.Count > 1)
        {
            for (int i = 1; i < GunList.Count;)
            {
                GunList.Remove(GunList[i]);
                AmmoReserve.Clear();
            }
        }

        if (GunList.Count > 0)
        {
            Gun = Instantiate(GunList[GunEquipedId].Gun);
            Gun.GetComponent<GunScript>().GunEquiped = true;
            Gun.transform.SetParent(CamUi.transform, false);
            InitaliseGun();
            AmmoReserve.Add(GunList[GunEquipedId].magazineSize);
        }
    }

    void InitaliseGun()
    {
        foreach (Transform g in Gun.GetComponentsInChildren<Transform>())
        {
            g.gameObject.layer = CamUi.layer;

        }
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

                    if (AmmoReserve[GunEquipedId] > 0)
                    {
                        if (typeShootTmp < CurrentWeaponD.ShootList.Length)
                        {

                            if (TimerShoot >= 1 / CurrentWeaponD.ShootList[typeShootTmp].fireRate)
                            {
                                shooting(CurrentWeaponD, typeShootTmp);
                                --AmmoReserve[GunEquipedId];
                                TimerShoot = 0;

                            }
                        }
                    }
                    else
                    {
                        ResetGun();
                    }
                }
            }
            else if (player.MyControler!=null)
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
                    if (AmmoReserve[GunEquipedId] > 0)
                    {
                        if (typeShootTmp < CurrentWeaponD.ShootList.Length)
                        {

                            if (TimerShoot >= 1 / CurrentWeaponD.ShootList[typeShootTmp].fireRate)
                            {
                                shooting(CurrentWeaponD, typeShootTmp);
                                --AmmoReserve[GunEquipedId];
                                TimerShoot = 0;

                            }
                        }
                    }
                    else
                    {
                        ResetGun();
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
        else if (player.MyControler!=null)
        {

            GunEquipedId++;
            if (GunEquipedId >= GunList.Count)
            {
                GunEquipedId = 0;
            }


        }

        Gun = Instantiate(GunList[GunEquipedId].Gun);
        Gun.GetComponent<GunScript>().GunEquiped = true;
        Gun.transform.SetParent(CamUi.transform, false);
        InitaliseGun();
        //CurrentWeaponAnim = Gun.GetComponent<GunScript>().AnimeGun;
        CurrentWeaponD = Gun.GetComponent<GunScript>().weaponDetaille;

    }
    void TakingGunManager()
    {
        //if (GunList.Count == 1)
        {
            //canShoot = false;
            Destroy(Gun);
            Gun = Instantiate(GunList[GunList.Count - 1].Gun);
            Gun.GetComponent<GunScript>().GunEquiped = true;
            Gun.transform.SetParent(CamUi.transform, false);
            InitaliseGun();
            //CurrentWeaponAnim = Gun.GetComponent<GunScript>().AnimeGun;
            CurrentWeaponD = Gun.GetComponent<GunScript>().weaponDetaille;
            GunEquipedId++;
            AmmoReserve.Add(GunList[GunEquipedId].magazineSize);
            Debug.Log(AmmoReserve[GunEquipedId]);
            

        }
    }

    // Update is called once per frame


    private void LateUpdate()
    {
        if (player.controler == CONTROLER.CLAVIER)
        {
            if (Input.mouseScrollDelta.y != 0 && GunList.Count > 1)
            {
                SwapGunManager();
            }
        }
        else
        {
            if (keyTimer > 0.2 && player.MyControler.buttonNorth.isPressed)
            {
                SwapGunManager();
                keyTimer = 0;
            }
        }

        MaxAmmo = GunList[GunEquipedId].magazineSize;
        currentAmmo = AmmoReserve[GunEquipedId];
    }
    bool DejaEquiped = false;

    private void OnCollisionEnter(Collision other)
    {
        bool check = false;
        int index = 0;
        WeaponSo newGun;
        if (other.collider.GetComponent<BoxGun>() != null)
        {
            newGun = other.collider.GetComponent<BoxGun>().GunInBox;

            for (int i = 0; i < GunList.Count; i++)
            {

                if (GunList[i].weaponName == newGun.weaponName)
                {
                    check = true;

                    AmmoReserve[i] = GunList[i].magazineSize;
                    break;
                }
                //if (GunList[i].weaponName == "ShotGun" || GunList[i].weaponName == "Snipe")
                //{
                //    DejaEquiped = true;
                //    AmmoReserve[i] = GunList[i].magazineSize;
                //}
            }
            if (!check)
            {
                if (GunList.Count == 1)
                {

                    GunList.Add(newGun);

                    TakingGunManager();
                }
                else
                {
                    GunList[1] = newGun;
                    Destroy(Gun);
                    Gun = Instantiate(GunList[GunEquipedId].Gun);
                    Gun.GetComponent<GunScript>().GunEquiped = true;
                    Gun.transform.SetParent(CamUi.transform, false);
                    InitaliseGun();
                    //CurrentWeaponAnim = Gun.GetComponent<GunScript>().AnimeGun;
                    CurrentWeaponD = Gun.GetComponent<GunScript>().weaponDetaille;
                    AmmoReserve[1] = GunList[1].magazineSize;

                }
            }

            //if (!DejaEquiped)
            //{

            //    GunList.Add(newGun);

            //    TakingGunManager();
            //}
            Destroy(other.gameObject);

        }

    }

}
