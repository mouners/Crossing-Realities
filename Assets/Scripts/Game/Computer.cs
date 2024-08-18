using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject ComputerScreenPanel;
    public InputField PasswordInput;
    public Button EnterBtn;

    public TextMeshPro computerText;

    void Awake(){
        EnterBtn.onClick.AddListener(OnEnterClicked);
    }

    private void OnEnterClicked(){
        if(PasswordInput.text == gameManager.randomPassword.ToString()){
            ComputerScreenPanel.gameObject.SetActive(false);
            computerText.gameObject.SetActive(true);

            gameManager.playerMove.enabled = true;
            gameManager.playerCam.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            gameManager.randomPortal = Random.Range(1, 4);
            switch(gameManager.randomPortal){
                case 1:
                    computerText.text = "Red Portal";
                    break;
                case 2:
                    computerText.text = "Green Portal";
                    break;
                case 3:
                    computerText.text = "Blue Portal";
                    break;
                case 4:
                    computerText.text = "Yellow Portal";
                    break;
                default:
                    break;
            }
            gameManager.doneTask(1, 2);
            gameManager.UiInventory.SetActive(true);
            gameManager.tasksPanel.gameObject.SetActive(true);
        }
    }
}
