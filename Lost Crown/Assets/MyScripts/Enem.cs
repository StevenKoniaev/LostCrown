using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public int maxHealth = 180;
    public int currentHealth;
    public GameObject boss;
    public GameObject myself;
    public BossHealthScript healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
       
    }

   public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        //Hurt anim
        animator.SetTrigger("Hurt");
        //Check current health if it is 0 or not
      if (boss == myself)
        {
            healthbar.SetBossHealth(currentHealth);
        }

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
        rb.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        this.enabled = false;
     

       
    }



}
