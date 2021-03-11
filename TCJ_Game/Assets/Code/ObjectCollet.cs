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
    public float moveSpeed;
    private bool key;
    public bool Key
    {
        get { return key; }
    }

    void Start()
    {
        moveSpeed = 5f;
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
        if (Plyr.gameObject.tag == "Key")
        {
            key = true;
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
        transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, -moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
    }
}
