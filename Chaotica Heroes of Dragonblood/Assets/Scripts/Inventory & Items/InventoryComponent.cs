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
    private List<ConsumableComponent> consumableObjects;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private Image weaponSlotImageUp;
    [SerializeField]
    private Image weaponSlotImageDown;
    [SerializeField]
    private Image consumableSlotImageLeft;
    [SerializeField]
    private Image consumableSlotImageRight;
    #endregion

    #region Variables
    private int weaponListIndex = 0;
    private int consumableListIndex = 0;
    private WeaponComponent currentlyActiveWeapon;
    private ConsumableComponent currentlyActiveConsumable;
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
        Debug.Log("Weapon:" + weaponListIndex);
    }
    #endregion


    public void AddConsumableToInventory(ConsumableComponent consumable)
    {
        //Add a new consumable to the list and set it deactive
        consumableObjects.Add(consumable);
        consumable.gameObject.SetActive(false);
    }
    public void DeleteConsumableInventory(ConsumableComponent consumable)
    {
        //Removing item from the list
        consumableObjects.Remove(consumable);
    }

    private void SwitchConsumable()
    {
        //These KeyCode's are just for testing
        int index = consumableListIndex;
        if(Input.GetKeyDown(KeyCode.L))
        {
            consumableListIndex++;
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            consumableListIndex--;
        }

        //Make sure the list index is not out of bounds
        consumableListIndex = Mathf.Clamp(consumableListIndex,0,consumableObjects.Count - 1);

        if(consumableListIndex != index)
        {
            //Swap intruction
            currentlyActiveConsumable.gameObject.SetActive(false);
            currentlyActiveConsumable = consumableObjects[consumableListIndex];
            currentlyActiveConsumable.gameObject.SetActive(true);
            if(currentlyActiveConsumable.GetConsumableDetails().GetItemSprite() != null)
            {
                consumableSlotImageLeft.enabled = true;
                consumableSlotImageLeft.sprite = currentlyActiveConsumable.GetConsumableDetails().GetItemSprite();
            }
            else
                consumableSlotImageLeft.enabled = false;
        }
        Debug.Log("Consumable: " + consumableListIndex);
    }

    #region Awake - Update
    
    private void Awake() 
    {
        currentlyActiveWeapon = weaponObjects[0];
        playerAnimator.Play(currentlyActiveWeapon.GetWeaponObjectDetails().GetIdleAnimationName());
        currentlyActiveConsumable = consumableObjects[0];

    }

    private void Update() 
    {
        SwitchWeapon();
        SwitchConsumable();
    }
    #endregion

    public WeaponComponent GetCurrentWeapon()
    {
        return currentlyActiveWeapon;
    }
}
