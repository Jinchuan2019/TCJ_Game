using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour,Interactable
{
    
    private bool _openDoor;
    public GameObject bomb;
    public float speed;
    public GameObject moveDoorUp;
    public GameObject moveDoorDown;
    public GameObject targetDoorUp;
    public GameObject targetDoorDown;
    private Vector2 doorUp;
    private Vector2 doorDown;
    // Start is called before the first frame update
    void Start()
    {
        bomb.SetActive(false);
        doorUp = moveDoorUp.transform.position;
        doorDown = moveDoorDown.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_openDoor)
        {
            //var direction= new Vector2(-0.5f,0f);
            //Vector2 target = Vector2.Lerp(transform.forward, direction , 0.5f);
            //moveDoorUp.transform.Translate(target * Time.deltaTime * speed, Space.World);
            //moveDoorDown.transform.Translate(target * Time.deltaTime * speed, Space.World);
            
            moveDoorUp.transform.position = Vector2.Lerp(moveDoorUp.transform.position, targetDoorUp.transform.position , 0.2f);
            moveDoorDown.transform.position = Vector2.Lerp(moveDoorDown.transform.position, targetDoorDown.transform.position , 0.2f);
        }
        else
        {
            
            moveDoorUp.transform.position = Vector2.Lerp(moveDoorUp.transform.position, doorUp , 0.2f);
            moveDoorDown.transform.position = Vector2.Lerp(moveDoorDown.transform.position, doorDown , 0.2f);
        }
    }
    public void Interact(CharacterController sender,EnumClass.Event eventName)
    {

        if(sender.gameObject.GetComponent<PlayerController>())
        {
            switch (eventName)
            {
                case EnumClass.Event.OpenTheDoor:
                    OpenTheDoor();
                    break;
                default:
                    break;
            }
        }

        if(sender.gameObject.GetComponent<ThiefController>())
        {
            switch (eventName)
            {
                case EnumClass.Event.OpenTheDoor:
                    UseBomb();
                    break;
                default:
                    break;
            }
        }

        print(sender.gameObject);
    }

    private void OpenTheDoor()
    {
        if(!_openDoor)
        {
            _openDoor = true;
            print("OpenTheDoor");
            Invoke("OpenTheDoor",3f);
        }
        else
        {
            _openDoor = false;
        }
        
    }

    private void UseBomb()
    {
        bomb.SetActive(true);
        print("UseBomb");
        Invoke("DestroyObj",3f);
    }

    private void DestroyObj()
    {
        Destroy(this.gameObject);
    }

}
