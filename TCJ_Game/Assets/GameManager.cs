using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isInit = true;
    public string[] item;

    // Start is called before the first frame update
    void Start()
    {
        if (isInit)
        {
            foreach (var i in item)
            {
                GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/" + i));
            }
            isInit = false;
        }
    }

}
