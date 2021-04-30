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
    [SerializeField]
    private Type WeaponType;
    [SerializeField]
    private Damage WeaponDamageType;
    [SerializeField]
    private float weaponDamageLight;
    [SerializeField]
    private float weaponHitRateLight;
    [SerializeField]
    private float weaponStaminaDecayLight;
    [SerializeField]
    private float weaponDamageHeavy;
    [SerializeField]
    private float weaponHitRateHeavy;
    [SerializeField]
    private float weaponStaminaDecayHeavy;
    [SerializeField]
    private bool isOwned = false;
    [SerializeField]
    private string idleAnimationStateName;
    #endregion

    #region Getter Functions
    public string GetIdleAnimationName()
    {
        return idleAnimationStateName;
    }

    public float GetLightAttackRate()
    {
        return weaponHitRateLight;
    }
    #endregion

}
