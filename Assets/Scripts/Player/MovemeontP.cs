using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class MovemeontP : MonoBehaviour
{
    //Reference to Custom Inputs
    CustomInputs input = null;

    //assignable Vector for custom inputs
    Vector3 movVector = Vector3.zero;

    //variables
    //Player movement
    [SerializeField] float moveSpeed;
    [SerializeField] float maxFallSpee = 100f;
    float groundDrag = 4f;

    //Ground Check
    float playerHeight = 2f;
    [SerializeField]LayerMask Ground;
    bool isGrounded;

    //WIn & loss
   [SerializeField] int lives = 3;
    public bool won = false;
    public bool lost = false;
    public VisualEffect speedEffect;
    //References
    Rigidbody rb;
    [SerializeField] Camera cam;

    private void Awake()
    {
        input = new CustomInputs();
        rb = GetComponent<Rigidbody>();
        cam.fieldOfView = 80f;
    }
    private void Update()
    {
        //Groud Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        
        //Update Drag
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;

        //Controll Max Speed
        if (isGrounded)
        {
            SpeedControllOnGround();
        }
        if (!isGrounded)
        {
            SpeedControllinAir();
        }
    }

    // FOV controll
    void DynamicFOV()
    {
        
        cam.fieldOfView = (rb.velocity.magnitude > 50f) ? Mathf.Lerp(cam.fieldOfView, 120, 1f * Time.deltaTime): Mathf.Lerp(cam.fieldOfView, 80, 5f * Time.deltaTime);
        if(rb.velocity.magnitude > 50f)
        {
            speedEffect.SetFloat("Visiblity", 1);
        }
        else
        {
            speedEffect.SetFloat("Visiblity", 0);
        }

        //if (rb.velocity.magnitude >= 50)
        //    {
        //        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,120,1f *Time.deltaTime);
        //        Debug.Log("Fov Changed");

        //    }

        //    else if (rb.velocity.magnitude >= 20)
        //    {
        //        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 80,5f *Time .deltaTime);
        //    }
    }

    //Limit Speed while on platform
    void SpeedControllOnGround()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x,rb.velocity.y, limitedVelocity.z);
        }
    }

    //Limit speed in air
    void SpeedControllinAir()
    {
        Vector3 flatairvel = new Vector3(0f, rb.velocity.y, 0f);

        if(flatairvel.magnitude > maxFallSpee)
        {
            Vector3 airvel = flatairvel.normalized * maxFallSpee;
            rb.velocity = new Vector3(rb.velocity.x, airvel.y, rb.velocity.z);
        }
       
    }

    // Movement
    void OnEnable()
    {
        input.Enable();
        input.Movement.PlayerMovement.performed += OnMovementPerformed;
       
        input.Movement.PlayerMovement.canceled += OnMovementCancled;
    }

    void OnDisable()
    {
        input.Disable();
        input.Movement.PlayerMovement.performed -= OnMovementPerformed;
        input.Movement.PlayerMovement.canceled -= OnMovementCancled;

    }
    void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movVector = context.ReadValue<Vector3>();
    }
    void OnMovementCancled(InputAction.CallbackContext context)
    {
        movVector = Vector3.zero;
    }
    void PlayerMovement()
    {
        Vector3 move = transform.right * movVector.x + transform.forward * movVector.z;
        rb.AddForce(new Vector3(move.x, 0f, move.z) * moveSpeed, ForceMode.Force);
    }

    //Win and lose logic
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            lives--;
        }
        if (lives < 0)
        {
            lives = 0;
        }
        if (lives == 0) 
        {
            lost = true;
        }
        if (collision.gameObject.CompareTag("Won"))
        {
            won = true;
        }
        
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        DynamicFOV();
       
    }
    
}
