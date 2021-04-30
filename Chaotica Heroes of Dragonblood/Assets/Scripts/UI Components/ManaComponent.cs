using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaComponent : MonoBehaviour
{
    [SerializeField]
    private Slider manaSlider;
    [SerializeField]
    private float characterMana;
    [SerializeField]
    private float manaRegenDelay;
    [SerializeField]
    private float manaRegenValue;


    private float manaRegenTimer = 0f;
    private bool manaUsed = false;

    private void Awake() 
    {
        manaSlider = GetComponent<Slider>();
        manaSlider.value = characterMana;
    }

    void Update()
    {
        manaSlider.value = characterMana;
        ManaRegen();

        if(Input.GetKeyDown(KeyCode.U))
        {
            UseMana(25);
        }
    }

    public void UseMana(float value)
    {
        characterMana -= value;
        characterMana = Mathf.Clamp(characterMana,0f,100f);
        manaUsed = true;
        manaRegenTimer = 0f;
    }

    public void GainMana(float value)
    {
        characterMana += value;
        characterMana = Mathf.Clamp(characterMana,0f,100f);
    }

    void ManaRegen()
    {
        if(manaUsed)
        {
            if(manaRegenTimer >= manaRegenDelay)
            {
                manaUsed = false;
                manaRegenTimer = 0f;
            }
            manaRegenTimer += Time.deltaTime;
        }
        else
        {
            characterMana += manaRegenValue * Time.deltaTime;
        }
    }

    public float GetCharacterMana()
    {
        return characterMana;
    }
}
