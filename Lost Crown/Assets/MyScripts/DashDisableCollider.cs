using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DashDisableCollider : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement playermovement;
    public Rigidbody2D rb;
    private float g;

    public void Start()
    {
        playermovement = player.GetComponent<PlayerMovement>();
        g = rb.gravityScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (playermovement.isDashing == true)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                rb.gravityScale = 0;
                StartCoroutine(EnableBox(.5f));
            }
        }
    }

    IEnumerator EnableBox(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<BoxCollider2D>().enabled = true;
        rb.gravityScale = g;
    }
}