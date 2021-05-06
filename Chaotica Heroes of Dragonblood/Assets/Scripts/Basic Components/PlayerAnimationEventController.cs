using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventController : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack playerAttack;
    

    public void FinishLightAttack()
    {
       playerAttack.FinishLightAttack();
    }

    public void FinishHeavytAttack()
    {
       playerAttack.FinishHeavyAttack();
    }

    public void LightAttackRadiusCheck()
    {
        playerAttack.PlayerAttackCheckLight();
    }

    public void HeavyAttackRadiusCheck()
    {
        playerAttack.PlayerAttackCheckHeavy();
    }
}
