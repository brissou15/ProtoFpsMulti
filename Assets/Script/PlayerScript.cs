using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private float m_speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.velocity = new Vector3(Input.GetAxis("Horizontal") * m_speed, 0, Input.GetAxis("Vertical") * m_speed);
    }
}
