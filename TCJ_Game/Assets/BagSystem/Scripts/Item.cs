using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private new Collider collider;
    public BagManager Bag;
    public ItemData itemInfo;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Bag = GameObject.Find("BagManager").GetComponent<BagManager>();
        collider = gameObject.GetComponent<Collider>();
        if(collider == null)
        {
            //
            collider = gameObject.AddComponent<BoxCollider>();
        }
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeItem()
    {
        print("Take "+ gameObject.name);
        Bag.ItemIntoBag(itemInfo);
        Destroy(this.gameObject);
    } 
}
