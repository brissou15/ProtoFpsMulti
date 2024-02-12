using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeProj : MonoBehaviour
{
    [SerializeField]
    GameObject BOOM;

    Rigidbody Propulse;


    [SerializeField]
    float ThrowForce;
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

    private void OnDestroy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.GetComponent<Dummy>())
            {
                hitCollider.GetComponent<Dummy>().damagingDummy(100);
            }
           
        }

        GameObject Projectile = Instantiate(BOOM, transform.position,
              Quaternion.identity);
    }
}
