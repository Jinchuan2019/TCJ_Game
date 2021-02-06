using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUnit : MonoBehaviour
{
    public Image Icon;
    public Text CountText;
    public Text ItemText;
    private ItemData tempData;

    public void Refresh(ItemData itemData)
    {
        tempData = itemData;
        Icon.sprite = itemData.Icon;
        ItemText.text = itemData.ItemID;
        CountText.text = itemData.Count.ToString();
    }
}
