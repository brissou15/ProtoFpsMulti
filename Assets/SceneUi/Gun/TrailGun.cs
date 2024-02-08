using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGun : MonoBehaviour
{
    Rigidbody Propulse;


    
    float ThrowForce = 700;

    [HideInInspector]
    public Vector3 throwDir;


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
        //Destroy(gameObject);

    }

    private void LateUpdate()
    {
        Destroy(gameObject, 3);
    }
}
