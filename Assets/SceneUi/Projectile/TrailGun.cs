using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGun : MonoBehaviour
{
    Rigidbody Propulse;



    float ThrowForce = 700;


    [SerializeField]
    int damage;

    // Start is called before the first frame update
    void Start()
    {
        Propulse = GetComponent<Rigidbody>();

        Propulse.AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("Player") /*| (1 << LayerMask.NameToLayer("Corps")*/)))
        {

            if (hit.collider.tag == "Dummy")
            {

                if (hit.collider.GetComponent<Dummy>())
                {
                    Debug.Log("touched");
                   hit.collider.GetComponent<Dummy>().damagingDummy(damage);
                    

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("PAN");
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }

    }

    private void LateUpdate()
    {
        Destroy(gameObject, 3);
    }
}
