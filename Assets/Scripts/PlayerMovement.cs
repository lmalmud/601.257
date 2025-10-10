using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5;

    public GameObject playerEye;
    
    private Vector2 movementDir;
    private Vector3 scaledMovement;
    
    private Rigidbody playerRb;
    
    private bool isGrounded = true;
    public float jumpForce = 1000;
    public float groundedDistance = 1.1f;

    private Camera mainCam;

    public GameObject bottom;
    
    
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;
    private Vector2 rotateDir;

    float rotationY = 0F;
    
    
    void OnMove(InputValue value)
    {
        movementDir = value.Get<Vector2>().normalized;
        scaledMovement = movementDir * (moveSpeed);
    }

    
    
    public void OnLook(InputValue value){
        // lookDir = value.Get<Vector2>();
        // scaledLookDir = lookDir * lookSpeed;
        
        Vector2 mouseScreenPos2D = Mouse.current.position.ReadValue();
        Vector3 mouseScreenPos3D = new Vector3(mouseScreenPos2D.x, mouseScreenPos2D.y, 4f); 
        Vector3 worldPosition = mainCam.ScreenToWorldPoint(mouseScreenPos3D);
        Quaternion rotation = Quaternion.FromToRotation(gameObject.transform.forward, worldPosition);

        // Debug.Log("mouse: " + mouseScreenPos3D);
        // Debug.Log("look: " + lookDir);
        
        
        
        if (axes == RotationAxes.MouseXAndY)
        {
            
            float rotationX = transform.localEulerAngles.y + value.Get<Vector2>().x * sensitivityX;

            rotationY += value.Get<Vector2>().y * sensitivityY;
            rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
            // rotateDir = new Vector2(-rotationY, rotationX);
            // Debug.Log(rotateDir);
            
            //side-to-side rotations move entire body
            transform.localEulerAngles = new Vector3(0, rotationX, 0);
            //up and down rotation just moves "eyes" -> camera
            playerEye.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            //Debug.Log("2");
            // transform.Rotate(0, value.Get<Vector2>().x * sensitivityX, 0);
            rotateDir = new Vector2(0, value.Get<Vector2>().x * sensitivityX);
        }
        else
        {
            //Debug.Log("3");
            rotationY += value.Get<Vector2>().y * sensitivityY;
            rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
            rotateDir = new Vector2(-rotationY, playerEye.transform.localEulerAngles.y);
            
            // transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }

    }

    void OnJump(InputValue value)
    {
        //Debug.Log("jumped");
        if (isGrounded)
        {
            //Debug.Log("impulse applied");
            playerRb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckIsGrounded();
        
        
        //Physics.Raycast(wandEndPoint.transform.position, gaze,  out RaycastHit hit, 10);
        
        //Debug.Log(CheckIsGrounded());
        // rb.transform.Translate(movementDir * (moveSpeed * Time.deltaTime), Space.World);
    }

    private void FixedUpdate()
    {
        playerRb.AddRelativeForce(scaledMovement.x, 0, scaledMovement.y);
    }

    bool CheckIsGrounded()
    {
        
        // if (playerRb.velocity.y == 0)
        // {
        
        return Physics.Raycast(bottom.transform.position, Vector3.down, out RaycastHit hit, groundedDistance);
        // }

        // return true;
    }

    void OnDrawGizmos()
    {
        
    }
}
