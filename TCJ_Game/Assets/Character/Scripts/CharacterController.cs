using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected new Rigidbody rigidbody;
    protected Animator animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
