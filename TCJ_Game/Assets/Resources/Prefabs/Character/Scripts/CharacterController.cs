using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected new Rigidbody2D rigidbody;
    protected Animator animator;
    protected bool key;
    public void SetKey(bool iskey) { key = iskey; }

    protected bool isOpen;

    public bool GetOpenDoor() { return isOpen; }
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
        rigidbody = GetComponent<Rigidbody2D>();
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

        if( vertical != 0 || horizontal != 0)
        {
            Vector2 direction = new Vector2( horizontal, vertical).normalized;
            float y = Camera.main.transform.rotation.eulerAngles.y;
            Vector2 target = Vector2.Lerp(transform.forward, direction, 0.5f);
            transform.Translate(target * Time.deltaTime * speed, Space.World);
            
            animator.SetFloat("XSpeed", horizontal);
            animator.SetFloat("YSpeed", vertical);
        }
        
    }

    protected virtual void OnTriggerStay2D(Collider2D other) 
    {
        var item = other.gameObject.GetComponent<Item>();
        if(item != null) {
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
                door.Interact(this,EnumClass.Event.OpenTheDoor,key);
            }
        }
        
    }

    public void SetOpenDoor(bool inOpen)
    {
        isOpen = inOpen;
        StartCoroutine(OpenTheDoor());
    }
    private IEnumerator OpenTheDoor()
    {
        yield return new WaitForSeconds(1.0f);
        SetOpenDoor(false);
    }
}
