using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransion : SceneLoad
{
    private BagManager bag;
    private AudioSource SE;

    public Vector3 exitPosition;
    protected override void Start()
    {
        base.Start();
        bag = BagManager.GetBagManager;
        SE = GetComponent<AudioSource>();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger) return;

        switch (sceneToLoad)
        {
            case "Level1":
                OnPlayerLoadScene(other);
                break;
            case "Level2":
                if (bag.OnCheckBag()) return;
                OnPlayerLoadScene(other);
                break;
            case "Level3":
                if (!bag.OnCheckBag()) return;
                OnPlayerLoadScene(other);

                break;
            case "Level5":
                if (other.CompareTag("Car"))
                {
                    sceneObjectLoader.NextScene(sceneToLoad);
                }
                break;
        }
        
    }

    private void OnPlayerLoadScene(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var character = other.GetComponent<CharacterController>();

            if (!character.GetOpenDoor())
            {
                SE.Play();
                character.SetOpenDoor(true);
                character.NextPos = exitPosition;
                sceneObjectLoader.NextScene(sceneToLoad);
            }
        }
    }

}
