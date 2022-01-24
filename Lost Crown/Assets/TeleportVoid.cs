using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportVoid : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.transform.position = this.transform.position; 
    }
}
