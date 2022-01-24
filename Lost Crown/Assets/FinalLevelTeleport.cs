using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinalLevelTeleport : MonoBehaviour
{

    public GameObject myself;
    public GameObject otherversion;
    // Update is called once per frame
    void Update()
    {
        if (myself.GetComponent<Enem>().currentHealth <= 0 && otherversion.GetComponent<Enem>().currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1f;
        }
    }
}
