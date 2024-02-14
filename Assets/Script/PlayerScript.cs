using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody))]

public class PlayerScript : MonoBehaviour
{
    [Header("References")]
    public Rigidbody m_rb;
    public Camera m_camera;
    public int m_team;
    public int m_addScore = 5;
    public int m_damageMultiplier = 1;

    [Header("Health")]
    public int m_maxHealth = 10;
    public int m_currentHealth;

    [Header("Speeds")]
    public float m_desiredSpeed;
    [SerializeField] private float m_baseSpeed;
    [SerializeField] private float m_wallRunSpeed;


    [Header("Saut")]
    [SerializeField] private float m_jumpForce;
    public bool m_isGrounded;
    public bool m_canDoubleJump;
    public float m_jumpCoolDown;
    public float m_jumpTimer;


    [Header("Camera")]
    [SerializeField] private float m_sensitivityX;
    [SerializeField] private float m_sensitivityY;
    [SerializeField] private float m_minY = -80f;
    [SerializeField] private float m_maxY = 90f;
    private float m_rotationY = 0f;
    private Vector2 m_rotation = new Vector2();

    [Header("Inputs")]
    [SerializeField] private KeyCode JumpKey;
    [SerializeField] private float deadZone = 0.3f;
    //[SerializeField] private KeyCode ForwardKey;
    //[SerializeField] private KeyCode BackwardKey;
    //[SerializeField] private KeyCode LeftKey;
    //[SerializeField] private KeyCode RightKey;

    [Header("States")]
    public MovementState m_state;
    public bool m_sliding;
    public bool m_crouching;
    public bool m_wallRunning;

    [Header("Slope Handling")]
    [SerializeField] private float m_maxSlopAngle;
    private RaycastHit slopeHit;

    [Header("MultiLocal")]
    public CONTROLER controler;//gère si clavier ou souris
    public Gamepad MyControler;

    [Header("Death")]
    public bool dead = false;
    public GameObject Boom;

    [Header("Bonus")]
    public bonusType bonusType = bonusType.Nothing;
    public float damageMultiplier = 1;
    public float bonusDuration = 0;
    private float bonusTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_desiredSpeed = m_baseSpeed;
        m_currentHealth = m_maxHealth;
    }
    private void Awake()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            vieManager();
            UpdateMovement();
            UpdateCamera();
            updateBonus();
        }
        exitGame();
        UnlockCursor();
    }

    void UpdateMovement()
    {
        BaseMovement();
        jump();
    }

    public void ResetHp()
    {
        m_currentHealth = m_maxHealth;
    }

    private void vieManager()
    {
        if(m_currentHealth <= 0)
        {
            RoundManager.instance.addScore(Mathf.Abs(m_team - 1), m_addScore);
            GameObject Object = Instantiate(Boom, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void BaseMovement()
    {
        //pour le splitScreen
        Vector3 inputs = new Vector3();
        if (controler == CONTROLER.CLAVIER)
        {
            if(!m_wallRunning)
            {
                if (Input.GetKey(KeyCode.A))
                    inputs.x = -1f;
                else if (Input.GetKey(KeyCode.D))
                    inputs.x = 1f;
                else
                    inputs.x = 0f;
            }
            if (Input.GetKey(KeyCode.W))
                inputs.z = 1f;
            else if (Input.GetKey(KeyCode.S))
                inputs.z = -1f;
            else
                inputs.z = 0f;
        }
        else if (MyControler!=null)
        {
            if (MyControler != null)
            {
                if(!m_wallRunning)
                {
                    if (Mathf.Abs(MyControler.leftStick.ReadValue().x) > deadZone)
                    {
                        inputs.x = MyControler.leftStick.ReadValue().x;
                    }
                }
                
                if (Mathf.Abs(MyControler.leftStick.ReadValue().y) > deadZone)
                {
                    inputs.y = MyControler.leftStick.ReadValue().y;
                }
            }
        }

        //Input normaux
        //Vector3 inputs = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //Vector3 Velocity = transform.localRotation * inputs * m_desiredSpeed;
        inputs.Normalize();
        if (controler == CONTROLER.CLAVIER)
        {
            Vector3 Velocity = transform.localRotation * inputs * m_desiredSpeed;
            m_rb.velocity = new Vector3(Velocity.x, m_rb.velocity.y, Velocity.z);
        }
        else if(MyControler != null)
        {
            m_rb.velocity = transform.localRotation * new Vector3(inputs.x * m_desiredSpeed, m_rb.velocity.y, inputs.y * m_desiredSpeed);
            //m_rb.velocity = Velocity;
        }
        //m_rb.velocity = new Vector3(Velocity.x, m_rb.velocity.y, Velocity.z);
        //voir pout utiliser un add force au lieu de la velocité 
    }
    void jump()
    {
        bool canJump = false;
        m_jumpTimer += Time.deltaTime;
        if (controler == CONTROLER.CLAVIER)
        {
            if (!m_wallRunning)
            {
                canJump = Input.GetKeyDown(JumpKey);
            }
        }
        else if(MyControler != null)
        {
            if (m_jumpTimer > 0.2 && !m_wallRunning)
            {
                canJump = MyControler.buttonSouth.IsPressed();
                if (canJump)
                {
                    m_jumpTimer = 0;
                }
            }
        }

        if (canJump)
        {
            if (m_isGrounded)
            {
                m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);

            }
            else if (m_canDoubleJump)
            {
                m_rb.velocity = new Vector3(m_rb.velocity.x, 0, m_rb.velocity.z);
                m_rb.AddForce(Vector3.up * (m_jumpForce / 1.5f), ForceMode.Impulse);
                m_canDoubleJump = false;
            }
        }
    }
    void UpdateCamera()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (controler == CONTROLER.CLAVIER)
            {
                m_rotation.x = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivityX;
                m_rotation.y += Input.GetAxis("Mouse Y") * m_sensitivityY;
            }
            else
            {
                if (MyControler != null)
                {
                    if (Mathf.Abs(MyControler.rightStick.ReadValue().x) > deadZone)
                    {
                        m_rotation.x = transform.rotation.eulerAngles.y + MyControler.rightStick.ReadValue().x * m_sensitivityX;
                    }
                    if (Mathf.Abs(MyControler.rightStick.ReadValue().y) > deadZone)
                    {
                        m_rotation.y += MyControler.rightStick.ReadValue().y * m_sensitivityY;
                    }
                }
            }

            m_rotation.y = Mathf.Clamp(m_rotation.y, m_minY, m_maxY);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_rotation.x, transform.eulerAngles.z);
            m_camera.transform.localEulerAngles = new Vector3(-m_rotation.y, 0, m_camera.transform.localEulerAngles.z);

            if (false)
            {
                float rotationX = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivityX;
                m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
                //float rotationY = transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * m_sensitivityY;
                m_rotationY = Mathf.Clamp(m_rotationY, m_minY, m_maxY);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationX, transform.eulerAngles.z);
                m_camera.transform.localEulerAngles = new Vector3(-m_rotationY, 0, m_camera.transform.localEulerAngles.z);
            }
        }
    }

    void stateManager()
    {
        if (m_wallRunning)
        {
            m_desiredSpeed = m_wallRunSpeed;
            m_state = MovementState.wallrunning;
        }
        else
        {
            m_state = MovementState.walking;
            m_desiredSpeed = m_baseSpeed;
        }
    }
    void exitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void UnlockCursor()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void updateBonus()
    {
        if(bonusType!=bonusType.Nothing)
        {
            bonusTimer += Time.deltaTime;
            if (bonusTimer >= bonusDuration)
            {
                bonusTimer = 0;
                //reset de toutes les variable modificatrice de stats qui se trouve sous le header Bonus
                damageMultiplier = 1;
            }
        }        
    }
    private void OnCollisionStay(Collision collision)
    {
        int GroundLayer = LayerMask.NameToLayer("Ground");
        if (collision.gameObject.layer == GroundLayer)
        {
            m_isGrounded = true;
            m_canDoubleJump = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        m_isGrounded = false;
    }

    public bool OnSLope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, transform.lossyScale.y * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            Debug.Log(angle);
            return Mathf.Abs(angle) < m_maxSlopAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopMoveDirection()
    {
        Vector3 moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}

public enum MovementState
{
    walking,
    sprinting,
    wallrunning,
    crouching,
    sliding,
    air
}
