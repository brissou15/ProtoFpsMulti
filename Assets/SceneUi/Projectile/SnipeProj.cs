using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeProj : MonoBehaviour
{
    [SerializeField]
    GameObject BOOM;

    Rigidbody Propulse;

    int damage;

    [SerializeField]
    float ThrowForce;
    // Start is called before the first frame update
    void Start()
    {
        Propulse = GetComponent<Rigidbody>();

        Propulse.AddForce(transform.forward * ThrowForce, ForceMode.Impulse);

        damage = GetComponent<StatProjManager>().DamageRecup;
    }

    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,1.5f);

        timer+= Time.deltaTime;
    }



    private void OnTriggerEnter(Collider other)
    {

        if(other.tag != "SnipeProj" &&  timer> 0.03)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnDestroy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 6);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.GetComponent<PlayerScript>())
            {
                hitCollider.GetComponent<PlayerScript>().m_currentHealth -= damage;

            }
           
        }

        GameObject Projectile = Instantiate(BOOM, transform.position,
              Quaternion.identity);
    }
}
