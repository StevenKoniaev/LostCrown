using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthpickupScript : MonoBehaviour
{
    public GameObject myself;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().PlaySound("Pickup");
            collision.GetComponent<PlayerCombat>().Heal();
        this.enabled = false;
        myself.SetActive(false);
        }
    }
}
