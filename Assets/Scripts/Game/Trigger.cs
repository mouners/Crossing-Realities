using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameManager gameManager;
    public PlayerMove playerMove;
    public PlayerCam playerCam;
    public ItemAssets itemAssets;

    public MissionWaypoint missionWaypoint;

    public KeyCode listenToKey = KeyCode.E;    

    public GameObject SpawnPosMap2;

    public Animator anim;
    public Animator endAnim;
    public List<Material> skyMaterial;

    public AudioClip PickUpSoundClip;
    public AudioClip PowerOnSoundClip;
    public AudioClip DigSoundClip;
    public AudioSource audioSource;

    Item battery = new Item {type = Item.ItemType.Battery};
    Item shovel = new Item {type = Item.ItemType.Shovel};
    Item flashlight = new Item {type = Item.ItemType.Flashlight};
    Item firstaid = new Item {type = Item.ItemType.Firstaid};
    Item hammer = new Item {type = Item.ItemType.Hammer};
    Item paper = new Item {type = Item.ItemType.Paper};
    Item fireEquip = new Item {type = Item.ItemType.FireEquip};
    
    int paperRandomIndex;

    public GameObject ComputerScreenPanel;
    bool isComputerUsed = false;

    public GameObject Paper2Panel;

    public GameObject FirstaidPutPos;
    public int PulledOutFireCount = 0;
    public bool IsInMap1 = true;

    public GameObject GetBackPortal;
    public GameObject GetBackTextPanel;
    public GameObject EndScenePanel;

    private void OnTriggerEnter(Collider other){
        switch(other.tag){
            case "teleport":
                if(gameManager.taskDoneCount == 3){
                    if(IsInMap1){
                        if(gameManager.randomPortal == int.Parse(other.name))
                            gameManager.CreateUseText("Press [E] To Teleport.");
                        else
                            StartCoroutine(gameManager.CreateErrorText("Wrong Portal."));
                    }else{
                        if(gameManager.taskDoneCount == 3)
                            gameManager.CreateUseText("Press [E] To Get Back.");
                        else
                            gameManager.CreateErrorText("You Have To Complete All The Tasks.");
                    }
                }else
                    gameManager.CreateErrorText("You Have To Complete All The Tasks.");
                break;
            case "battery":
                gameManager.CreateUseText("Press [E] To Pick Up Battery.");
                break;
            case "shovel":
                gameManager.CreateUseText("Press [E] To Pick Up Shovel.");
                break;
            case "power":
                if(playerManager.inventory.IsHave(battery))
                    gameManager.CreateUseText("Press [E] To Turn On Power.");
                else
                    StartCoroutine(gameManager.CreateErrorText("You Do Not Have A Battery."));
                break;
            case "flashlight":
                gameManager.CreateUseText("Press [E] To Pick Up Flashlight.");
                break;
            case "firstaid":
                gameManager.CreateUseText("Press [E] To Pick Up Firstaid.");
                break;
            case "putFirstaid":
                if(playerManager.inventory.IsHave(Item.ItemType.Firstaid))
                    gameManager.CreateUseText("Press [E] To Put Firstaid.");
                else
                    gameManager.CreateErrorText("You Do Not Have Firstaid");
                break;
            case "hammer":
                gameManager.CreateUseText("Press [E] To Pick Up Hammer.");
                break;
            case "computer":
                if(gameManager.isPowerOn){
                    if(!isComputerUsed)
                        gameManager.CreateUseText("Press [E] To Use Computer.");
                }
                else
                    StartCoroutine(gameManager.CreateErrorText("The Power Is Off."));
                break;
            case "dig":
                if(playerManager.inventory.IsHave(shovel))
                    gameManager.CreateUseText("Press [E] To Dig.");
                else
                    StartCoroutine(gameManager.CreateErrorText("You Do Not Have A Shovel."));
                break;
            case "paper":
                gameManager.CreateUseText("Press [E] To Open The Massege.");
                break;
            case "fireEquip":
                gameManager.CreateUseText("Press [E] To Pick Up Fire Equip.");
                break;
            case "fire":
                if(playerManager.inventory.IsHave(Item.ItemType.FireEquip))
                    gameManager.CreateUseText("Press [E] To put out the fire.");
                else
                    gameManager.CreateErrorText("You Do Not Have A Fire Equip.");
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch(other.tag){
            case "teleport":
                if(gameManager.taskDoneCount == 3){
                    if(IsInMap1){
                        if(Input.GetKey(listenToKey)){
                            if(gameManager.randomPortal == int.Parse(other.name)){
                                StartCoroutine(Teleport(int.Parse(other.name)));
                                IsInMap1 = false;
                            }
                        }
                    }else{
                        if(Input.GetKeyDown(listenToKey)){
                            if(gameManager.taskDoneCount == 3){
                                gameManager.RemoveUseText();
                                playerMove.enabled = false;
                                playerCam.enabled = false;
                                gameManager.tasksPanel.SetActive(false);
                                gameManager.UiInventory.SetActive(false);
                                EndScenePanel.gameObject.SetActive(true);
                                StartCoroutine(PlayAnim());
                            }   
                        }
                    }
                }
                break;
            case "battery":
                if(Input.GetKey(listenToKey)){
                    gameManager.RemoveUseText();
                    GameObject.Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
                    playerManager.inventory.AddItem(battery);

                    audioSource.clip = PickUpSoundClip;
                    audioSource.Play();
                    
                    StartCoroutine(gameManager.CreateUsementText("Battery", "You can use the battery for get back the power.", itemAssets.batterySprite));
                }
                break;
            case "shovel":
                if(Input.GetKey(listenToKey)){
                    gameManager.RemoveUseText();
                    GameObject.Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
                    playerManager.inventory.AddItem(shovel);

                    audioSource.clip = PickUpSoundClip;
                    audioSource.Play();

                    paperRandomIndex = gameManager.SpawnPaper();
                    StartCoroutine(gameManager.CreateUsementText("Shovel", "You can use the shovel for digging the x marks.", itemAssets.shovelSprite));

                    if(playerManager.inventory.IsHave(Item.ItemType.Hammer) && playerManager.inventory.IsHave(Item.ItemType.Firstaid)){
                        gameManager.doneTask(1, 3);
                    }
                }
                break;
            case "power":
                if(playerManager.inventory.IsHave(battery)){
                    if(Input.GetKey(listenToKey)){
                        gameManager.RemoveUseText();
                        gameManager.TurnOnPower();

                        audioSource.clip = PowerOnSoundClip;
                        audioSource.Play();

                        gameManager.doneTask(1, 1);
                        playerManager.inventory.RemoveItem(Item.ItemType.Battery);
                    }
                }
                break;
            case "flashlight":
                if(Input.GetKey(listenToKey)){
                    gameManager.RemoveUseText();
                    GameObject.Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
                    playerManager.inventory.AddItem(flashlight);

                    audioSource.clip = PickUpSoundClip;
                    audioSource.Play();

                    StartCoroutine(gameManager.CreateUsementText("FlashLight", "You can use the FlashLight to illuminate dark areas and reveal hidden paths. [Press F To Activate/Deactivate]", itemAssets.flashLightSprite));
                }
                break;
            case "firstaid":
                if(Input.GetKey(listenToKey)){
                    gameManager.RemoveUseText();
                    GameObject.Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
                    playerManager.inventory.AddItem(firstaid);

                    audioSource.clip = PickUpSoundClip;
                    audioSource.Play();

                    StartCoroutine(gameManager.CreateUsementText("Firstaid", "You can use the Firstaid to treat someone.", itemAssets.firstaidSprite));
                    if(playerManager.inventory.IsHave(Item.ItemType.Hammer) && playerManager.inventory.IsHave(Item.ItemType.Shovel)){
                        gameManager.doneTask(1, 3);
                    }
                }
                break;
            case "putFirstaid":
                if(playerManager.inventory.IsHave(Item.ItemType.Firstaid)){
                    if(Input.GetKey(listenToKey)){
                        gameManager.RemoveUseText();
                        GameObject.Destroy(other);
                        FirstaidPutPos.gameObject.SetActive(true);
                        gameManager.doneTask(2, 3);
                        playerManager.inventory.RemoveItem(Item.ItemType.Firstaid);
                    }
                }
                break;
            case "hammer":
                if(Input.GetKey(listenToKey)){
                    gameManager.RemoveUseText();
                    GameObject.Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
                    playerManager.inventory.AddItem(hammer);

                    audioSource.clip = PickUpSoundClip;
                    audioSource.Play();

                    StartCoroutine(gameManager.CreateUsementText("Hammer", "You can use the Hammer to protect your self and destory objects.", itemAssets.hammerSprite));

                    if(playerManager.inventory.IsHave(Item.ItemType.Shovel) && playerManager.inventory.IsHave(Item.ItemType.Firstaid)){
                        gameManager.doneTask(1, 3);
                    }
                }
                break;
            case "computer":
                if(gameManager.isPowerOn){
                    if(!isComputerUsed){
                        if(Input.GetKey(listenToKey)){
                            gameManager.RemoveUseText();
                            ComputerScreenPanel.gameObject.SetActive(true);
                            playerMove.enabled = false;
                            playerCam.enabled = false;

                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;

                            gameManager.UiInventory.SetActive(false);
                            gameManager.tasksPanel.gameObject.SetActive(false);
                            isComputerUsed = true;
                        }
                    }
                }
                break;
            case "dig":
                if(playerManager.inventory.IsHave(shovel)){
                    if(Input.GetKey(listenToKey)){
                        gameManager.RemoveUseText();
                        if(int.Parse(other.gameObject.name) == paperRandomIndex){
                            playerManager.inventory.AddItem(paper);

                            StartCoroutine(gameManager.CreateUsementText("Paper", "The Paper have a code in it you can use it somewhere. [Press H to open/close it]", itemAssets.paperSprite));

                            missionWaypoint.RemoveWaypoint(1);
                            missionWaypoint.RemoveWaypoint(2);
                            missionWaypoint.RemoveWaypoint(3);
                        }
                        missionWaypoint.RemoveWaypoint(int.Parse(other.gameObject.name));
                    }
                }
                break;
            case "paper":
                if(Input.GetKey(listenToKey)){
                    if(!Paper2Panel.activeSelf){
                        Paper2Panel.SetActive(true);
                        gameManager.RemoveUseText();
                        gameManager.doneTask(2, 1);

                        playerMove.enabled = false;
                        playerCam.enabled = false;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }  
                }
                break;
            case "fireEquip":
                if(Input.GetKey(listenToKey)){
                    gameManager.RemoveUseText();
                    GameObject.Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
                    playerManager.inventory.AddItem(fireEquip);

                    audioSource.clip = PickUpSoundClip;
                    audioSource.Play();

                    StartCoroutine(gameManager.CreateUsementText("Fire Equip", "You can use the Fire Equip to put out the fire.", itemAssets.fireEquipSprite));
                }
                break;
            case "fire":
                if(playerManager.inventory.IsHave(Item.ItemType.FireEquip)){
                    if(Input.GetKey(listenToKey)){
                        gameManager.RemoveUseText();
                        GameObject.Destroy(other.GetComponentInParent<ParticleSystem>().gameObject);
                        PulledOutFireCount++;

                        if(PulledOutFireCount == 16){
                            gameManager.doneTask(2, 2);
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other){
        switch(other.tag){
            case "teleport":
                if(IsInMap1){
                    if(gameManager.randomPortal == int.Parse(other.name))
                        gameManager.RemoveUseText();
                }else{
                    gameManager.RemoveUseText();
                }
                break;
            case "battery":
                gameManager.RemoveUseText();
                break;
            case "shovel":
                gameManager.RemoveUseText();
                break;
            case "power":
                if(playerManager.inventory.IsHave(battery))
                    gameManager.RemoveUseText();
                break;
            case "flashlight":
                gameManager.RemoveUseText();
                break;
            case "firstaid":
                gameManager.RemoveUseText();
                break;
            case "putFirstaid":
                if(playerManager.inventory.IsHave(Item.ItemType.Firstaid))
                    gameManager.RemoveUseText();
                break;
            case "hammer":
                gameManager.RemoveUseText();
                break;
            case "computer":
                if(gameManager.isPowerOn){
                    if(!isComputerUsed)
                        gameManager.RemoveUseText();
                }
                break;
            case "dig":
                if(playerManager.inventory.IsHave(shovel))
                    gameManager.RemoveUseText();
                break;
            case "paper":
                gameManager.RemoveUseText();  
                break;
            case "fireEquip":
                gameManager.RemoveUseText();
                break;
            case "fire":
                if(playerManager.inventory.IsHave(Item.ItemType.FireEquip))
                    gameManager.RemoveUseText();
                break;
            default:
                break;
        }
    }

    private IEnumerator Teleport(int name){
        if(gameManager.taskDoneCount == 3){
            anim.SetTrigger("blink");
            yield return new WaitForSeconds(0.5f);

            switch(name){
                case 1:
                    Vector3 pos1 = SpawnPosMap2.transform.position;
                    gameObject.transform.position = new Vector3(pos1.x, pos1.y, pos1.z);
                    break;
                case 2:
                    Vector3 pos2 = SpawnPosMap2.transform.position;
                    gameObject.transform.position = new Vector3(pos2.x, pos2.y, pos2.z);
                    break;
                case 3:
                    Vector3 pos3 = SpawnPosMap2.transform.position;
                    gameObject.transform.position = new Vector3(pos3.x, pos3.y, pos3.z);
                    break;
                case 4:
                    Vector3 pos4 = SpawnPosMap2.transform.position;
                    gameObject.transform.position = new Vector3(pos4.x, pos4.y, pos4.z);   
                    break;
                default:
                    break;
            }
            RenderSettings.skybox = skyMaterial[2];

            gameManager.task0.SetActive(false);
            gameManager.task1.SetActive(true);
            gameManager.tasksPanel.SetActive(true);
            gameManager.taskDoneCount = 0;
        }else{
            gameManager.CreateErrorText("You Have To Complete All The Tasks.");
        }
    }

    public void closePaper2Panel(){
        Paper2Panel.SetActive(false);

        playerMove.enabled = true;
        playerCam.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private IEnumerator PlayAnim(){
        endAnim.SetTrigger("RunAnim");
        yield return new WaitForSeconds(30f);
        Application.Quit();
    }
}
