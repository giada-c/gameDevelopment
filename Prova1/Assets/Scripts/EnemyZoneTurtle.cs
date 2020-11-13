using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

// Duplicate code from Laucher present, a super class can be introduced

public class EnemyZoneTurtle : MonoBehaviour
{

    
    public float rotationSpeedIntruder = 3f;

    private Transform _intruder;
    private SimplePhysicsRotation _idleRotationBehaviour;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _idleRotationBehaviour = GetComponentInParent<SimplePhysicsRotation>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        if (_intruder)
        {
            Vector3 intruderDirection = (_intruder.transform.position - transform.position);
            intruderDirection.y = 0;
            intruderDirection.Normalize();
            Quaternion lerpToIntruder = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(intruderDirection, Vector3.up), rotationSpeedIntruder * Time.fixedDeltaTime);
            _rigidbody.rotation = lerpToIntruder;
        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnTriggerEnter(Collider other)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.gameObject.tag == "Player")
        {
            _intruder = other.transform;
            if (_idleRotationBehaviour)
            {
                _idleRotationBehaviour.automatic = false;
            }
        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnTriggerExit(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.gameObject.tag == "Player")
        {
            _intruder = null;
            if (_idleRotationBehaviour)
            {
                _idleRotationBehaviour.SyncRotation();
                _idleRotationBehaviour.automatic = true;
            }
        }
    }
}