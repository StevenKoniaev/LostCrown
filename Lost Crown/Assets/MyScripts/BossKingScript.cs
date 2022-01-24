using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKingScript : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    private float oldSpeed;
    public float timer;
    public Transform attackpoint;
    public float AttackRange = 2f;
    public LayerMask PlayerLayer;
    public Rigidbody2D rb;
    public Transform myself;
    public GameObject thePlayer;
    public PlayerCombat playerscript;
    public GameObject youWin;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private RaycastHit2D hitright;
    private Transform target;
    private bool isKnockcback;
    private float distance;
    private bool attackMode;
    public bool inRange;
    private bool cooling;
    private float intTimer;
    private bool SummonDialog = true;
    public BossHealthScript healthbar;
    #endregion
    public Animator anim;
    

    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerscript = thePlayer.GetComponent<PlayerCombat>();
        healthbar.SetMaxBossHealth(400);
        oldSpeed = moveSpeed;
    }
    private void Awake()
    {
        intTimer = timer;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (isKnockcback)
        {
            
            return;
        }
        if (inRange == true)
        {

            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }
        if (inRange == false)
        {
            anim.SetBool("EnemWalking", false);
            StopAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.transform;
            
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(myself.transform.position, target.position);

        if (distance > attackDistance)
        {
            EnemMove();
            StopAttack();
        }
        else if (attackDistance >= Mathf.Abs(distance) && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("EnemAttacking", false);
        }
    }


    void EnemMove()
    {


        anim.SetBool("EnemWalking", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            Vector3 rotation = myself.transform.eulerAngles;
            if (transform.position.x > target.position.x)
            {
                rotation.y = 0f;
            }
            else
            {
                rotation.y = 180f;
            }
            myself.transform.eulerAngles = rotation;
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("EnemWalking", false);
        int randomNum = Random.Range(0 , 3);

  
        if (randomNum == 1)
        {
            moveSpeed = 5f;
            anim.SetBool("MaceAttack", true);
        }
        else
        {
            anim.SetBool("MaceAttackTwo", true);
        }


    }

    void StopAttack()
    {
        
        cooling = false;
        attackMode = false;
        anim.SetBool("GroundAttack", false);
        anim.SetBool("MaceAttack",false);
        anim.SetBool("MaceAttackTwo", false);
        moveSpeed = oldSpeed;
    }


    public void TriggerCooling()
    {
        cooling = true;
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    public void HitBox()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackpoint.position, AttackRange, PlayerLayer);

        if (hitPlayer != null & playerscript.invFrames == false)

        {
            hitPlayer.GetComponent<PlayerCombat>().TakeDamage(40);
        }

    }

    public void Knockback(int knockbackthrust)
    {

        isKnockcback = true;
        rb.AddForce(Vector2.right * knockbackthrust);

    }

    public void KnockbackStop()
    {
        isKnockcback = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackpoint.position, AttackRange);
    }

    public void JumpBack()
    {
    }
    public void JumpForward()
    {

    }

    public void Teleport()
    {
       transform.position = target.transform.position;
    }

    public void YouWin()
    {
        Time.timeScale = 0f;
        youWin.SetActive(true);
        FindObjectOfType<AudioManager>().StopSound("Boss");
        FindObjectOfType<AudioManager>().PlaySound("Win");

    }
}
