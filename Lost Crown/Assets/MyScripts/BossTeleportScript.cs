using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleportScript : MonoBehaviour
{
    float nextTeleportTime = 0f;
    public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time >= nextTeleportTime)
        {
            nextTeleportTime = Time.time + 3f;
            anim.SetBool("GroundAttack", false);
            anim.SetBool("MaceAttack", false);
            anim.SetBool("MaceAttackTwo", false);
            anim.SetBool("EnemWalking", false);
            anim.SetTrigger("Teleporting");
        }
       
    }
}
