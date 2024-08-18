using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Trigger trigger;

    bool IsInCutScene = true;
    public List<string> CutSceneText = new List<string>(){}; 
    public int textIndex = 0;

    public GameObject CutSceneTextPanel;
    public Text cutSceneTex;
    public float fadeDuration = 0.5f;

    public PlayerMove playerMove;
    public PlayerCam playerCam;

    public GameObject tasksPanel;
    public GameObject task0;
    public int taskDoneCount = 0;
    public GameObject task1;
    public Text t1;
    public Text t2;
    public Text t3;
    public Text t4;
    public Text t5;
    public Text t6;

    public GameObject UiInventory;

    public GameObject UseTextObjectPrefab;
    public GameObject UsementTextObjectPrefab;
    public GameObject ErrorTextObjectPrefab;
    public Transform CanvasContent;

    public bool isPowerOn = false;
    public Material WhiteLight;
    public Material BlackMat;
    public Material WhiteMat;
    public Transform[] CeilingLights;
    public Transform[] ComputerScreens;

    public GameObject paper;
    public int randomPassword;
    public int randomPortal = 0;

    void Start(){
        foreach(Transform ceilingLight in CeilingLights){
            ceilingLight.Find("Ceiling_lights_lo").GetComponent<MeshRenderer>().material = BlackMat;
            ceilingLight.GetComponentInChildren<Light>().enabled = false;
        }

        playerMove.enabled = false;
        playerCam.enabled = false;
        CutSceneTextPanel.gameObject.SetActive(true);
        StartCoroutine(StartCutScene());
    }

    private IEnumerator StartCutScene(){
        yield return new WaitForSeconds(1f);
        StartCoroutine(changeText());
    }

    void Update(){
        if(IsInCutScene){
            if(Input.GetKeyDown(KeyCode.Space)){
                textIndex ++;
                StartCoroutine(changeText());
            }
        }
    }

    private IEnumerator changeText(){
        switch(textIndex){
            case 0:
                yield return StartCoroutine(FadeTextToZeroAlpha());
                cutSceneTex.text = $"- {CutSceneText[0]}";
                yield return StartCoroutine(FadeTextToFullAlpha());
                break;
            case 1:
                yield return StartCoroutine(FadeTextToZeroAlpha());
                cutSceneTex.text = $"- {CutSceneText[1]}";
                yield return StartCoroutine(FadeTextToFullAlpha());
                break;
            case 2:
                yield return StartCoroutine(FadeTextToZeroAlpha());
                cutSceneTex.text = $"- {CutSceneText[2]}";
                yield return StartCoroutine(FadeTextToFullAlpha());
                break;
            case 3:
                yield return StartCoroutine(FadeTextToZeroAlpha());  
                cutSceneTex.text = $"- {CutSceneText[3]}";
                yield return StartCoroutine(FadeTextToFullAlpha());
                break;
            case 4:
                yield return StartCoroutine(FadeTextToZeroAlpha());
                cutSceneTex.text = $"- {CutSceneText[4]}";
                StartCoroutine(FadeTextToFullAlpha());
                break;
            case 5:
                yield return StartCoroutine(FadeTextToZeroAlpha());
                cutSceneTex.text = $"- {CutSceneText[5]}";
                yield return StartCoroutine(FadeTextToFullAlpha());
                break;
            case 6:
                CutSceneTextPanel.gameObject.SetActive(false);
                playerMove.enabled = true;
                playerCam.enabled = true;
                IsInCutScene = false;
                UiInventory.SetActive(true);

                yield return new WaitForSeconds(2f);
                tasksPanel.gameObject.SetActive(true);
                task0.gameObject.SetActive(true);

                break;
            default:
                break;
        }
    }

    public IEnumerator FadeTextToFullAlpha()
    {
        cutSceneTex.color = new Color(cutSceneTex.color.r, cutSceneTex.color.g, cutSceneTex.color.b, 0);
        while (cutSceneTex.color.a < 1.0f)
        {
            cutSceneTex.color = new Color(cutSceneTex.color.r, cutSceneTex.color.g, cutSceneTex.color.b, cutSceneTex.color.a + (Time.deltaTime / fadeDuration));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha()
    {
        cutSceneTex.color = new Color(cutSceneTex.color.r, cutSceneTex.color.g, cutSceneTex.color.b, 1);
        while (cutSceneTex.color.a > 0.0f)
        {
            cutSceneTex.color = new Color(cutSceneTex.color.r, cutSceneTex.color.g, cutSceneTex.color.b, cutSceneTex.color.a - (Time.deltaTime / fadeDuration));
            yield return null;
        }
    }


    public void doneTask(int task, int taskIndex){
        if(task == 1){
            switch(taskIndex){
                case 1:
                    t1.color = Color.gray;
                    t1.fontStyle = FontStyle.Italic;
                    taskDoneCount++;
                    break;
                case 2:
                    t2.color = Color.gray;
                    t2.fontStyle = FontStyle.Italic;
                    taskDoneCount++;
                    break;
                case 3:
                    t3.color = Color.gray;
                    t3.fontStyle = FontStyle.Italic;
                    taskDoneCount++;
                    break;
                default:
                    break;
            }
        }else if(task == 2){
            switch(taskIndex){
                case 1:
                    t4.color = Color.gray;
                    t4.fontStyle = FontStyle.Italic;
                    taskDoneCount++;
                    break;
                case 2:
                    t5.color = Color.gray;
                    t5.fontStyle = FontStyle.Italic;
                    taskDoneCount++;
                    break;
                case 3:
                    t6.color = Color.gray;
                    t6.fontStyle = FontStyle.Italic;
                    taskDoneCount++;
                    break;
                default:
                    break;
            }

            if(taskDoneCount == 3){
                trigger.GetBackTextPanel.SetActive(true);
                trigger.GetBackPortal.gameObject.SetActive(true);
            }
        }
    }

    public void TurnOnPower(){
        foreach(Transform ceilingLight in CeilingLights){
            ceilingLight.Find("Ceiling_lights_lo").GetComponent<MeshRenderer>().material = WhiteLight;
            ceilingLight.GetComponentInChildren<Light>().enabled = true;
        }
        foreach(Transform computerSceen in ComputerScreens){
            computerSceen.GetComponent<MeshRenderer>().material = WhiteMat;
        }
        isPowerOn = true;
    }

    public int SpawnPaper(){
        randomPassword = Random.Range(10000, 99999);
        paper.GetComponentInChildren<Text>().text = randomPassword.ToString();
        int paperRandomIndex = Random.Range(1, 3);
        return paperRandomIndex;
    }

    GameObject useText;
    public void CreateUseText(string text){
        GameObject useTextObject = Instantiate(UseTextObjectPrefab, CanvasContent);
        useTextObject.GetComponentInChildren<Text>().text = text;
        useText = useTextObject;
    }

    public void RemoveUseText(){
        if(useText == null){
            return;
        }
        GameObject.Destroy(useText);
    }

    GameObject UsementText;
    public IEnumerator CreateUsementText(string itemName, string itemInfo, Sprite itemImg){
        if(UsementText != null){
            yield return WaitUsement();
        }
        GameObject usementTextObject = Instantiate(UsementTextObjectPrefab, CanvasContent);

        Text[] texts = usementTextObject.GetComponentsInChildren<Text>();
        foreach(Text text in texts){
            if(text.gameObject.name == "ItemName"){
                text.text = itemName;
            }else if(text.gameObject.name == "ItemUsement"){
                text.text = itemInfo;
            }
        }

        Image[] images = usementTextObject.GetComponentsInChildren<Image>();
        foreach(Image image in images){
            if(image.gameObject.name == "ItemImg"){
                image.sprite = itemImg;
            }
        }

        UsementText = usementTextObject;

        StartCoroutine(RemoveUsementText());
    }

    public IEnumerator RemoveUsementText(){
        if(UsementText != null){
            yield return new WaitForSeconds(10f);
            GameObject.Destroy(UsementText);
        }
    }

    GameObject errorText;
    public IEnumerator CreateErrorText(string text){
        if(errorText != null){
            yield return WaitError();
        }
        GameObject errorTextObject = Instantiate(ErrorTextObjectPrefab, CanvasContent);
        errorTextObject.GetComponentInChildren<Text>().text = text;
        errorText = errorTextObject;

        StartCoroutine(RemoveErrorText());
    }

    public IEnumerator RemoveErrorText(){
        if(errorText != null){
            yield return new WaitForSeconds(5f);
            GameObject.Destroy(errorText);
        }
    }

    bool WaitConditionUsement() {
        return UsementText.IsDestroyed();
    }

    IEnumerator WaitUsement() {
        yield return new WaitUntil(WaitConditionUsement);
    }

    bool WaitConditionError() {
        return errorText.IsDestroyed();
    }

    IEnumerator WaitError() {
        yield return new WaitUntil(WaitConditionError);
    }
}