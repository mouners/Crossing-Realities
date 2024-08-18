
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour{
    public PlayerManager playerManager;
    public Transform canvas;

    public Image img1;
    public Image img2;
    public Image img3;

    public Transform target1;
    public Transform target2;
    public Transform target3;

    bool isSpawnedWaypoints = false;

    List<int> removedIndex = new List<int>();

    void Update(){
        if(playerManager.inventory.IsHave(Item.ItemType.Shovel)){
            if(!isSpawnedWaypoints){
                SpwanWaypoints();
                isSpawnedWaypoints = true;
            }
            if(target1 != null && img1 != null){
                UpdateWaypoint(target1, img1);
            }

            if(target2 != null && img2 != null){
                UpdateWaypoint(target2, img2);
            }

            if(target3 != null && img3 != null){
                UpdateWaypoint(target3, img3);
            }
        }else{
            if(img1 != null){
                img1.gameObject.SetActive(false);
            }

            if(img2 != null){
                img2.gameObject.SetActive(false);
            }

            if(img3 != null){
                img3.gameObject.SetActive(false);
            }
        }   
    }

    private void SpwanWaypoints(){
        img1.gameObject.SetActive(true);
        img2.gameObject.SetActive(true);
        img3.gameObject.SetActive(true);
    }

    private void UpdateWaypoint(Transform target, Image img){
        img.gameObject.SetActive(true);
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);

        if(Vector3.Dot((Vector3) (target.position - transform.position), transform.forward) < 0){
            if(pos.x < Screen.width / 2)
                pos.x = maxX;
            else
                pos.x = minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        img.GetComponentInChildren<Text>().text = ((int) Vector3.Distance(target.position, transform.position)).ToString() + "m";
    }

    public void RemoveWaypoint(int index){
        if(index == 1){
            if(target1 != null && img1 != null){
                GameObject.Destroy(target1.gameObject);
                GameObject.Destroy(img1.gameObject);
            }
        }else if(index == 2){
            if(target2 != null && img2 != null){
                GameObject.Destroy(target2.gameObject);
                GameObject.Destroy(img2.gameObject);
            }
        }else if(index == 3){
            if(target3 != null && img3 != null){
                GameObject.Destroy(target3.gameObject);
                GameObject.Destroy(img3.gameObject);
            }
        }
    }
}