using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class PersonalAI : MonoBehaviour
{
    public Animator animator;
    public Transform target;
    public float speed = 200f;
    public float nextWayPointD = 3f;
    Path path;
    int currentWayPoint = 0;
    bool reached = false;
    Seeker seeker;
    float nextAttackTime = 0f;
    public Transform enemyGFX;
    public float atkRate = 100f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f); 
        
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            animator.SetBool("EnemWalking", false);

            path = p;
            currentWayPoint = 0;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (path == null)
        {
           
            return;
        }


        if (currentWayPoint >= path.vectorPath.Count)
        {
            reached = true;

            animator.SetBool("EnemWalking", !reached);
          
            return;
        }
        else
        {
            reached = false;
            animator.SetBool("EnemWalking", !reached);
            Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
       
            if (distance < nextWayPointD)
            {
                animator.SetBool("EnemWalking", true);
                currentWayPoint++;
            }

            if (rb.velocity.x >= 0.01f)
            {
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        
    }

  
}
