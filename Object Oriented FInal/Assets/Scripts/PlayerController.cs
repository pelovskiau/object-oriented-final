using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private float horizontalInput;
    private float verticalInput;
    private float walkSpeed = 1200f;
    private float gravityMod = 1f;
    private float jumpForce = 1000f;
    private float regularWalk = 1200f;
    private GameObject menu;
    private bool isGrounded = true;
    private bool isPaused = false;
    private bool isCrouched = false;
    private bool isRunning = false;
    private bool isWalking = true;
    private float runSpeed;
    private float crouchSpeed;
    private RaycastHit currentHit;
    private bool isLookingAtInteractable = false;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float mouseSensitivity = 100f;
    private float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crouchSpeed = walkSpeed / 2;
        runSpeed = walkSpeed* 2;
        playerRB = GetComponent<Rigidbody>();
        menu = GameObject.Find("Menu");
        Physics.gravity *= gravityMod;
        cameraHolder = GameObject.Find("Main Camera").transform;
        playerRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        playerRB.linearDamping = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        Look();
        if (Input.GetAxisRaw("Vertical")!=0)
        {
            Debug.Log("Forward/back");
            Walk();
        }
        if (Input.GetAxisRaw("Horizontal")!=0)
        {
            Strafe();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Sprint();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            Crouch();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Use();
        }

    }
    private void Walk()
    {
        verticalInput = Input.GetAxis("Vertical");
        playerRB.AddForce(transform.forward * verticalInput *walkSpeed, ForceMode.Force);
    }
    private void Strafe()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        playerRB.AddForce(transform.right * horizontalInput* walkSpeed, ForceMode.Force);
    }
    private void Jump()
    {
        if (isGrounded) 
        {
            isGrounded = false;
            playerRB.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse); 
        }
            
    }
    private void Pause()
    {
        if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0;//stops time and enables the menu.
            menu.SetActive(true);
        }
        else if (isPaused == true) 
        {
            Time.timeScale = 1;//resumes and disables the menu
            menu.SetActive(false);
        }

    }
    private void Use()
    {
        Debug.Log("Gotta do the raycasting and interaction here");
    }
    private void Crouch() 
    {
        if (isWalking == true && isCrouched==false && isRunning==false)
        {
            isWalking = false;
            isCrouched = true;
            walkSpeed = crouchSpeed;
        }
        if (isWalking == false && isCrouched == true) 
        {
            isWalking = true;
            isCrouched=false;
            walkSpeed = regularWalk;
        }
    }

    private void Sprint() 
    {
        if (isWalking == true && isCrouched == false && isRunning == false)
        {
            isWalking = false;
            isRunning = true;
            walkSpeed = runSpeed;
        }
        if (isWalking == false && isRunning == true)
        {
            isWalking = true;
            isRunning = false;
            walkSpeed = regularWalk;
        }
    }
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation - clamp to prevent flipping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation
        transform.Rotate(Vector3.up * mouseX);

        // Raycast each frame
        LookRayCast();
    }
    private void LookRayCast()
    {
        Ray ray = new Ray(cameraHolder.position, cameraHolder.forward);
        float range = 5f;

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
            Debug.Log("Looking at: " + hit.collider.name);
            // Store the target if needed
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { 
            isGrounded = true;
        }
    }


}
