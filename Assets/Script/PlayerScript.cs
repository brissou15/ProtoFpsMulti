using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_jumpForce;
    [SerializeField] private Camera m_camera;
    [SerializeField] private float m_sensitivityX;
    [SerializeField] private float m_sensitivityY;
    [SerializeField] private float m_minY = -80f;
    [SerializeField] private float m_maxY = 90f;
    private bool m_isGrounded;
    private bool m_canDoubleJump;

    private float rotationY = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateCamera();
        exitGame();
        UnlockCursor();
    }

    void UpdateMovement()
    {
        BaseMovement();
        jump();
    }
    private void BaseMovement()
    {
        Vector3 inputs = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 Velocity = transform.rotation * inputs * m_speed;
        m_rb.velocity = new Vector3(Velocity.x, m_rb.velocity.y, Velocity.z);
    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_isGrounded)
            {
                m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            }
            else if (m_canDoubleJump)
            {
                m_rb.AddForce(Vector3.up * (m_jumpForce / 1.5f), ForceMode.Impulse);
                m_canDoubleJump = false;
            }
        }

    }
    void UpdateCamera()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            float rotationX = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
            //float rotationY = transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * m_sensitivityY;
            rotationY = Mathf.Clamp(rotationY, m_minY, m_maxY);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationX, transform.eulerAngles.z);
            m_camera.transform.localEulerAngles = new Vector3 (-rotationY,0,0);
        }
    }
    void exitGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void UnlockCursor()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        m_isGrounded = true;
        m_canDoubleJump = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        m_isGrounded = false;
    }
}
