using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICrystalComponent : MonoBehaviour
{
    #region Unity Editor Seen Variables
    [Header("Components")]
    [SerializeField]
    private HealthComponent playerHealthUIComponent;
    [SerializeField]
    private StaminaComponent playerStaminaUIComponent;
    [SerializeField]
    private ManaComponent playerManaUIComponent;
    [SerializeField]
    private Image crystalSprite;
    [SerializeField]
    private float fadeValue;
    [SerializeField]
    private float fadeLimitValue;
    [SerializeField]
    private string crystalType;
    #endregion

    #region Variables
    private float playerHealth;
    private float colorValue;
    #endregion

    #region Awake - Update
    private void Awake() 
    {   
        if(crystalType == "Red")
            playerHealthUIComponent = transform.parent.GetComponent<HealthComponent>();
        else if(crystalType == "Green")
            playerStaminaUIComponent = transform.parent.GetComponent<StaminaComponent>();
        else if(crystalType == "Blue")
            playerManaUIComponent = transform.parent.GetComponent<ManaComponent>();

        crystalSprite = GetComponent<Image>();
    }
    void Update()
    {
        //Check the crystal type and change the base value for fading
        if(crystalType == "Red")
            CrystalFade(playerHealthUIComponent.GetCharacterHealth());
        else if(crystalType == "Green")
            CrystalFade(playerStaminaUIComponent.GetCharacterStamina());
        else if(crystalType == "Blue")
            CrystalFade(playerManaUIComponent.GetCharacterMana());

        //Debug.Log("Fade:" + fadeValue + "Fade Limit: " + fadeLimitValue);
    }
    #endregion

    #region  Fade Function
    void CrystalFade(float value)
    {
        //Change color value to proportion wiht chosen character value value
        colorValue = fadeLimitValue + fadeValue * value;
        colorValue = Mathf.Clamp(colorValue,0f,1f);

        //Change consumed color for crystal image
        crystalSprite.color = new Color(colorValue,colorValue,colorValue);
    }
    #endregion
}
