using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableComponent : MonoBehaviour
{
    #region Unity Editor Seen Variables
    [SerializeField]
    private ItemComponentManager playerItemComponentManager;
    [SerializeField]
    private ConsumableObject consumableObject;
    [SerializeField]
    #endregion

    #region Update - Awake

    private void Awake() 
    {
        playerItemComponentManager = GetComponent<ItemComponentManager>();    
    }

    private void Update() 
    {
        if(transform.parent != GameObject.Find("Consumables").transform)
        {
            playerItemComponentManager.PlayerDistance();
            playerItemComponentManager.PlayerPickUpObject("Consumables");
        }
    }
    #endregion

    #region Getter Functions
    public ConsumableObject GetConsumableDetails()
    {
        return consumableObject;
    }
    #endregion

}
