using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonKing : MonoBehaviour
{
    public Animator animator;
  
   public BossDialogScript dialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Summon", true);
            dialog.GetComponent<BossDialogScript>().canStartDialogue = true;
            this.enabled = false;
        }
    }
}