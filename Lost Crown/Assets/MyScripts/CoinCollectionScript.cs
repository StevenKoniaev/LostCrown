using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectionScript : MonoBehaviour
{
    public GameObject myself;
    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().PlaySound("Coin");
            collision.GetComponent<PlayerCombat>().CoinUpdate();
            this.enabled = false;
            myself.SetActive(false);
        }
    }
}
