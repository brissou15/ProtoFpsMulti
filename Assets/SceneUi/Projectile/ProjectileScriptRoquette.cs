using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScriptRoquette : MonoBehaviour
{

    Rigidbody Propulse;

    [SerializeField]
    GameObject Trail;

    [SerializeField]
    float ThrowForce;


    [HideInInspector]
    public string LayerName;

    // Start is called before the first frame update
    void Start()
    {
        Propulse = GetComponent<Rigidbody>();
       
        Propulse.AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
       
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer != LayerMask.NameToLayer(LayerName) && other.gameObject.tag !="Projectile" )
        //{
        //    ParticleSystem Projectile = Instantiate(Particule, transform.position, Quaternion.identity);

        //    if (Explode)
        //    {
        //        if (LayerName != "Ennemis")
        //        {
        //            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        //            foreach (var hitCollider in hitColliders)
        //            {
        //                if (hitCollider.tag == "Ennemis" && LayerName == "Player")
        //                {
        //                    //hitCollider.GetComponentInParent<EnnemisScript>().Die();
        //                   hitCollider.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
        //                }
        //            }
        //        }
        //    }

        //    if (other.tag == "Ennemis")
        //    {
        //        //hit.collider.GetComponent<EnnemisScript>().
        //    }

        //    Destroy(gameObject);

        //}
    }
    private void LateUpdate()
    {
        Destroy(gameObject,0.2f);
    }
    Quaternion Calcul(int angleRecup)
    {
        Vector3 machin = Random.insideUnitSphere * angleRecup;

        return Quaternion.Euler(machin.x, machin.y, machin.z) * transform.rotation;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < 20; i++)
        {

            GameObject Projectile = Instantiate(Trail, transform.position,
               Calcul(15));

            //Projectile.GetComponent<ProjectileScriptRoquette>().throwDir = CamUi.transform.forward;
            //Projectile.transform.localRotation = CamUi.transform.rotation;
            //Projectile.GetComponent<ProjectileScriptRoquette>().LayerName = LayerMask.LayerToName(gameObject.layer);
            //Debug.Log(LayerMask.LayerToName(gameObject.layer));

        }
    }

}
