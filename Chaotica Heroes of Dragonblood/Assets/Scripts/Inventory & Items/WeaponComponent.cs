using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    #region Unity Editor Seen Variables
    [SerializeField]
    private ItemComponentManager playerItemComponentManager;
    [SerializeField]
    private WeaponObject weaponObject;
    #endregion

    #region Awake - Update
    private void Awake() 
    {
       playerItemComponentManager = GetComponent<ItemComponentManager>();
    }
    private void Update() 
    {
        if(transform.parent != GameObject.Find("Weapons").transform)
        {
            playerItemComponentManager.PlayerDistance();
            playerItemComponentManager.PlayerPickUpObject("Weapons");
        }
    }
    #endregion

    #region Getter Functions
    public WeaponObject GetWeaponObjectDetails()
    {
        return weaponObject;
    }
    #endregion
}
