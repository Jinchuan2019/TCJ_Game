using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public GameObject bag;
    private static BagManager _bagManager;
    public static BagManager GetBagManager
    {
        get { return _bagManager; }
    }
    private bool _isOpen;
    public BagUnit cloneUnit;
    public Transform bagNode;
    public Sprite[] totalItemSprites;
    private List<ItemData> itemDataList = new List<ItemData>();

    private void Awake()
    {
        bag.SetActive(_isOpen);
        if (_bagManager == null)
        {
            _bagManager = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isOpen = !_isOpen;
            bag.SetActive(_isOpen);
        }
    }

    public void ItemIntoBag(ItemData itemInfo)
    {
        foreach (var item in itemDataList)
        {
            if (itemInfo.ItemID == item.ItemID)
            {
                item.Count++;
                itemInfo.Count = item.Count;
                item.GetBagUnit().Refresh(itemInfo);
                return;
            }
        }

        BagUnit unit = Instantiate<BagUnit>(cloneUnit, bagNode);
        itemInfo.SetBagUnit(unit);
        unit.gameObject.SetActive(true);
        itemDataList.Add(itemInfo);
        unit.Refresh(itemInfo);
    }

    public void OnClickUnit(BagUnit unit)
    {
        unit.OnUse();

        if (unit.GetItemData().Count == 0)
        {
            itemDataList.Remove(unit.GetItemData());
        }
    }

    public bool OnCheckBag()
    {
        bool isCoat = false;
        bool isWatch = false;
        foreach (var item in itemDataList)
        {
            if (item.ItemID == "Coat")
            {
                isCoat = true;
            }
            else if(item.ItemID == "Watch")
            {
                isWatch = true;
            }
        }

        if(isCoat && isWatch)
        {
            print("can go to party");
            return true;
        }

        return false;
    }
}
