using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    //Variables
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int atkDamage = 40;
    public float atkRate = 2f;
    float nextAttackTime = 0f;
    Rigidbody2D m_Rigidbody;
    public int Health = 180;
    public HeatlhScript healthbar;
    public bool invFrames = false;
  //      private float comboTime = 0;
    public int coins;
    public CoinBarScript coinbar;
    private int maxCoins;
    public GameObject GameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        if ((SceneManager.GetActiveScene().buildIndex) == 1) {
            FindObjectOfType<AudioManager>().PlaySound("MainTheme");
        } else if ((SceneManager.GetActiveScene().buildIndex) == 2) {
            FindObjectOfType<AudioManager>().PlaySound("Battle");
        }else if ((SceneManager.GetActiveScene().buildIndex) == 3){
            FindObjectOfType<AudioManager>().PlaySound("Boss");
        }

       
        m_Rigidbody = GetComponent<Rigidbody2D>();
        healthbar.SetMaxHealth(Health);
        maxCoins = 40;
        coinbar.SetMaxCoinAmount(maxCoins);
    }

    // Update is called once per frame  
    void Update() 
    {
        if (Time.time >= nextAttackTime && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack_3") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack_2"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
              
                Attack();
                nextAttackTime = Time.time + 1f / atkRate;
            }
        }
      
    }

    void Attack()
    {
       //Attack trigger
            animator.SetTrigger("Attack");
      
            
    }

    private void OnDrawGizmosSelected()
    {
        //Draw in editor
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void AllowMove()
    {
        //Allows player movement
        m_Rigidbody.constraints = RigidbodyConstraints2D.None;
        m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    private void HitBox()
    {
        //Hitbox For Enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
      
         
        foreach (Collider2D enemy in hitEnemies)
        {  
            enemy.GetComponent<Enem>().TakeDamage(40);


            

        }


     
    }

    public void TakeDamage(int damage)
    {
        //Take damage
        m_Rigidbody.constraints = RigidbodyConstraints2D.None;
        m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        Health -= damage;
        healthbar.SetHealth(Health); 
        //Make sure he cacn move
        
        //Hurt anim
        animator.SetTrigger("PlayerHurt");
        //Check current health if it is 0 or not

        if (Health <= 0)
        {
            Die();
            
        }
    }

    void Die()
    {
        //DIE
        animator.SetBool("PlayerDead", true);
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;

        if ((SceneManager.GetActiveScene().buildIndex) == 1)
        {
            FindObjectOfType<AudioManager>().StopSound("MainTheme");
        }
        else if ((SceneManager.GetActiveScene().buildIndex) == 2)
        {
            FindObjectOfType<AudioManager>().StopSound("Battle");
        }
        else if ((SceneManager.GetActiveScene().buildIndex) == 3)
        {
            FindObjectOfType<AudioManager>().StopSound("Boss");
        }
        FindObjectOfType<AudioManager>().PlaySound("Death");

    }
    public void InvcibleFrames()
    {
        //Invicible Frames
        invFrames = true;
    }

    public void DisableFrames()
    {
        //Disables Them
        invFrames = false;
    }

    public void Heal()
    {
        //Heal
        Health += 20;
        healthbar.SetHealth(Health);
    }

    public void CoinUpdate()
    {
        //Update coins
        coins++;
        coinbar.SetCoin(coins);
    }

    public void screenShakeRef()
    {
        //Shake screen
        ScreenShakeController.instance.StartShake(.2f, .1f);
    }
    public void SwordSwingSound()
    {
        //Sound of sword swings
        FindObjectOfType<AudioManager>().PlaySound("PlayerSwordSwing");
    }
}
