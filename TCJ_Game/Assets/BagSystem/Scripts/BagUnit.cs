using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUnit : MonoBehaviour
{
    public Image Icon;
    public Text CountText;
    public Text ItemText;
    private ItemData itemData;
    public ItemData GetItemData(){ return itemData;}


    public void Refresh(ItemData itemInfo)
    {
        itemData = itemInfo;
        Icon.sprite = itemInfo.Icon;
        ItemText.text = itemInfo.ItemID;
        CountText.text = itemInfo.Count.ToString();
    }

    public void OnUse()
    {
        itemData.Count--;

        if(itemData.Count == 0)
        {
            Destroy(gameObject);
        }

        Refresh(itemData);
        print("Use Item " + itemData.ItemID);
    }
}
