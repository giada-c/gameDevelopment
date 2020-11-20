using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{

    public float speedMovement = 15f;
    public float speedJump = 10f;
    public float SpeedRotation = 20f;

    public float SpeedUpMultiplier = 5f;
    public float MouseSensitivity = 5f;

    public float distToGround;
    private Rigidbody rigidbody;

   
    private bool mouseMode = false;

    
    private Vector3 movement = Vector3.zero;
    private Vector3 jump = Vector3.zero;


    public Animator animator;
    Transform prima;
    Transform dopo;


    // Start is called before the first frame update
    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        rigidbody = GetComponent<Rigidbody>();
        prima = rigidbody.transform;
        dopo = rigidbody.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            mouseMode = !mouseMode;
        }

        if (IsGrounded())
        { 

            // Debug.Log(" TERRA");

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Jump");
            float z = Input.GetAxis("Vertical");

            

            movement = new Vector3(x, 0, z);
            movement *= speedMovement;

            jump = new Vector3(0, y, 0);
            jump *= speedJump;
        }
        else
        {
            //Debug.Log("NON TERRA");

            movement = Vector3.zero;
            jump = Vector3.zero;



        }




        


        prima = dopo;
        dopo = rigidbody.transform;

        Debug.Log("\tprima-->" + prima.position + "\n\t\tdopo-->" + dopo.position);
        if (prima.position.x == dopo.position.x && prima.position.z == dopo.position.z)
        {

            animator.SetFloat("move", 5);
        }
        else
        {

            animator.SetFloat("move", 10);
        }
    }

    void FixedUpdate()
    {
        chMove();
        chJump();
    }

    void chMove()
    {
        //Debug.Log("MOVE-->" + movement);
        rigidbody.AddForce(movement, ForceMode.Acceleration);
        

    }

    void chJump()
    {
        //Debug.Log("JUMP-->" + jump);
        rigidbody.AddForce(jump, ForceMode.Impulse);
    }


    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

}




/*
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
            //Debug.Log("TERRA");

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
                //Debug.Log("RUN");
                translation *= SpeedUpMultiplier;
                rotation *= SpeedUpMultiplier;
            }
            
        }
        else
        {
            //Debug.Log("NON TERRA");
           
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
}*/
