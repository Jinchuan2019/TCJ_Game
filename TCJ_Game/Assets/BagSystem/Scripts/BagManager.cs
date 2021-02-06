using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public GameObject Bag;
    private bool _IsOpen;
    public BagUnit cloneUnit;
    public Transform bagNode;
    public Sprite[] totalItemSprites;
    private List<ItemData> itemDataList = new List<ItemData>();

    private void Awake() 
    {
        
    }

    private void Start() 
    {
        Bag.SetActive(_IsOpen);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            _IsOpen = !_IsOpen;
            Bag.SetActive(_IsOpen);
        }
    }

    public void ItemIntoBag(ItemData itemInfo)
    {
        foreach (var item in itemDataList)
        {
            if(itemInfo.ItemID == item.ItemID)
            {
                item.Count++;
                itemInfo.Count = item.Count;
                item.GetBagUnit().Refresh(itemInfo);
                return;
            }
        }
        
        BagUnit unit = Instantiate<BagUnit>(cloneUnit,bagNode);
        itemInfo.SetBagUnit(unit);
        itemDataList.Add(itemInfo);
        unit.Refresh(itemInfo);
        
    }
}
