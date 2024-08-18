
using UnityEngine;

public class PlayerManager : MonoBehaviour{

    public Inventory inventory;

    [SerializeField] private UI_Inventory uiInventory;

    private void Awake(){
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }
}