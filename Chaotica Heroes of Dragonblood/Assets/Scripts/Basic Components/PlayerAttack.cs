using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Unity Editor Seen Variables
    [Header("Components")]
    [SerializeField]
    private InventoryComponent playerInventory;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private StaminaComponent playerStamina;
    [SerializeField]
    private Transform centerOfAttack;
    [Header("Gizmos Options")]
    [SerializeField]
    private bool isGizmosOpen = false;
    [SerializeField]
    private float gizmosSphereRadius = 0.5f;
    [Header("Does The Scene Allow Combat")]
    //Use to enable or disable combat of the player when needed
    [SerializeField]
    private bool isCombatActive = true;
    #endregion

    #region Variables
    private float lightAttackTimer = 0f;
    private bool isAttackLight = false;
    private bool canAttackLight = true;

    private float heavyAttackTimer = 0f;
    private bool isAttackHeavy = false;
    private bool canAttackHeavy = true;
    private float mouseHoldTimer = 0;
    private WeaponComponent currentActiveWeapon;
    private WeaponObject currentActiveWeaponObject;
    #endregion

    #region Awake & Update
    private void Awake() 
    {
        playerInventory = transform.Find("Inventory").GetComponent<InventoryComponent>(); 
        playerAnimator = transform.Find("PlayerSprite").GetComponent<Animator>();   
    }

    private void Update() 
    {   
        //Just for simplicity in the code 
        currentActiveWeapon = playerInventory.GetCurrentWeapon();
        currentActiveWeaponObject = currentActiveWeapon.GetWeaponObjectDetails();

        PlayerAttackFunc();
    }
    #endregion

    #region Player Attack Functions
    void PlayerAttackFunc()
    {
        //Check if the scene allows to combat
        if(isCombatActive)
        {
            //Initialize the time so that the program can calculate if the mouse button press is a light attack ot heavy
            if(Input.GetMouseButtonDown(0))
            {
                mouseHoldTimer = Time.time;
            }

            PlayerLightAttack();
            PlayerHeavyAttack();
        }
    }

    void PlayerLightAttack()
    {
        //Play the light attack animation when pressed to left mouse button and released until the heavy trigger, cannot attack if the player is doing the action Dodge
        if(Input.GetMouseButtonUp(0))
        {
            if(Time.time < mouseHoldTimer + currentActiveWeaponObject.GetHeavyAttackMouseHoldTime() && playerStamina.GetCharacterStamina() > currentActiveWeaponObject.GetStaminaDecayLight())
            {
                if(canAttackLight && !playerAnimator.GetBool("Dodge") && currentActiveWeaponObject.GetItemName() != "Deneme")
                {
                    isAttackLight = true;
                    playerAnimator.SetBool("isAttackLight",isAttackLight);
                    playerStamina.UseStamina(currentActiveWeaponObject.GetStaminaDecayLight());
                    canAttackLight = false;
                }
            }
        }

        //Weapon Light Attack rate will prevent spamming and every weapon will have its own attack delay
        if(!isAttackLight)
        {
            lightAttackTimer += Time.deltaTime;
            if(lightAttackTimer >= currentActiveWeaponObject.GetLightAttackRate())
            {
                lightAttackTimer = 0f;
                canAttackLight = true;
            }
        }

    }
    void PlayerHeavyAttack()
    {
        //Play the heavy attack animation when pressed to left mouse button and hold for the time of the heavy trigger, cannot attack if the player is doing the action Dodge
        if(Input.GetMouseButtonUp(0))
        {
            if(Time.time >= mouseHoldTimer + currentActiveWeaponObject.GetHeavyAttackMouseHoldTime()  && playerStamina.GetCharacterStamina() > currentActiveWeaponObject.GetStaminaDecayHeavy())
            {
                if(canAttackHeavy && !playerAnimator.GetBool("Dodge") && currentActiveWeaponObject.GetItemName() != "Deneme")
                {
                    isAttackHeavy = true;
                    playerAnimator.SetBool("isAttackHeavy",isAttackHeavy);
                    playerStamina.UseStamina(currentActiveWeaponObject.GetStaminaDecayHeavy());
                    canAttackHeavy = false;
                }
            }
        }

        //Weapon Light Attack rate will prevent spamming and every weapon will have its own attack delay
        if(!isAttackHeavy)
        {
            heavyAttackTimer += Time.deltaTime;
            if(heavyAttackTimer >= currentActiveWeaponObject.GetHeavyAttackRate())
            {
                heavyAttackTimer = 0f;
                canAttackHeavy = true;
            }
        }
    }
    #endregion

    #region Finish and Check Overlap For Attack Functions
    public void FinishLightAttack()
    {
        //This will be used as an animation event on light attack animation
        isAttackLight = false;
        playerAnimator.SetBool("isAttackLight",isAttackLight);
    } 
    public void FinishHeavyAttack()
    {
        //This will be used as an animation event on light attack animation
        isAttackHeavy = false;
        playerAnimator.SetBool("isAttackHeavy",isAttackHeavy);
    } 

    public void PlayerAttackCheckLight()
    {
        if(isAttackLight)
        {
            Collider2D[] objectsThatCanBeDamaged = Physics2D.OverlapCircleAll(centerOfAttack.position,currentActiveWeaponObject.GetLightAttackRadius());

            foreach (Collider2D objectToDamage in objectsThatCanBeDamaged)
            {
                Debug.Log("LIGHT HIT");
                //Light Attack Hasarı Koy (Send Message kullanılabilir...)
            }
        }
    }
    public void PlayerAttackCheckHeavy()
    {
        if(isAttackHeavy)
        {
            Collider2D[] objectsThatCanBeDamaged = Physics2D.OverlapCircleAll(centerOfAttack.position,currentActiveWeaponObject.GetHeavyAttackRadius());

            foreach (Collider2D objectToDamage in objectsThatCanBeDamaged)
            {
                Debug.Log("HEAVY HIT");
                //Light Attack Hasarı Koy (Send Message kullanılabilir...)
            }
        }
    }
    #endregion

    private void OnDrawGizmos() 
    {
        if(isGizmosOpen)
            Gizmos.DrawWireSphere(centerOfAttack.position,gizmosSphereRadius);    
    }
    //BURADA DEBUG İÇİN OVERLAPCIRCLE ÇİZME METODU YAZ
}
