using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake(){
        Instance = this;
    }

    public Sprite batterySprite;
    public Sprite firstaidSprite;
    public Sprite flashLightSprite;
    public Sprite hammerSprite;
    public Sprite shovelSprite;
    public Sprite paperSprite;
    public Sprite fireEquipSprite;
}
