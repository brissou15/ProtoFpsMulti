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

    // Start is called before the first frame update
    void Start()
    {
        if (GunList.Count > 0)
        {
            //Gun = Instantiate(GunList[GunEquipedId].Gun);
            //Gun.GetComponent<GunScript>().GunEquiped = true;
            //Gun.transform.SetParent(CamUi.transform, false);
            //Gun.transform.position = GunList[0].posForUi;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
