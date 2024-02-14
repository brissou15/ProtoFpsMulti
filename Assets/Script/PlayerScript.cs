using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody))]

public class PlayerScript : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Camera camera;
    public int team;
    public int addScore = 5;

    [Header("Health")]
    public int maxHealth = 10;
    public int currentHealth;

    [Header("Speeds")]
    public float desiredSpeed;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float wallRunSpeed;


    [Header("Saut")]
    [SerializeField] private float jumpForce;
    public bool isGrounded;
    public bool canDoubleJump;
    public float jumpCoolDown;
    public float jumpTimer;


    [Header("Camera")]
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    [SerializeField] private float minY = -80f;
    [SerializeField] private float maxY = 90f;
    private float rotationY = 0f;
    private Vector2 rotation = new Vector2();

    [Header("Inputs")]
    [SerializeField] private KeyCode JumpKey;
    [SerializeField] private float deadZone = 0.3f;
    //[SerializeField] private KeyCode ForwardKey;
    //[SerializeField] private KeyCode BackwardKey;
    //[SerializeField] private KeyCode LeftKey;
    //[SerializeField] private KeyCode RightKey;

    [Header("States")]
    public MovementState state;
    public bool sliding;
    public bool crouching;
    public bool wallRunning;

    [Header("Slope Handling")]
    [SerializeField] private float maxSlopAngle;
    private RaycastHit slopeHit;

    [Header("MultiLocal")]
    public CONTROLER controler;//gère si clavier ou souris
    public Gamepad MyControler;

    [Header("Death")]
    public bool dead = false;
    public GameObject Boom;

    [Header("Bonus")]
    public string bonusName;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        desiredSpeed = baseSpeed;
        currentHealth = maxHealth;
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
        currentHealth = maxHealth;
    }

    private void vieManager()
    {
        if(currentHealth <= 0)
        {
            RoundManager.instance.addScore(Mathf.Abs(team - 1), addScore);
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
            if(!wallRunning)
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
                if(!wallRunning)
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

        //Vector3 Velocity = transform.localRotation * inputs * desiredSpeed;
        inputs.Normalize();
        if (controler == CONTROLER.CLAVIER)
        {
            Vector3 Velocity = transform.localRotation * inputs * desiredSpeed;
            rb.velocity = new Vector3(Velocity.x, rb.velocity.y, Velocity.z);
        }
        else if(MyControler != null)
        {
            rb.velocity = transform.localRotation * new Vector3(inputs.x * desiredSpeed, rb.velocity.y, inputs.y * desiredSpeed);
            //rb.velocity = Velocity;
        }
        //rb.velocity = new Vector3(Velocity.x, rb.velocity.y, Velocity.z);
        //voir pout utiliser un add force au lieu de la velocité 
    }
    void jump()
    {
        bool canJump = false;
        jumpTimer += Time.deltaTime;
        if (controler == CONTROLER.CLAVIER)
        {
            if (!wallRunning)
            {
                canJump = Input.GetKeyDown(JumpKey);
            }
        }
        else if(MyControler != null)
        {
            if (jumpTimer > 0.2 && !wallRunning)
            {
                canJump = MyControler.buttonSouth.IsPressed();
                if (canJump)
                {
                    jumpTimer = 0;
                }
            }
        }

        if (canJump)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * (jumpForce / 1.5f), ForceMode.Impulse);
                canDoubleJump = false;
            }
        }
    }
    void UpdateCamera()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (controler == CONTROLER.CLAVIER)
            {
                rotation.x = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
                rotation.y += Input.GetAxis("Mouse Y") * sensitivityY;
            }
            else
            {
                if (MyControler != null)
                {
                    if (Mathf.Abs(MyControler.rightStick.ReadValue().x) > deadZone)
                    {
                        rotation.x = transform.rotation.eulerAngles.y + MyControler.rightStick.ReadValue().x * sensitivityX;
                    }
                    if (Mathf.Abs(MyControler.rightStick.ReadValue().y) > deadZone)
                    {
                        rotation.y += MyControler.rightStick.ReadValue().y * sensitivityY;
                    }
                }
            }

            rotation.y = Mathf.Clamp(rotation.y, minY, maxY);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation.x, transform.eulerAngles.z);
            camera.transform.localEulerAngles = new Vector3(-rotation.y, 0, camera.transform.localEulerAngles.z);

            //if (false)
            //{
            //    float rotationX = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            //    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            //    //float rotationY = transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * sensitivityY;
            //    rotationY = Mathf.Clamp(rotationY, minY, maxY);
            //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationX, transform.eulerAngles.z);
            //    camera.transform.localEulerAngles = new Vector3(-rotationY, 0, camera.transform.localEulerAngles.z);
            //}

            //
        }
    }

    void stateManager()
    {
        if (wallRunning)
        {
            desiredSpeed = wallRunSpeed;
            state = MovementState.wallrunning;
        }
        else
        {
            state = MovementState.walking;
            desiredSpeed = baseSpeed;
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
    private void OnCollisionStay(Collision collision)
    {
        int GroundLayer = LayerMask.NameToLayer("Ground");
        if (collision.gameObject.layer == GroundLayer)
        {
            isGrounded = true;
            canDoubleJump = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public bool OnSLope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, transform.lossyScale.y * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            Debug.Log(angle);
            return Mathf.Abs(angle) < maxSlopAngle && angle != 0;
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
