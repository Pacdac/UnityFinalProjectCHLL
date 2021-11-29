using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I use Physics.gravity a lot instead of Vector3.up because you can point the gravity to a different direction and i want the controller to work fine
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private static Vector3 vecZero = Vector3.zero;
    private Rigidbody rb;

    private bool enableMovement = true;

    public static GameObject carriableObject;

    [Header("Movement properties")]
    public float crouchSpeed = 3.0f;
    public float walkSpeed = 8.0f;
    public float runSpeed = 12.0f;
    public float changeInStageSpeed = 10.0f; // Lerp from walk to run and backwards speed
    public float maximumPlayerSpeed = 150.0f;
    [HideInInspector] public float vInput, hInput;
    public Transform groundChecker;
    public float groundCheckerDist = 0.2f;

    [Header("Jump")]
    public float jumpForce = 500.0f;
    public float jumpCooldown = 1.0f;
    private bool jumpBlocked = false;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private bool isGrounded = false;
    private GameObject carriedObject;
    public bool IsGrounded { get { return isGrounded; } }

    private Vector3 inputForce;
    private int i = 0;
    private float prevY;

    private void Update()
    {
        if (Input.GetKeyDown(KeyManager.Carry))
        {
            if (CameraRaycast.isCarriable && carriedObject == null)
            {
                carriableObject.transform.parent = this.gameObject.transform;
                carriableObject.GetComponent<Rigidbody>().isKinematic = true;
                carriableObject.transform.localPosition = new Vector3(0f, 0.5f, 1f);
                carriedObject = carriableObject;
            }

            else if (carriedObject != null)
            {
                carriedObject.transform.SetParent(null);
                carriedObject.GetComponent<Rigidbody>().isKinematic = false;
                carriedObject = null;
            }
        }
    }

    private void FixedUpdate()
    {

        isGrounded = (Mathf.Abs(rb.velocity.y - prevY) < .1f) && (Physics.OverlapSphere(groundChecker.position, groundCheckerDist).Length > 1); // > 1 because it also counts the player
        prevY = rb.velocity.y;

        // Input
        vInput = Input.GetAxisRaw("Vertical");
        hInput = Input.GetAxisRaw("Horizontal");

        // Clamping speed
        rb.velocity = ClampMag(rb.velocity, maximumPlayerSpeed);

        if (!enableMovement)
            return;
        if (Input.GetKey(KeyManager.Crouch)) {
            inputForce = (transform.forward * vInput + transform.right * hInput).normalized * crouchSpeed;
            if (carriedObject != null)
            carriedObject.transform.localPosition = new Vector3(0f, 0f, 1f);
        } else if (Input.GetKey(KeyManager.Run)) {
            inputForce = (transform.forward * vInput + transform.right * hInput).normalized * runSpeed;
            if (carriedObject != null)
                carriedObject.transform.localPosition = new Vector3(0f, 0.5f, 1f);
        } else {
            inputForce = (transform.forward * vInput + transform.right * hInput).normalized * walkSpeed;
            if (carriedObject != null)
                carriedObject.transform.localPosition = new Vector3(0f, 0.5f, 1f);
        }
            

        if (isGrounded)
        {
            // Jump
            if (Input.GetButton("Jump") && !jumpBlocked)
            {
                rb.AddForce(-jumpForce * rb.mass * Vector3.down);
                jumpBlocked = true;
                Invoke("UnblockJump", jumpCooldown);
            }
            // Ground controller
            rb.velocity = Vector3.Lerp(rb.velocity, inputForce, changeInStageSpeed * Time.fixedDeltaTime);
        }
        else
            // Air control
            rb.velocity = ClampSqrMag(rb.velocity + inputForce * Time.fixedDeltaTime, rb.velocity.sqrMagnitude);
    }

    private static Vector3 ClampSqrMag(Vector3 vec, float sqrMag)
    {
        if (vec.sqrMagnitude > sqrMag)
            vec = vec.normalized * Mathf.Sqrt(sqrMag);
        return vec;
    }

    private static Vector3 ClampMag(Vector3 vec, float maxMag)
    {
        if (vec.sqrMagnitude > maxMag * maxMag)
            vec = vec.normalized * maxMag;
        return vec;
    }

    #region Previous Ground Check
    /*private void OnCollisionStay(Collision collision)
    {
        isGrounded = false;
        Debug.Log(collision.contactCount);
        for(int i = 0; i < collision.contactCount; ++i)
        {
            if (Vector3.Dot(Vector3.up, collision.contacts[i].normal) > .2f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }*/
    #endregion

    private void UnblockJump()
    {
        jumpBlocked = false;
    }
    
    
    // Enables jumping and player movement
    public void EnableMovement()
    {
        enableMovement = true;
    }

    // Disables jumping and player movement
    public void DisableMovement()
    {
        enableMovement = false;
    }
}
