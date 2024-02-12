using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{

    [SerializeField]
    int HpMax;

    int currentHeal;

    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHeal = HpMax;
        GetComponent<Rigidbody>().isKinematic = true;

    }

    public void damagingDummy(int damage)
    {
        currentHeal -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if(currentHeal <= 0 && !dead)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
            dead = true;
            RoundManager.instance.addScore(0, 1);
        }


        if(dead)
        {
            Destroy(gameObject, 3);
        }
    }
}
