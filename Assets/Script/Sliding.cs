using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerScript player;
    [SerializeField] private Rigidbody rb;


    [Header("Sliding")]
    [SerializeField] private float maxSlidingTime;
    [SerializeField] private float slideForce;
    private float slideTimer;
    [SerializeField] private float slideYScale;
    private float startYScale;


    [Header("Input")]
    [SerializeField] private KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;
    private bool sliding;
    // Start is called before the first frame update
    void Start()
    {
        startYScale = transform.localScale.y;
    }
    private void FixedUpdate()
    {
        if (sliding)
        {
            SlidingMovement();
        }
    }
    // Update is called once per frame
    void Update()
    {
        InputSlide();
    }

    private void InputSlide()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0) && !player.m_wallRunning)
        {
            StartSlide();
        }
        if (Input.GetKeyUp(slideKey) && sliding)
        {
            StopSlide();
        }
    }

    private void StartSlide()
    {
        sliding = true;

        transform.localScale = new Vector3(transform.localScale.x, slideYScale, transform.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = 0;
    }
    private void SlidingMovement()
    {
        Vector3 inputDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
        if (!player.OnSLope())
        {
            slideTimer += Time.deltaTime;
        }
        if (slideTimer >= maxSlidingTime)
        {
            StopSlide();
            slideTimer = 0;
        }
    }
    private void StopSlide()
    {
        sliding = false;
        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
    }
}
