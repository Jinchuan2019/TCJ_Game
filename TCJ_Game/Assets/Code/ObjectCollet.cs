using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCollet : MonoBehaviour
{  
    public static int objects=1;
    public static int Colleted = 0;
    GameObject obJui;
    GameObject CoJui;
    public GameObject objToDestroy;
   

    void Start()
    {
        obJui = GameObject.Find("ObjectNum");       
        CoJui= GameObject.Find("ColletNum");
        if (Colleted==1)
        {
            Destroy(objToDestroy);
            return;
        }
     
    }
    void OnTriggerEnter(Collider Plyr)
    {
        if (Plyr.gameObject.tag == "Box")
        {
            Destroy(objToDestroy);
            objects--;
            Colleted++;
        }
    }

    void Update()
    {
        obJui.GetComponent<Text>().text = ObjectCollet.objects.ToString();
        CoJui.GetComponent<Text>().text = ObjectCollet.Colleted.ToString();
        if (ObjectCollet.objects == 0)
        {         
            obJui.GetComponent<Text>().text = "Done";             
        }
    }
}
