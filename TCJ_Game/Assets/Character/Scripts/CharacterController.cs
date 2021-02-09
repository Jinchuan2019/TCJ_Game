using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected new Rigidbody rigidbody;
    protected Animator animator;
    private enum State
    {
        GetItem,
        Talk,
    }

    private State _state;

    private bool _run;
    public float speed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if( vertical != 0 || horizontal != 0){
            Vector3 direction = new Vector3( horizontal, 0, vertical).normalized;
            float y = Camera.main.transform.rotation.eulerAngles.y;
            direction = Quaternion.Euler(0, y, 0) * direction;
            Vector3 target = Vector3.Lerp(transform.forward, direction, 0.2f);
            transform.LookAt(transform.position + target);
            transform.Translate(target * Time.deltaTime * speed, Space.World);
        }
    }

    protected virtual void OnTriggerStay(Collider other) 
    {
        var item = other.gameObject.GetComponent<Item>();
        if(other == null) {
            //TakeItem
            if(Input.GetKeyDown(KeyCode.F))
            {
                item.TakeItem();
            }
        }

        var door = other.gameObject.GetComponent<Door>();
        if(door != null) {
            //OpenTheDoor
            if(Input.GetKeyDown(KeyCode.F))
            {
                door.Interact(this,EnumClass.Event.OpenTheDoor);
            }
        }
        
    }

}
