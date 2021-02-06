using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public Sprite Icon;
    public int Count;
    public string ItemID;
    public bool Equip;
    private BagUnit bagUnit;

    public void SetBagUnit(BagUnit unit){ bagUnit = unit;}
    public BagUnit GetBagUnit(){ return bagUnit;}

}
