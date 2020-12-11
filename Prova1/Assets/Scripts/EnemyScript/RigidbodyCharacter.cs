/*** SOURCE EXAMPLE
 * https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483
 * Adapted to have fixed rotation while moving
 */

using System;
using UnityEditor.Animations;
using UnityEngine;

public class RigidbodyCharacter : MonoBehaviour
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
        
        bool speedUp = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _mouseMode = !_mouseMode;
        }

        float rotationY = 0;

        if (_mouseMode)
        {
            // TODO: add code here (1)
            // Let Unity take control of the cursor, so it will not click outside the game window
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            rotationY += Input.GetAxis("Mouse x") * MouseSensitivity;
        }
        else
        {
            // TODO: add code here (2)
            // Relase the cursor (mouse mode off)
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (Input.GetKey(KeyCode.Q))
                rotationY -= 1;
            else if (Input.GetKey(KeyCode.E))
                rotationY += 1;

        }

        // TODO: add code here (3)
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

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }

    // Esegue da 0 a piu volte all'update in modo asincrono. Serve per funzioni legate alla fisica
    void FixedUpdate()
    {
        // TODO: add code here (4)
        _body.MovePosition(_body.position + _translation * Speed * Time.fixedDeltaTime);
        _body.MoveRotation(_body.rotation  * Quaternion.Euler(_rotation * SpeedRotate * Time.fixedDeltaTime));

    }
}