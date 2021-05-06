using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

#region  Enums'
enum Damage
{
    NECROTIC, FIRE, ACID, ICE, METAL
}

enum Type
{
    HAMMER
}
#endregion
[CreateAssetMenu(fileName = "WeaponItem", menuName = "WeaponItem")]
public class WeaponObject : ItemObject
{
    #region Unity Editor Seen Variables
    [Header("Types")]
    [SerializeField]
    private Type WeaponType;
    [SerializeField]
    private Damage WeaponDamageType;
    [Header("Light Attacks")]
    [SerializeField]
    private float weaponDamageLight;
    [SerializeField]
    private float weaponHitRateLight;
    [SerializeField]
    private float weaponLightAttackRadius;
    [SerializeField]
    private float weaponStaminaDecayLight;
    [Header("Heavy Attacks")]
    [SerializeField]
    private float weaponDamageHeavy;
    [SerializeField]
    private float weaponHitRateHeavy;
    [SerializeField]
    private float weaponStaminaDecayHeavy;
    [SerializeField]
    private float weaponHeavyAttackRadius;
    [SerializeField]
    private float weaponHeavyMouseHold;

    [Header("Animation")]
    [SerializeField]
    private string idleAnimationStateName;
    #endregion

    #region Getter Functions
    public string GetIdleAnimationName()
    {
        return idleAnimationStateName;
    }
    #endregion


    #region Light Attack Functions
    public float GetLightAttackRate()
    {
        return weaponHitRateLight;
    }
    public float GetStaminaDecayLight()
    {
        return weaponStaminaDecayLight;
    }
    public float GetLightAttackRadius()
    {
        return weaponLightAttackRadius;
    }
    #endregion

    #region Heavy Attack Functions
    public float GetHeavyAttackRate()
    {
        return weaponHitRateHeavy;
    }
    public float GetStaminaDecayHeavy()
    {
        return weaponStaminaDecayHeavy;
    }
    public float GetHeavyAttackRadius()
    {
        return weaponLightAttackRadius;
    }

    public float GetHeavyAttackMouseHoldTime()
    {
        return weaponHeavyMouseHold;
    }
    #endregion

}
