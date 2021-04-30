using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : ScriptableObject 
{

    #region  Unity Editor Seen Variables
    [SerializeField]
    private string itemName;
    [SerializeField]
    private float itemPrice;
    [SerializeField]
    private Sprite itemSprite;
    #endregion

    #region  Getter Functions
    public string GetItemName()
    {
        return itemName;
    }

    public float GetItemPrice()
    {
        return itemPrice;
    }
    public Sprite GetItemSprite()
    {
        return itemSprite;
    }
    #endregion

}
