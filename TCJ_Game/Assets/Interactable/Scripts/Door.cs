using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour,Interactable
{
    
    private bool _openDoor;
    public GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        bomb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_openDoor)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,90,0),0.05f);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,0),0.05f);
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
