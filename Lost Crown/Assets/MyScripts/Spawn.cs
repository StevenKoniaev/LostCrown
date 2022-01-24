using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject EnemSpawn;
    public Transform SpawnPoint;

        private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(EnemSpawn, SpawnPoint.transform.position, Quaternion.identity);
    }
}
