using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShellAttack : MonoBehaviour
{
 
    private Animator animator;
        
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsNearPlayer())
        {
            animator.SetBool("isAttacking", true);
        } 
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }


    bool IsNearPlayer()
    {
        int layerMask = 1 << 8; // Player MUST stay in layer 8
        return Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward),
            3.5f, layerMask);
    }
}
