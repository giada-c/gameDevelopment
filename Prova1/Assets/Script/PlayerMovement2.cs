using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{

    public float speedMovement = 20f;
    public float speedJump = 10f;
    public float SpeedRotation = 20f;

    public float SpeedUpMultiplier = 5f;
    public float MouseSensitivity = 5f;

    public float distToGround;
    private Rigidbody rigidbody;

    private bool run=false;
    private bool mouseMode =false;

    private Vector3 translation = Vector3.zero;
    private Vector3 translationJump = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX=0;
        float moveY=0;
        float moveZ=0;

        float roteY=0;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            mouseMode = !mouseMode;
        }
       


        if (IsGrounded())
        {
            Debug.Log("TERRA");

            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");

            moveY = Input.GetAxis("Jump");

        

            if(mouseMode)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                roteY += Input.GetAxis("Mouse X") * MouseSensitivity;

            }
            else {

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                roteY = Input.GetAxis("RotateView");
            }
           

            translation = Vector3.zero;
            translation += transform.right * moveX;
            translation += transform.forward * moveZ;

            translationJump = Vector3.zero;
            translationJump += transform.up * moveY;

            rotation = Vector3.zero;
            rotation += Vector3.up * roteY;

           
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("RUN");
                translation *= SpeedUpMultiplier;
                rotation *= SpeedUpMultiplier;
            }
            
        }
        else
        {
            Debug.Log("NON TERRA");
        }
        
    }
    

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }


    void FixedUpdate()
    {

        rigidbody.MovePosition(rigidbody.position + translation * speedMovement * Time.fixedDeltaTime);
        rigidbody.MovePosition(rigidbody.position + translationJump * speedJump * Time.fixedDeltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(rotation * SpeedRotation * Time.fixedDeltaTime));

    }
}
/*
 

using System;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpHeight = 2f;
    public float SpeedUpMultiplier = 5f;
    public float SpeedRotate = 20f;

    private Rigidbody _body;

    public float GroundDistance = 0.2f;
    public LayerMask Ground;
    public Transform _groundChecker;
    private bool _isGrounded = true;

    private Vector3 _translation = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;

    public float MouseSensitivity = 5f;
    private bool _mouseMode = false;

    public Animator animator;

    private bool canMove = true;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // check if ground below character is present

        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (moveX == 0 && moveZ == 0 && _isGrounded)
        {
            //animator.Play("BasicMotions@Idle01");
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {

            //animator.Play("Jump");
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        else if (_isGrounded && (moveX != 0 || moveZ != 0))
        {
            // animator.Play("BasicMotions@Run01");
        }



        bool speedUp = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _mouseMode = !_mouseMode;
        }

        float rotationY = 0;

        if (_mouseMode)
        {
            // Let Unity take control of the cursor, so it will not click outside game window
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            rotationY += Input.GetAxis("Mouse X") * MouseSensitivity;
        }
        else
        {
            // Release cursor control
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (Input.GetKey(KeyCode.Q))
            {
                rotationY -= 1;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                rotationY += 1;
            }
        }

        // adapt input to where the character local rotation, otherwise wasd will move in world space
        _translation = Vector3.zero;
        _translation += transform.forward * moveZ;
        _translation += transform.right * moveX;

        _rotation = Vector3.up * rotationY;

        if (speedUp)
        {
            _translation *= SpeedUpMultiplier;
            _rotation *= SpeedUpMultiplier;
        }


        // Uncomment below to change player direction based on where it is going
        // if (_inputs != Vector3.zero)
        //     transform.forward = _inputs;

        /*if (Input.GetButtonDown("Jump") && _isGrounded)
        {

            animator.Play("BasicMotions@Jump01");
            
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }*//*
    }

    void FixedUpdate()
    {


        _body.MovePosition(_body.position + _translation * Speed * Time.fixedDeltaTime);
        _body.MoveRotation(_body.rotation * Quaternion.Euler(_rotation * SpeedRotate * Time.fixedDeltaTime));


    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Barrier")
        {
            Debug.Log("ROCCIA");
        }


    }

}


*/