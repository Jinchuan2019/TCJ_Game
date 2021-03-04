using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransion : MonoBehaviour
{
    public string sceneToLoad;
    private SceneObjectLoader sceneObjectLoader;
    private void Start()
    {
        if(sceneObjectLoader == null)
        {
            sceneObjectLoader = GameObject.Find("objectSaver").GetComponent<SceneObjectLoader>();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            var character = other.GetComponent<CharacterController>();

            if (!character.GetOpenDoor())
            {
                character.SetOpenDoor(true);
                StartCoroutine(sceneObjectLoader.LoadScene(sceneToLoad));
            }
        }
    }

}
