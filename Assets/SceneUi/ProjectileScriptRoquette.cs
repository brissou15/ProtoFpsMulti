using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScriptRoquette : MonoBehaviour
{

    Rigidbody Propulse;

    [SerializeField]
    ParticleSystem Particule;

    [SerializeField]
    float ThrowForce;

    [HideInInspector]
    public Vector3 throwDir;

    [SerializeField]
    bool Explode;

    [SerializeField]
    float explosionRadius;

    [SerializeField]
    float explosionForce;

    [HideInInspector]
    public string LayerName;

    // Start is called before the first frame update
    void Start()
    {
        Propulse = GetComponent<Rigidbody>();

        Propulse.AddForce(throwDir * ThrowForce, ForceMode.Impulse);
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
        Destroy(gameObject,5);
    }


}
