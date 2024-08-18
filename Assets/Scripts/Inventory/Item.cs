using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Battery,
        Firstaid,
        Flashlight,
        Hammer,
        Shovel,
        Paper,
        FireEquip
    }

    public ItemType type;

    public Sprite GetSprite(){
        switch(type){
            default:
            case ItemType.Battery:      return ItemAssets.Instance.batterySprite;
            case ItemType.Firstaid:     return ItemAssets.Instance.firstaidSprite;
            case ItemType.Flashlight:   return ItemAssets.Instance.flashLightSprite;
            case ItemType.Hammer:       return ItemAssets.Instance.hammerSprite;
            case ItemType.Shovel:       return ItemAssets.Instance.shovelSprite;
            case ItemType.Paper:        return ItemAssets.Instance.paperSprite;
            case ItemType.FireEquip:    return ItemAssets.Instance.fireEquipSprite;
        }
    }

    public string GetName(){
        switch(type){
            default:
            case ItemType.Battery:      return "Battery";
            case ItemType.Firstaid:     return "Firstaid";
            case ItemType.Flashlight:   return "Flashlight";
            case ItemType.Hammer:       return "Hammer";
            case ItemType.Shovel:       return "Shovel";
            case ItemType.Paper:        return "Paper";
            case ItemType.FireEquip:        return "Fire Equip";
        }
    }
}
