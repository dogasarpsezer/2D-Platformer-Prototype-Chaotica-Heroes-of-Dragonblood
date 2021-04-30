using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private InventoryComponent playerInventory;
    [SerializeField]
    private Animator playerAnimator;

    private int lightAttackCounter = 0;
    private float lightAttackTimer = 0;
    private float lightAttackTimerOverall = 0;
    private bool lightAttack = false;
    private bool lightAttackOverall = false;
    private void Awake() 
    {
        playerInventory = transform.Find("Inventory").GetComponent<InventoryComponent>(); 
        playerAnimator = transform.Find("PlayerSprite").GetComponent<Animator>();   
    }

    private void Update() 
    {
        PlayerAttackFunc();
    }

    void PlayerAttackFunc()
    {
        PlayerLightAttack();

    }

    void PlayerLightAttack()
    {
        if(Input.GetMouseButtonDown(0) && !lightAttack && playerInventory.GetCurrentWeapon().GetWeaponObjectDetails().GetItemName() != "Deneme")
        {
            lightAttack = true;
            lightAttackCounter++;
            lightAttackOverall = false;
            Debug.Log(lightAttackCounter);
            if(lightAttackCounter % 2 != 0)
                playerAnimator.SetTrigger("First Attack");
            else
                playerAnimator.SetTrigger("Second Attack");
        }
        
        /*if(Input.GetMouseButtonUp(0))
        {
            lightAttackOverall = true;
        }

        if(lightAttack)
        {
            lightAttackTimer += Time.deltaTime;
            if(lightAttackTimer >= playerInventory.GetCurrentWeapon().GetWeaponObjectDetails().GetLightAttackRate())
            {
                lightAttackTimer = 0;
                lightAttack = false;
            }
        }

        if(lightAttackOverall)
        {
            lightAttackTimerOverall += Time.deltaTime;
            if(lightAttackTimerOverall >= 0.3)
            {
                lightAttackTimerOverall = 0;
                lightAttackCounter = 0;
                lightAttackOverall = false;
                playerAnimator.SetTrigger("Attack Finished");
            }
        }*/
    }

    void PlayerHeavyAttack()
    {

    }
}
