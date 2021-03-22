using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ISaveable
{
    private new Collider2D collider;
    private BagManager bag;
    public ItemData itemInfo;
    public string prefabName;
    private AudioSource SE;
    
    
    protected void Awake()
    {
        bag = BagManager.GetBagManager;
        collider = gameObject.GetComponent<Collider2D>();
        SE = GetComponent<AudioSource>();
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeItem()
    {
        SE.Play();
        bag.ItemIntoBag(itemInfo);
        print("Take "+ gameObject.name);
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
