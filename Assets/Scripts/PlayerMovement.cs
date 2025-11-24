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
    private Vector3 movementDir;
    private Vector3 scaledMovement;
    private Rigidbody playerRb;
    private bool isGrounded = true;
    private bool rotationFrozen = false;
    

    
    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        movementDir = new Vector3(input.x, 0, input.y);
        movementDir = Vector3.ClampMagnitude(movementDir, 1f);
        // Debug.Log(movementDir);
    }

    void OnFreezeRotation()
    {
        rotationFrozen = !rotationFrozen;
    }

    
    
    public void OnLook(InputValue value)
    {
        if (rotationFrozen) return;
        
        rotationX = transform.localEulerAngles.y + value.Get<Vector2>().x * sensitivityX;

        rotationY += value.Get<Vector2>().y * sensitivityY;
        rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
        
        //side-to-side rotations moves entire body
        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        //up and down rotation just moves "eyes" -> camera
        playerEye.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
       

    }

    void OnJump(InputValue value)
    {
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

        if(GameObject.Find("LoseScreen") != null || GameObject.Find("WinScreen") != null)
        {
            rotationFrozen = true;
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
