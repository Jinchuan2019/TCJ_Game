using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    public string sceneToLoad;
    protected SceneObjectLoader sceneObjectLoader;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (sceneObjectLoader == null)
        {
            sceneObjectLoader = GameObject.Find("objectSaver").GetComponent<SceneObjectLoader>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
