using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ConsumableType
{
    HEALTH,
    ENDURANCE,
    MANA,
    WEAPON_SPEED,
    WEAPON_DAMAGE,
    RESISTANCE_1,
    RESISTANCE_2,
    RESISTANCE_3
}
[CreateAssetMenu(fileName = "ConsumableItem", menuName = "ConsumableItem")]
public class ConsumableObject : ItemObject
{
    [SerializeField]
    private ConsumableType typeOfConsumable;
    [SerializeField]
    private float consumableEffectTime;
    [SerializeField]
    private float consumableEffectAmount;
}
