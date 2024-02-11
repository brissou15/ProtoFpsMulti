using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Wallrunning")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallRunForce;
    [SerializeField] private float WallJumpUpForce = 20;
    [SerializeField] private float WallJumpSideForce = 20;
    [SerializeField] private float maxWallRunTime = 2f;
    [SerializeField] private float leaningOnWall = 10;
    [SerializeField] private float wallClimbSpeed;
    private float wallRunTimer;

    [Header("Input")]
    [SerializeField] private KeyCode upwardRunkey = KeyCode.LeftShift;
    [SerializeField] private KeyCode downwardsRunkey = KeyCode.LeftControl;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    private bool upwardRunning;
    private bool downwardsRunning;
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    //[SerializeField] private Transform orientation;
    //[SerializeField] private PlayerMovementAdvanced pm;
    [SerializeField] private PlayerScript player;
    [SerializeField] private Rigidbody rb;

    [Header("Exiting")]
    [SerializeField] private float exitWallTime;
    private bool exitingWall;
    private float exitWallTimer;

    [Header("Gravity")]
    [SerializeField] private bool useGravity;
    [SerializeField] private float gravityCounterForce;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        checkWall();
    }

    private void FixedUpdate()
    {
        if (player.wallRunning)
        {
            wallRunTimer += Time.fixedDeltaTime;
            WallRunningMovement();
        }
    }

    private void checkWall()
    {
        Vector3 vectorRight = Vector3.Scale(transform.right, transform.localEulerAngles);
        Vector3 vectorLeft = Vector3.Scale(-transform.right, transform.localEulerAngles);
        wallRight = Physics.Raycast(transform.position, transform.right, out rightWallHit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -transform.right, out leftWallHit, wallCheckDistance, whatIsWall);
        Debug.DrawRay(transform.position, transform.right, Color.green);
        Debug.DrawRay(transform.position, -transform.right, Color.green);
    }


    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        verticalInput = Input.GetAxis("Vertical");
        if (!player.wallRunning)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        upwardRunning = Input.GetKey(upwardRunkey);
        downwardsRunning = Input.GetKey(downwardsRunkey);


        // State WallRun
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && wallRunTimer < maxWallRunTime && !exitingWall)
        {
            //le début du wall run
            StartWallRun();
            //WallJump
            if (Input.GetKeyDown(jumpKey))
            {
                WallJump();
            }
        }
        else if (exitingWall)
        {
            if (player.wallRunning)
            {
                StopWallRun();
            }
            exitWallTimer += Time.deltaTime;
            if (exitWallTimer > exitWallTime)
            {
                exitingWall = false;
                exitWallTimer = 0;
            }
        }
        else if (wallRunTimer >= maxWallRunTime)
        {
            //WallJump();
            exitingWall = true;
        }
        else
        {
            if (player.wallRunning)
                StopWallRun();
        }
    }

    private void StartWallRun()
    {
        player.wallRunning = true;
        Camera camera = player.m_camera;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);//permet de garder la velocité et d'empêcher le perso de tomber
        if (wallLeft)
        {            
            player.m_camera.transform.localEulerAngles = new Vector3(player.m_camera.transform.localEulerAngles.x, player.m_camera.transform.localEulerAngles.y, transform.localEulerAngles.z - leaningOnWall);
        }
        else if (wallRight)
        {            
            player.m_camera.transform.localEulerAngles = new Vector3(player.m_camera.transform.localEulerAngles.x, player.m_camera.transform.localEulerAngles.y, transform.localEulerAngles.z + leaningOnWall);
        }
    }

    private void WallRunningMovement()
    {
        //rb.useGravity = false;
        rb.useGravity = useGravity;
        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);//permet de garder la velocité et d'empêcher le perso de tomber
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        // Inverse le vecteur avant du mur si nécessaire pour s'assurer que le joueur aille toujours devant lui
        if ((transform.forward - wallForward).magnitude > (transform.forward + wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        //force pour avancer
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        //force d'attirance vers le mur
        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }


        //Monter et descente 
        if (upwardRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        }
        if (downwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);
        }


        //affaiblir la la gravité
        if (useGravity)
        {
            rb.AddForce(Vector3.up * gravityCounterForce, ForceMode.Force);
        }
    }

    private void StopWallRun()
    {
        player.wallRunning = false;
        rb.useGravity = true;
        wallRunTimer = 0;
        player.m_camera.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, 0);
        if (wallLeft)
        {
            rb.AddForce(Vector3.right * WallJumpSideForce, ForceMode.Impulse);
        }
        else if (wallRight)
        {
            rb.AddForce(Vector3.left * WallJumpSideForce, ForceMode.Impulse);
        }
    }
    private void WallJump()
    {
        exitingWall = true;

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 forceToApply = transform.up * WallJumpUpForce + wallNormal * WallJumpSideForce;

        //add force
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }   
}
