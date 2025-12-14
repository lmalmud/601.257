using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*
    Author: Brady Bock
    Date Created: 10/1/25
    Date Last Updated: 10/14/25
    Summary: This script is responsible for handling player x-z translation, rotation, and jumping
*/
public class PlayerMovement : MonoBehaviour
{
    
    

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 100;
    [SerializeField] private float groundedDistance = 0.2f;
    [SerializeField] private GameObject bottom;
    
    [Header("Move Parameters")]
    [SerializeField] private float maxAcceleration = 10f;
    [SerializeField] private float maxDeceleration = 20f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Rotate Parameters")]
    [SerializeField] private float sensitivityX = 1F;
    [SerializeField] private float sensitivityY = 1F;
    [SerializeField] private float minimumY = -60F;
    [SerializeField] private float maximumY = 60F;
    [SerializeField] private GameObject playerEye;
    
    
    //variables for keeping stack of the player's current state
    private float rotationY = 0F;
    private float rotationX = 0F;
    // store raw look input from the input system and apply per-frame
    private Vector2 lookInput = Vector2.zero;
    private Vector3 movementDir;
    private Vector3 scaledMovement;
    private Rigidbody playerRb;
    private bool isGrounded = true;
    private bool rotationFrozen = false;
    

    // public void OnMove(InputAction.CallbackContext context)
    // {
    //     Debug.Log(context.ReadValue<Vector2>());
    // }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            movementDir = Vector3.zero;
            return;
        }
        Vector2 input = context.ReadValue<Vector2>();
        movementDir = new Vector3(input.x, 0, input.y);
        movementDir = Vector3.ClampMagnitude(movementDir, 1f);
        // Debug.Log(movementDir);
    }

    public void OnFreezeRotation(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        rotationFrozen = !rotationFrozen;
    }

    
    
    public void OnLook(InputAction.CallbackContext context)
    {
        // Store the latest look delta. We'll apply smoothing and time-scaling in Update()
        // Using context.ReadValue<Vector2>() here avoids depending on context.performed
        // which can be called irregularly on some platforms (WebGL).
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }

        if (isGrounded)
        {
            playerRb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        rotationFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckIsGrounded();

        if(GameObject.Find("LoseScreen") != null || GameObject.Find("WinScreen") != null || GameObject.Find("PauseMenu") != null)
        {
            rotationFrozen = true;
        }
        if(GameObject.Find("PauseMenu") == null)
        {
            rotationFrozen = false; //there is definately a more optimal way to do this with events
        }

        // Apply look input every frame with time-scaling to avoid jitter on variable frame rates (helps WebGL)
        if (!rotationFrozen && playerEye != null)
        {
            // scale converts raw delta into a usable degrees/sec; tweak as needed
            const float scale = 100f;
            rotationX += lookInput.x * sensitivityX * Time.deltaTime * scale;
            rotationY += lookInput.y * sensitivityY * Time.deltaTime * scale;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localRotation = Quaternion.Euler(0f, rotationX, 0f);
            playerEye.transform.localRotation = Quaternion.Euler(-rotationY, 0f, 0f);
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = movementDir * maxSpeed;
        AccelerateToward(targetVelocity);
    }
    
    void AccelerateToward(Vector3 targetVelocity) {
        float limit = maxAcceleration;
        Vector3 relativeVelocity = Quaternion.Inverse(transform.rotation) * playerRb.velocity;

        Vector3 deltaV = targetVelocity - relativeVelocity;

        // If we're stopping or reversing direction, use deceleration limit instead.
        if (Vector3.Dot(relativeVelocity, deltaV) < 0)
        {
            limit = maxDeceleration;
        }
        // Since we're calling this in FixedUpdate, deltaTime gives our fixed timestep
        Vector3 accel = deltaV / Time.deltaTime;
        accel = Vector3.ClampMagnitude(accel, limit);
        accel = Quaternion.Euler(0, transform.rotation.y, 0) * accel;
        accel.y = 0;
        playerRb.AddRelativeForce(accel, ForceMode.Acceleration);
    }

    bool CheckIsGrounded()
    {
        //TODO make this a more sophisticated check. numerous cases where it fails (ex. when standing on the edge of something)
        return Physics.Raycast(bottom.transform.position, Vector3.down, out RaycastHit hit, groundedDistance);

    }

}
