using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGame : SceneLoad
{
    private Item item;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        item = GetComponent<Item>();
        item.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(CharacterController sender, EnumClass.Event eventName, bool key)
    {
        if (key) return;

        if(eventName == EnumClass.Event.OpenSecurity)
        {
            if (sender.gameObject.GetComponent<CharacterController>())
            {
                sceneObjectLoader.NextScene(sceneToLoad);
            }
        }
        print(sender.gameObject);
    }

}
