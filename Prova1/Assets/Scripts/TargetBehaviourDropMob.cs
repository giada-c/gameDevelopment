using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;
using UnityEngine;


public class TargetBehaviourDropMob : MonoBehaviour
{
    public float hitTime = 2f;
    public int life = 5;
    public GameObject objectToDrop;

    public Renderer[] targetRenderers;
    private bool _isHit = false;
    private bool _isBleeding = false;
    private float _hitTimer = 0f;

    void Awake()
    {
        if (targetRenderers == null || targetRenderers.Length == 0)
        {
            targetRenderers = GetComponentsInChildren<MeshRenderer>();
        }
    }

    void Update()
    {
        //Conrol if the mob still alive
        if (life == 0)
        {
            Instantiate(objectToDrop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }


        if (_isHit && !_isBleeding) // Register hit and start bleeding state
        {
            _isHit = false;
            _isBleeding = true;
            foreach (var r in targetRenderers)
            {
                foreach (var m in r.materials)
                {
                    m.color = Color.red;
                }
            }
            _hitTimer = hitTime;
        }
        else if (_hitTimer > 0f) // While bleeding
        {
            _hitTimer -= Time.deltaTime;
        }
        else if (_isBleeding) // Stop bleeding and return to normal state
        {
            _isBleeding = false;
            foreach (var r in targetRenderers)
            {
                foreach (var m in r.materials)
                {
                    m.color = Color.white;
                }
            }
        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Damage")
        {
            _isHit = true;
            --life;
        }
    }
}