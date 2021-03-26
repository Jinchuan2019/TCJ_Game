using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour,Interactable
{
    
    private bool _openCar;
    public float speed;
    private Transform player;
    protected Animator animator;
    protected new Rigidbody2D rigidbody;
    private Transform boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        boxCollider = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(_openCar)
        {
            Move();
        }
        else
        {
            
        }
    }

    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (vertical != 0 || horizontal != 0)
        {
            Vector2 direction = new Vector2(horizontal, vertical).normalized;
            float y = Camera.main.transform.rotation.eulerAngles.y;
            Vector2 target = Vector2.Lerp(transform.forward, direction, 0.5f);
            transform.Translate(target * Time.deltaTime * speed, Space.World);

            animator.SetFloat("XSpeed", horizontal);
            animator.SetFloat("YSpeed", vertical);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            boxCollider.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            boxCollider.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.F))
        {
            StartCoroutine(OpenCar(false));
        }
    }
    public void Interact(CharacterController sender,EnumClass.Event eventName, bool key)
    {
        if(eventName == EnumClass.Event.OpenCar)
        {
            if (sender.gameObject.GetComponent<PlayerController>())
            {
                player = sender.gameObject.GetComponent<Transform>();
                StartCoroutine(OpenCar(key));
            }
            else if (sender.gameObject.GetComponent<ThiefController>())
            {
                player = sender.gameObject.GetComponent<Transform>();
                UseCar();
            }
        }
        print(sender.gameObject);
    }

    private IEnumerator OpenCar(bool key)
    {
        yield return new WaitForSeconds(0.5f);
        if(!_openCar && key)
        {
            _openCar = true;
            player.SetParent(this.transform);
            player.gameObject.SetActive(false);
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            print("OpenTheCar");
            
        }
        else
        {
            _openCar = false;
            player.SetParent(null);
            player.gameObject.SetActive(true);
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            DontDestroyOnLoad(player);
        }
        
    }

    private void UseCar()
    {
        StartCoroutine(OpenCar(true));
        print("UseCar");
    }

    private void DestroyObj()
    {
        Destroy(this.gameObject);
    }

}
