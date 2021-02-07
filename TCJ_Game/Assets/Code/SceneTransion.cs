using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransion : MonoBehaviour
{
    public string sceneToLoad;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!other.isTrigger )
        {

            SceneManager.LoadScene(sceneToLoad);

        }
    }

}
