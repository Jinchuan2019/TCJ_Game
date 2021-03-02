using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ISaveable
{
    private new Collider2D collider;
    private BagManager bag;
    public ItemData itemInfo;
    public string prefabName;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        bag = BagManager.GetBagManager;
        collider = gameObject.GetComponent<Collider2D>();

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

    public SaveData Save()
    {
        SaveData saveData = new SaveData();
        saveData.prefabName = prefabName;
        saveData.position = transform.position;
        return saveData;
    }
}
