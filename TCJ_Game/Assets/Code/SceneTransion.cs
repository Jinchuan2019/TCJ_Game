using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransion : MonoBehaviour
{
    public string sceneToLoad;
    protected SceneObjectLoader sceneObjectLoader;
    private BagManager bag;
    private AudioSource SE;
    protected virtual void Start()
    {
        bag = BagManager.GetBagManager;
        SE = GetComponent<AudioSource>();
        if (sceneObjectLoader == null)
        {
            sceneObjectLoader = GameObject.Find("objectSaver").GetComponent<SceneObjectLoader>();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger) return;

        switch (sceneToLoad)
        {
            case "Level1":
            case "Level2":
                OnPlayerLoadScene(other);
                break;
            case "Level3":
                if (!bag.OnCheckBag()) return;
                OnPlayerLoadScene(other);

                break;
            case "Level4":
                if (other.CompareTag("Car"))
                {
                    StartCoroutine(sceneObjectLoader.LoadScene(sceneToLoad));
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
                StartCoroutine(sceneObjectLoader.LoadScene(sceneToLoad));
            }
        }
    }

}
