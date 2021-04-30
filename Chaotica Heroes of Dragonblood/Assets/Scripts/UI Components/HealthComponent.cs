using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    #region  Unity Editor Seen Variables
    [Header("Components")]
    [SerializeField]
    private Animator characterAnimator;
    [SerializeField]
    private Slider healthSlider;
    [Header("Health")]
    [SerializeField]
    private float characterHealth = 100;
    [SerializeField]
    private bool isPlayer;
    #endregion

    #region Awake - Update Functions
    private void Awake() 
    {
        if(isPlayer)
            healthSlider = GetComponent<Slider>();    
        healthSlider.value = characterHealth;
    }
    void Update()
    {
        //This part of the block is purely for testing
        if(Input.GetKeyDown(KeyCode.T))
        {
            PlayerDamaged(7);
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            PlayerHealed(20);
        }

        //Set slider value
        healthSlider.value = characterHealth;
    }
    #endregion

    #region  Player Damaged - Healed Functions
    void PlayerDamaged(float damage)
    {
        //Simple adjustment to health
        characterHealth -= damage;
        characterHealth = Mathf.Clamp(characterHealth,0,100);
        //Trigger the hit animation
        characterAnimator.SetTrigger("Hit");
        //Check death and if death is true Death animation will occure
        if(characterHealth <= 0)
        {   
            characterAnimator.SetBool("Death",true);
        }
    }

    void PlayerHealed(float heal)
    {
        //Simple adjustment to health
        characterHealth += heal;
        characterHealth = Mathf.Clamp(characterHealth,0,100);
    }
    #endregion

    #region Getter Functions
    public float GetCharacterHealth()
    {
        return characterHealth;
    }
    #endregion
}
