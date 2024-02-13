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
        Destroy(gameObject,1.5f);
    }

    private void OnDestroy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.GetComponent<PlayerScript>())
            {
                hitCollider.GetComponent<PlayerScript>().m_currentHealth -=  50;
            }
           
        }

        GameObject Projectile = Instantiate(BOOM, transform.position,
              Quaternion.identity);
    }
}
