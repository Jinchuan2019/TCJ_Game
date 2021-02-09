using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private new Collider collider;
    private BagManager bag;
    public ItemData itemInfo;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        bag = BagManager.GetBagManager;
        collider = gameObject.GetComponent<Collider>();

        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeItem()
    {
        print("Take "+ gameObject.name);
        bag.ItemIntoBag(itemInfo);
        Destroy(this.gameObject);
    } 
}
