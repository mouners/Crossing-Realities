using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;
    public Inventory()
    {
        itemList = new List<Item> ();
    }

    public void AddItem(Item item){
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList(){
        return itemList;
    }

    public void RemoveItem(Item.ItemType type){
        if(itemList == null){
            return;
        }

        foreach(Item item in itemList){
            if(item.type == type){
                itemList.Remove(item);
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void RemoveAll(){
        if(itemList == null){
            return;
        }

        foreach(Item item in itemList){
            itemList.Remove(item);
        }
    }

    public bool IsHave(Item item){
        if(itemList == null){
            return false;
        }

        bool isHave = false;
        foreach(Item i in itemList){
            if(i == item){
                isHave = true;
            }
        }
        return isHave;
    }

    public bool IsHave(Item.ItemType type){
        if(itemList == null){
            return false;
        }

        bool isHave = false;
        foreach(Item i in itemList){
            if(i.type == type){
                isHave = true;
            }
        }
        return isHave;
    }
    
}
