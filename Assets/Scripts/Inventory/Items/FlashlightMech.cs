using UnityEngine;

public class FlashlightMech : MonoBehaviour{
    public PlayerManager playerManager;
    public bool isOn = false;
    public GameObject lightSource;

    public AudioSource audioSource;
    public AudioClip FlashLightSoundClip;
    
    void Update(){
        if(playerManager.inventory.IsHave(Item.ItemType.Flashlight)){
            if(Input.GetKeyDown(KeyCode.F)){
                if(isOn){
                    lightSource.SetActive(false);
                    isOn = false;
                }else{
                    lightSource.SetActive(true);
                    isOn = true;
                }
                audioSource.clip = FlashLightSoundClip;
                audioSource.Play();
            }
        }
    }
}