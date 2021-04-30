using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryComponent : MonoBehaviour
{
    #region Unity Editor Seen Variables
    [SerializeField]
    private List<WeaponComponent> weaponObjects;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private Image weaponSlotImageUp;
    [SerializeField]
    private Image weaponSlotImageDown;
    #endregion

    #region Variables
    private int weaponListIndex = 0;
    private WeaponComponent currentlyActiveWeapon;
    #endregion

    #region  Add - Delete - Switch Weapon
    public void AddWeaponToInventory(WeaponComponent weapon)
    {
        //Add a new weapon to the list and set it deactive
        weaponObjects.Add(weapon);
        weapon.gameObject.SetActive(false);
    }
    public void DeleteWeaponInventory(WeaponComponent weapon)
    {
        //Removing item from the list
        weaponObjects.Remove(weapon);
    }
    //Simple swap intruction with setting the items active and deactive
    private void SwitchWeapon()
    {
        //These KeyCode's are just for testing
        int index = weaponListIndex;
        if(Input.GetKeyDown(KeyCode.P))
        {
            weaponListIndex++;
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            weaponListIndex--;
        }

        //Make sure the list index is not out of bounds
        weaponListIndex = Mathf.Clamp(weaponListIndex,0,weaponObjects.Count - 1);

        if(weaponListIndex != index)
        {
            //Swap intruction
            currentlyActiveWeapon.gameObject.SetActive(false);
            currentlyActiveWeapon = weaponObjects[weaponListIndex];
            currentlyActiveWeapon.gameObject.SetActive(true);
            playerAnimator.Play(currentlyActiveWeapon.GetWeaponObjectDetails().GetIdleAnimationName());
            if(currentlyActiveWeapon.GetWeaponObjectDetails().GetItemSprite() != null)
            {
                weaponSlotImageUp.enabled = true;
                weaponSlotImageUp.sprite = currentlyActiveWeapon.GetWeaponObjectDetails().GetItemSprite();
            }
            else
                weaponSlotImageUp.enabled = false;
        }
        //Debug.Log(weaponListIndex);
    }
    #endregion
    
    #region Awake - Update
    private void Awake() 
    {
        currentlyActiveWeapon = weaponObjects[0];
        playerAnimator.Play(currentlyActiveWeapon.GetWeaponObjectDetails().GetIdleAnimationName());   
    }

    private void Update() 
    {
        SwitchWeapon();
    }
    #endregion

    public WeaponComponent GetCurrentWeapon()
    {
        return currentlyActiveWeapon;
    }
}
