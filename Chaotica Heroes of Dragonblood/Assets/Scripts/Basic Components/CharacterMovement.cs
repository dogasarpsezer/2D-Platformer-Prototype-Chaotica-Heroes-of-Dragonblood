using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region  Unity Editor Seen Variables
    [Header("Components")]
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private Rigidbody2D playerRb2D;
    [SerializeField]
    private HealthComponent playerHealthComponent;
    [SerializeField]
    private StaminaComponent playerStaminaComponent;
    [SerializeField]
    private GameObject inventoryObject;
    [Header("Movement")]
    [SerializeField]
    [Range(0f,10f)]
    private float characterRunSpeed;
    [Header("Jump")]
    [SerializeField]
    private GroundCheck playerGroundCheck;
    [SerializeField]
    [Range(0f,50f)]
    private float characterJumpSpeed;
    [SerializeField]
    [Range(0f,10f)]
    private float jumpTimeLimit;
    [SerializeField]
    [Range(0f,1f)]
    private float jumpCooldown = 0.2f;
    [SerializeField]
    [Range(0f,0.5f)]
    private float jumpMinTimer;
    [Header("Dodge")]
    [SerializeField]
    [Range(0f,30f)]
    private float dodgeSpeed;
    [SerializeField]
    [Range(0f,10f)]
    private float dodgeTimeLimit;
    [SerializeField]
    [Range(0f,1f)]
    private float dodgeCooldown = 0.2f;
    #endregion

    #region Variables
    private float horizontal = 0f;
    private float jumpTimer = 0f;
    private bool jumpState = false;
    private float jumpCooldownTimer = 0f;
    private float dodgeTimer = 0f;
    private bool isDodge = false;
    private float dodgeCooldownTimer;
    private bool isInventoryActive = false;
    private Vector3 velocity;
    private Vector3 initialPosition;
    #endregion Variables

    #region Awake - Start - Update
    private void Awake() 
    {
        //Getting necessary componenets
        playerAnim = GetComponentInChildren<Animator>();
        playerGroundCheck = GetComponentInChildren<GroundCheck>();    
        playerRb2D = GetComponent<Rigidbody2D>(); 
        inventoryObject = transform.Find("Inventory").gameObject;

        if(inventoryObject.activeInHierarchy)
        {
            isInventoryActive = true;
        }
    }

    void Update()
    {
        PlayerMovement();
        //Debug.Log(playerGroundCheck.GetGroundState());
    }
    #endregion

    #region Player Movement - Jump - Animations - Dodge
    void PlayerMovement()
    {
        //Check if the players Death animation is played
        if(!playerAnim.GetBool("Death"))
        {
            //Get horizontal input
            horizontal = Input.GetAxisRaw("Horizontal");

            PlayerAnimation();
            //Calculate player Velocity and move the character on a horizontal line
            initialPosition = transform.position;
            //Debug.Log(horizontal);
            transform.position += Vector3.right * horizontal * characterRunSpeed * Time.deltaTime;
            velocity = (transform.position - initialPosition)/Time.deltaTime;
            PlayerJump();
            PlayerDodge();
        }
    }


    void PlayerAnimation()
    {
        //Set Animation variables
        playerAnim.SetFloat("Horizontal", Mathf.Abs(horizontal));
        playerAnim.SetBool("Jump",jumpState);
        playerAnim.SetBool("Grounded",playerGroundCheck.GetGroundState());
        playerAnim.SetBool("Dodge",isDodge);


        //Flipping the character towards the characters horizontal movement
        if(horizontal > 0f)
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
        else if(horizontal < 0f)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
        }
    }

    void PlayerJump()
    {
        //For jumping cooldown check if the player is already jumping and if he is on ground
        if(!jumpState && playerGroundCheck.GetGroundState())
        {
            //Simple time counter and limiting the variable for safety
            jumpCooldownTimer +=Time.deltaTime;
            Mathf.Clamp(jumpCooldownTimer,0f,jumpCooldown + 0.1f);
        }

        //Getting key input for jumping, however you also have to check if the player is on the ground and if the cooldown is over
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0) && playerGroundCheck.GetGroundState() && jumpCooldownTimer >= jumpCooldown && playerStaminaComponent.GetCharacterStamina() > 5f)
        {
            //Reset the cooldown and initiate jumping
            jumpState = true;
            jumpCooldownTimer = 0f;
            playerStaminaComponent.UseStamina(15);
        }

        //Doing the actual jumping for a special amount of time
        if(jumpState && jumpTimer < jumpTimeLimit)
        {
            //Jump position change
            transform.position += new Vector3(0f,characterJumpSpeed*Time.deltaTime);

            //Timer for jumping length
            jumpTimer += Time.deltaTime;

            //To cut off jump speed if the characters jump time is over or if the player pulled their hand from the input key or if the player decided do dodge right after jump
            if(jumpTimer >= jumpTimeLimit || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.JoystickButton0) || playerAnim.GetBool("isAttackLight") || playerAnim.GetBool("isAttackHeavy"))
            {
                jumpTimer = 0f;
                jumpState = false;
            }
        }
    }

    void PlayerDodge()
    {
        //For dodge cooldwon check if the player is already doing the dodge
        if(!isDodge)
        {
            //Simple timer and limiter(for safety)
            dodgeCooldownTimer += Time.deltaTime;
            Mathf.Clamp(dodgeCooldownTimer,0f,dodgeCooldown + 0.1f);
        }

        //Check if the player pressed the input key for dodge, if the player is on the ground, if the player character is actually moving horizontally and if the cooldown is over
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton1) && playerGroundCheck.GetGroundState() && Mathf.Abs(velocity.x) > 0.2f && Mathf.Abs(velocity.y) <= 0.1f && dodgeCooldownTimer >= dodgeCooldown && playerStaminaComponent.GetCharacterStamina() > 5f)
        {
            //Initiate dodge and reset cooldown
            isDodge = true;
            dodgeCooldownTimer = 0f;
            playerStaminaComponent.UseStamina(20);
        }
        //Inititate Dodge
        if(isDodge)
        {
            //Check the flipped side of the character
            if(transform.localScale.x == 1f)
            {   
                //Move the character positive horizotnal          
                transform.position += transform.right * dodgeSpeed * Time.deltaTime;
            }
            else
            {
                //Move the character negative horizontal
                transform.position += -transform.right * dodgeSpeed * Time.deltaTime;
            }
            //Simple Timer
            dodgeTimer += Time.deltaTime;
            //Check for dodge time length, if the payer pulled their hand from input key or if they pressed jumping input key
            if(dodgeTimer >= dodgeTimeLimit || Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton1) )
            {
                //Reset Dodge timer
                dodgeTimer = 0f;
                //Finish Dodge
                isDodge = false;
            } 
        }
    }

    public bool GetInventoryState()
    {
        return isInventoryActive;
    }
    #endregion
}
