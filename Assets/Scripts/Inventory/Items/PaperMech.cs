using Unity.VisualScripting;
using UnityEngine;

public class PaperMech : MonoBehaviour{

    public PlayerManager playerManager;
    public bool IsOpened = false;
    public GameObject paper;

    void Update(){
        if(playerManager.inventory.IsHave(Item.ItemType.Paper)){
            if(Input.GetKeyDown(KeyCode.H)){
                if(IsOpened){
                    paper.SetActive(false);
                    IsOpened = false;
                }else{
                    paper.SetActive(true);
                    IsOpened = true;
                }
            }
        }
    }
}