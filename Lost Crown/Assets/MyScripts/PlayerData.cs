    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    //Constructor
    public int health;
    public int coins;
    public float[] position;
    public int Scene;

    public PlayerData (PlayerCombat player)
    {
        //Info
        Scene = (SceneManager.GetActiveScene().buildIndex);
        coins = player.coins;
        health = player.Health;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
