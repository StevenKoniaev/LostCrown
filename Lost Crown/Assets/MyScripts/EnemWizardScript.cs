using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemWizardScript : MonoBehaviour
{
   public Rigidbody2D rb;
  
    public Animator animator;
    public int maxHealth = 180;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
       
    }

   public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GetComponent<WizardCombat>().Knockback(100);
        //Hurt anim
        animator.SetTrigger("Hurt");
        //Check current health if it is 0 or not
       
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Dead", true);
        //Die and disable 
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<WizardCombat>().enabled = false; 
       
        rb.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        this.enabled = false;
       
    }
}
