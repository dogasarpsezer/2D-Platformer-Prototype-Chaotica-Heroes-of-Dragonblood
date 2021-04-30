using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaComponent : MonoBehaviour
{
    [SerializeField]
    private Slider staminaSlider;

    [SerializeField]
    private float characterStamina;
    [SerializeField]
    private float staminaRegenDelay;
    [SerializeField]
    private float staminaRegenValue;

    private bool staminaUsed = false;
    private float staminaRegenTimer = 0f;
  
    private void Awake() 
    {
        staminaSlider = GetComponent<Slider>();
        staminaSlider.value = characterStamina;
    }
    void Update()
    {
        staminaSlider.value = characterStamina;
        StaminaRegen();
    }

    public void UseStamina(float value)
    {
        characterStamina -= value;
        characterStamina = Mathf.Clamp(characterStamina,0f,100f);
        staminaUsed = true;
        staminaRegenTimer = 0f;
    }

    public void GainStamina(float value)
    {
        characterStamina += value;
        characterStamina = Mathf.Clamp(characterStamina,0f,100f);
    }

    void StaminaRegen()
    {
        if(staminaUsed)
        {
            if(staminaRegenTimer >= staminaRegenDelay)
            {
                staminaUsed = false;
                staminaRegenTimer = 0f;
            }
            staminaRegenTimer += Time.deltaTime;
        }
        else
        {
            characterStamina += staminaRegenValue * Time.deltaTime;
            characterStamina = Mathf.Clamp(characterStamina,0f,100f);
        }
    }

    public float GetCharacterStamina()
    {
        return characterStamina;
    }
}
