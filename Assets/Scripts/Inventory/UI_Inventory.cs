
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory: MonoBehaviour{
    private Inventory inventory;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;

    public void SetInventory(Inventory inventory){
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanger;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanger(object sender, System.EventArgs e){
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems(){
        foreach (RectTransform item in itemSlotContainer.GetComponentsInChildren<RectTransform>())
        {   
            if(item.name == "ItemSlotTemplate(Clone)"){
                Destroy(item.gameObject);
            }
        }

        foreach(Item item in inventory.GetItemList()){
            Transform itemSlotTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<Transform>();
            itemSlotTransform.gameObject.SetActive(true);
            Image image = itemSlotTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            Text name = itemSlotTransform.Find("name").GetComponent<Text>();
            name.text = item.GetName();

        }
    }
}