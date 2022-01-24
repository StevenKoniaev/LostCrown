using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooling : MonoBehaviour
{
    public GameObject Enemy;

    public void StopScript()
    {
        Enemy.GetComponent<EnemyCombat>().enabled = false;
    }
    public void Knockbackbegin()
    {
        Enemy.GetComponent<EnemyCombat>().Knockback(20);
    }

    public void cool()
    {
        
        Enemy.GetComponent<EnemyCombat>().TriggerCooling();
    }

    public void AttackDamage()
    {
        Enemy.GetComponent<EnemyCombat>().HitBox();
    }

    public void KnockbackTransfer()
    {
        Enemy.GetComponent<EnemyCombat>().KnockbackStop();
    }
}
