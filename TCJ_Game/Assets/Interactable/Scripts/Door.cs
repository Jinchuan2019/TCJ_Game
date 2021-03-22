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
            var targetDoor = new Vector2(2.5f,0);

            moveDoorUp.transform.position = Vector2.Lerp(moveDoorUp.transform.position, doorUp - targetDoor , 0.2f);
            moveDoorDown.transform.position = Vector2.Lerp(moveDoorDown.transform.position, doorDown - targetDoor , 0.2f);
        }
        else
        {
            moveDoorUp.transform.position = Vector2.Lerp(moveDoorUp.transform.position, doorUp , 0.2f);
            moveDoorDown.transform.position = Vector2.Lerp(moveDoorDown.transform.position, doorDown , 0.2f);
        }
    }
    public void Interact(CharacterController sender, EnumClass.Event eventName, bool key)
    {
        if (eventName == EnumClass.Event.OpenDoor)
        {
            if (sender.gameObject.GetComponent<PlayerController>())
            {
                OpenDoor(key);
            }
            else if (sender.gameObject.GetComponent<ThiefController>())
            {
                UseBomb();
            }
        }
        print(sender.gameObject);
    }

    private void OpenDoor(bool key)
    {
        if(!_openDoor && key)
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
