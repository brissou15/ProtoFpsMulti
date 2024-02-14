using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGun : MonoBehaviour
{
    Rigidbody Propulse;

   public GameObject Particuleobject;


    float ThrowForce = 1500;


    int damage;

    // Start is called before the first frame update
    void Start()
    {

        damage = GetComponent<StatProjManager>().DamageRecup;

        Propulse = GetComponent<Rigidbody>();

        Propulse.AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~/*(1 << LayerMask.NameToLayer("Player") |*/ (1 << LayerMask.NameToLayer("Projectile"))))
        {


            if (hit.collider.GetComponent<PlayerScript>())
            {
                Debug.Log("touched");
                hit.collider.GetComponent<PlayerScript>().m_currentHealth -= damage;

                GameObject Object = Instantiate(Particuleobject, hit.point, Quaternion.identity);
                //hit.collider.GetComponent<Dummy>().damagingDummy(damage);
                //hit.point

            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            //Destroy(gameObject);
        }

    }

    private void LateUpdate()
    {
        Destroy(gameObject, 3);
    }
}
