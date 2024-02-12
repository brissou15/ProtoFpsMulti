using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGun : MonoBehaviour
{
    Rigidbody Propulse;



    float ThrowForce = 700;




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
                   hit.collider.GetComponent<Dummy>().damagingDummy(5);
                    

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);

    }

    private void LateUpdate()
    {
        Destroy(gameObject, 3);
    }
}
